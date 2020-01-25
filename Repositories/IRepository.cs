using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tsumasakidachi.Estates.Repositories
{
    public interface IRepository<T>
    {
        public Task<T> FindAsync(IDictionary<string, object> options);
        public Task<IEnumerable<T>> FindAllAsync(IDictionary<string, object> options);
    }
}
