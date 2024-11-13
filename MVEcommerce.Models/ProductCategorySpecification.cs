using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* Used, Implement later */
namespace MVEcommerce.Models
{
    public class ProductCategorySpecification
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("CategorySpecification")]
        public int CategorySpecificationId { get; set; }

        [Required]
        public required string Value { get; set; }

        // Navigation property
        public virtual Product? Product { get; set; }
        public virtual CategorySpecification? CategorySpecification { get; set; }
    }
}
