using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tsumasakidachi.Estates.EntityMappers
{
    public interface IEntityMapper<TDto, TEntity>
    {
        public TEntity Map(TDto dto);
    }
}
