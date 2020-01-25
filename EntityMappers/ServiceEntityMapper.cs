using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsumasakidachi.Estates.Dtos;
using Tsumasakidachi.Estates.Entities;

namespace Tsumasakidachi.Estates.EntityMappers
{
    public class ServiceEntityMapper : IEntityMapper<ServiceDto, Service>
    {
        public Service Map(ServiceDto dto)
        {
            var entity = new Service();

            entity.Id = dto.id;
            entity.UpdatedAt = DateTime.Parse(dto.updated_at);
            entity.Order = dto.order;
            entity.Floor = dto.floor;
            entity.Name = dto.name;
            entity.Area = dto.area;
            entity.ProvideAs = dto.provide_as;
            entity.CostPerSqM = dto.cost_per_sq_m;
            entity.Unit = dto.unit;

            return entity;
        }
    }
}
