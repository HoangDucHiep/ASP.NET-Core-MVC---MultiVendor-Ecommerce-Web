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

        //Can't handle now - fix later
        //[ForeignKey("ParentCategoryNavigation")]
        //public int? ParentCategory { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Slug { get; set; }

        //public string? Description { get; set; }
        public string? BannerImage { get; set; }

        [Required]
        public required string Status { get; set; }  // Active, InActive

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        //public virtual Category ParentCategoryNavigation { get; set; }
        //public virtual ICollection<Category> ChildCategories { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
