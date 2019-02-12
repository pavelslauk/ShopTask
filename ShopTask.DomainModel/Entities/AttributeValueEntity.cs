using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTask.DomainModel.Entities
{
    public class AttributeValue
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int ProductAttributeId { get; set; }

        public virtual ProductAttribute ProductAttribute { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public AttributeValue()
        {
            Products = new List<Product>();
        }
    }
}
