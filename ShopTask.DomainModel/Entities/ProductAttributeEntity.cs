using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShopTask.DomainModel.Entities
{
    public class ProductAttribute
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }

        public ProductAttribute()
        {
            AttributeValues = new List<AttributeValue>();
        }
    }
}
