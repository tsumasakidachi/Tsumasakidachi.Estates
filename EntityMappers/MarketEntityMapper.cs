using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsumasakidachi.Estates.Dtos;
using Tsumasakidachi.Estates.Entities;

namespace Tsumasakidachi.Estates.EntityMappers
{
    public class MarketEntityMapper : IEntityMapper<MarketDto, Market>
    {
        public Market Map(MarketDto dto)
        {
            var entity = new Market();

            entity.Id = dto.id;
            entity.UpdatedAt = DateTime.Parse(dto.updated_at);
            entity.Name = dto.name;
            entity.Order = dto.order;

            return entity;
        }
    }
}
