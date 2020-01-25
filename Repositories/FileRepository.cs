using Microsoft.Extensions.Caching.Memory;
using NaturalSort.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Tsumasakidachi.Estates.Dtos;
using Tsumasakidachi.Estates.Entities;
using Tsumasakidachi.Estates.EntityMappers;

namespace Tsumasakidachi.Estates.Repositories
{
    public class FileRepository : IRepository<File>
    {
        protected IEntityMapper<FileDto, File> EntityMapper;
        protected readonly HttpClient Client;
        protected readonly IMemoryCache MemoryCache;

        public FileRepository(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory, IEntityMapper<FileDto, File> entityMapper)
        {
            Client = httpClientFactory.CreateClient("Tsumasakidachi");
            EntityMapper = entityMapper;
            MemoryCache = memoryCache;
        }

        public async Task<IEnumerable<File>> FindAllAsync(IDictionary<string, object> options)
        {
            var parameters = new Dictionary<string, string>();
            var url = "/files";

            if (options.ContainsKey("Path"))
            {
                url += options["Path"].ToString();
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
                var serializer = new DataContractJsonSerializer(typeof(ItemsDto<FileDto>));
                var structureDtos = serializer.ReadObject(responseStream) as ItemsDto<FileDto>;
                var structures = (from dto in structureDtos.items
                                  select EntityMapper.Map(dto)).ToList();
                var comparer = StringComparer.OrdinalIgnoreCase.WithNaturalSort();
                structures.Sort((a, b) => comparer.Compare(a.Url.ToString(), b.Url.ToString()));

                return structures;
            });
        }

        public async Task<File> FindAsync(IDictionary<string, object> options)
        {
            return (await FindAllAsync(options)).FirstOrDefault();
        }
    }
}
