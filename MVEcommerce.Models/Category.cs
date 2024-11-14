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


        public string? Name { get; set; }


        public string? Slug { get; set; }

        //public string? Description { get; set; }
        public string? BannerImage { get; set; } = string.Empty;


        public string? Status { get; set; } = "Active";  // Active, InActive


        public DateTime? CreatedAt { get; set; } = DateTime.Now;


        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        //public virtual Category ParentCategoryNavigation { get; set; }
        //public virtual ICollection<Category> ChildCategories { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
