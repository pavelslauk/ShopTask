using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShopTask.DomainModel.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [RegularExpression(@"((^[1-9][0-9]*(((\.)|(\,))[0-9]*)?)|(^0((\.)|(\,))(([0-9]*)?[1-9]([0-9]*)?)))$")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ProductAttribute> AttributeValues { get; set; }

        public Product()
        {
            AttributeValues = new HashSet<ProductAttribute>();
        }
    }
}