using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models
{
    public class CategorySpecification
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        public required string Name { get; set; }

        // Navigation property
        public virtual Category? Category { get; set; }
        public virtual ICollection<ProductCategorySpecification>? ProductCategorySpecifications { get; set; }
    }
}
