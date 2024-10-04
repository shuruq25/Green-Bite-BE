using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Utils
{
    public class PaginationOptions
    {
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public SortOptions Sort { get; set; } = new SortOptions();
        public FilterOptions Filter { get; set; } = new FilterOptions();
    }
}
