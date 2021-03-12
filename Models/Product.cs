using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class Product
    {
        public Product()
        {
            Favorites = new HashSet<Favorite>();
            OrderDetails = new HashSet<OrderDetail>();
            Previews = new HashSet<Preview>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Isbn { get; set; }
        public int SupplierId { get; set; }
        public string Author { get; set; }
        public int Inventory { get; set; }
        public int CategoryId { get; set; }
        public DateTime PublishDate { get; set; }
        public int Sort { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Introduction { get; set; }
        public int? TotalSales { get; set; }
        public bool IsFav { get; set; }
        public bool IsSpecial { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Preview> Previews { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
