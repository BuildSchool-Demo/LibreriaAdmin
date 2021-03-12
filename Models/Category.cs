using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? Updatetime { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
