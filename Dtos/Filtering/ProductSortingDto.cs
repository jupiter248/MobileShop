using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Filtering
{
    public class ProductSortingDto
    {
        public bool NewestArrivals { get; set; }
        public bool HighestPrice { get; set; }
        public bool LowestPrice { get; set; }
    }
}