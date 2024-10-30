using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [ForeignKey("ParentCategoryNavigation")]
        public int? ParentCategory { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Slug { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public virtual Category ParentCategoryNavigation { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
