using Microsoft.Extensions.Caching.Memory;
using NaturalSort.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Tsumasakidachi.Estates.Dtos;
using Tsumasakidachi.Estates.Entities;
using Tsumasakidachi.Estates.EntityMappers;

namespace Tsumasakidachi.Estates.Repositories
{
    public class MarketRepository : IRepository<Market>
    {
        protected IEntityMapper<MarketDto, Market> EntityMapper;
        protected readonly HttpClient Client;
        protected readonly IMemoryCache MemoryCache;

        public MarketRepository(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory, IEntityMapper<MarketDto, Market> entityMapper)
        {
            Client = httpClientFactory.CreateClient("Tsumasakidachi");
            EntityMapper = entityMapper;
            MemoryCache = memoryCache;
        }

        public async Task<IEnumerable<Market>> FindAllAsync(IDictionary<string, object> options)
        {
            var parameters = new Dictionary<string, string>();

            if (options.ContainsKey("Id"))
            {
                parameters.Add("id", options["Id"].ToString());
            }

            var url = "/markets";
            var content = await new FormUrlEncodedContent(parameters).ReadAsStringAsync();

            if(!string.IsNullOrEmpty(content))
            {
                url += "?" + content;
            }

            return await MemoryCache.GetOrCreateAsync(url, async e =>
            {
                e.SetOptions(new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromSeconds(120)
                });

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString() + "\n" + await response.Content.ReadAsStringAsync());
                }

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var serializer = new DataContractJsonSerializer(typeof(ItemsDto<MarketDto>));
                var structureDtos = serializer.ReadObject(responseStream) as ItemsDto<MarketDto>;
                var structures = (from dto in structureDtos.items
                                  select EntityMapper.Map(dto)).ToList();
                var comparer = StringComparer.OrdinalIgnoreCase.WithNaturalSort();
                structures.Sort((a, b) => comparer.Compare(a.Order, b.Order));

                return structures;
            });
        }

        public async Task<Market> FindAsync(IDictionary<string, object> options)
        {
            return (await FindAllAsync(options)).FirstOrDefault();
        }
    }
}
