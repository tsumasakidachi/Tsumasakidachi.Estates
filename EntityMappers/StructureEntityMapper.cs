using NaturalSort.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsumasakidachi.Estates.Dtos;
using Tsumasakidachi.Estates.Entities;

namespace Tsumasakidachi.Estates.EntityMappers
{
    public class StructureEntityMapper : IEntityMapper<StructureDto, Structure>
    {
        protected IEntityMapper<ServiceDto, Service> ServiceMapper;
        protected IEntityMapper<MarketDto, Market> MarketMapper;

        public StructureEntityMapper(IEntityMapper<ServiceDto, Service> serviceMapper, IEntityMapper<MarketDto, Market> marketMapper)
        {
            ServiceMapper = serviceMapper;
            MarketMapper = marketMapper;
        }

        public Structure Map(StructureDto dto)
        {
            var entity = new Structure();
            entity.Id = dto.id;
            entity.UpdatedAt = DateTime.Parse(dto.updated_at);
            entity.Name = dto.name;
            entity.Order = dto.order;
            entity.Slug = dto.slug;
            entity.Comment = dto.comment;
            entity.IsPublished = dto.is_published;
            entity.TagIds = dto.tag_ids != null ? new List<string>(dto.tag_ids) : new List<string>();
            entity.MarketId = dto.market_id;
            entity.Market = MarketMapper.Map(dto.market);
            entity.Type = dto.type;
            entity.CompletedAt = dto.completed_at;
            entity.Status = dto.status;
            entity.Address = dto.address;
            entity.Server = dto.server;
            entity.World = dto.world;
            entity.Lat = dto.lat;
            entity.Lon = dto.lon;
            entity.EyecatchUrl = dto.eyecatch_url != null ? new Uri(dto.eyecatch_url) : null;

            if (dto.services != null)
            {
                entity.Services = (from serviceDto in dto.services
                                   select ServiceMapper.Map(serviceDto)).ToList();
                var comparer = StringComparer.OrdinalIgnoreCase.WithNaturalSort();
                entity.Services.Sort((a, b) => comparer.Compare(a.Order, b.Order));
                entity.Services.Reverse();
            }
            else
            {
                entity.Services = new List<Service>();
            }

            return entity;
        }
    }
}
