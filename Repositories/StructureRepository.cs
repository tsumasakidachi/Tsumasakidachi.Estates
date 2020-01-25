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
    public class StructureRepository : IRepository<Structure>
    {
        protected readonly IEntityMapper<StructureDto, Structure> EntityMapper;
        protected readonly HttpClient Client;
        protected readonly IMemoryCache MemoryCache;

        public StructureRepository(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory, IEntityMapper<StructureDto, Structure> entityMapper)
        {
            Client = httpClientFactory.CreateClient("Tsumasakidachi");
            EntityMapper = entityMapper;
            MemoryCache = memoryCache;
        }

        public async Task<IEnumerable<Structure>> FindAllAsync(IDictionary<string, object> options)
        {
            var parameters = new Dictionary<string, string>();

            if (options.ContainsKey("Id"))
            {
                parameters.Add("id", options["Id"].ToString());
            }
            else if (options.ContainsKey("Slug"))
            {
                parameters.Add("slug", options["Slug"].ToString());
            }
            else if (options.ContainsKey("Available"))
            {
                parameters.Add("available", options["Available"].ToString());
            }
            
            if (options.ContainsKey("IncludeServices"))
            {
                parameters.Add("include_services", options["IncludeServices"].ToString());

                if(options.ContainsKey("HasServices"))
                {
                    parameters.Add("has_services", options["HasServices"].ToString());
                }
            }

            var url = "/structures";
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
                    throw new Exception(response.StatusCode.ToString() + " " + await response.Content.ReadAsStringAsync());
                }

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var serializer = new DataContractJsonSerializer(typeof(ItemsDto<StructureDto>));
                var structureDtos = serializer.ReadObject(responseStream) as ItemsDto<StructureDto>;
                var structures = (from dto in structureDtos.items
                                  select EntityMapper.Map(dto)).ToList();
                var comparer = StringComparer.OrdinalIgnoreCase.WithNaturalSort();
                structures.Sort((a, b) => comparer.Compare(a.Order, b.Order));

                return structures;
            });
        }

        public async Task<Structure> FindAsync(IDictionary<string, object> options)
        {
            return (await FindAllAsync(options)).FirstOrDefault();
        }
    }
}
