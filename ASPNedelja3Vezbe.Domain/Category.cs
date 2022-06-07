using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNedelja3Vezbe.Domain
{ 
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public int? ParentId { get; set; }

        public virtual Image Image { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; } = new List<Category>();
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<CategorySpecification> Specifications { get; set; } = new List<CategorySpecification>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
