using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTask.DomainModel.Entities
{
    public class ProductAttribute
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public ProductAttribute()
        {
            Products = new HashSet<Product>();
        }
    }
}
