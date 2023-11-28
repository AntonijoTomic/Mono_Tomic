using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Classes
{
    public class PagingInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagingInfo()
        {
            
            PageNumber = 1;
            PageSize = 5;
        }
    }
}
