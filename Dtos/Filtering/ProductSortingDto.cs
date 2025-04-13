using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Filtering
{
    public class ProductSortingDto
    {
        public bool NewestArrivals { get; set; } = false;
        public bool HighestPrice { get; set; } = false;
        public bool LowestPrice { get; set; } = false;
    }
}