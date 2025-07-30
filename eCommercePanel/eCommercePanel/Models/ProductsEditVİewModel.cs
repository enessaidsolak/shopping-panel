using Microsoft.AspNetCore.Http;
using eCommercePanel.DATA.Entity;
using System.Collections.Generic;

namespace eCommercePanel.Models
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
