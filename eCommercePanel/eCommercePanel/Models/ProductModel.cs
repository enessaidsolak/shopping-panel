using eCommercePanel.DATA.Entity;
using System.Collections.Generic;

namespace eCommercePanel.Models
{
    public class ProductModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
