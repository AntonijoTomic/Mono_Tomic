using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Classes
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalPages { get; set; }
    }
}
