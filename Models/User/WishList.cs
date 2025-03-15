using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Products;

namespace MainApi.Models.User
{
    public class WishList
    {
        public required string UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public required int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}