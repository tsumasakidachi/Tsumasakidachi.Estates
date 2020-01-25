using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsumasakidachi.Estates.Dtos;
using Tsumasakidachi.Estates.Entities;

namespace Tsumasakidachi.Estates.EntityMappers
{
    public class FileEntityMapper : IEntityMapper<FileDto, File>
    {
        public File Map(FileDto dto)
        {
            var entity = new File();

            entity.Id = dto.id;
            entity.UpdatedAt = DateTime.Parse(dto.updated_at);
            entity.Url = new Uri(dto.url);

            return entity;
        }
    }
}
