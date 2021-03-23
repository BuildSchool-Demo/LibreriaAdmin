using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            Favorites = new HashSet<Favorite>();
            OrderDetails = new HashSet<OrderDetail>();
            Previews = new HashSet<Preview>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        [Required]
        [Column("ISBN")]
        [StringLength(50)]
        public string Isbn { get; set; }
        public int SupplierId { get; set; }
        [Required]
        [StringLength(50)]
        public string Author { get; set; }
        public int Inventory { get; set; }
        public int CategoryId { get; set; }
        [Column(TypeName = "date")]
        public DateTime PublishDate { get; set; }
        public int Sort { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
        public string Introduction { get; set; }
        public int? TotalSales { get; set; }
        [Column("isSpecial")]
        public bool IsSpecial { get; set; }
        [Column(TypeName = "money")]
        public decimal SpecialPrice { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("Products")]
        public virtual Supplier Supplier { get; set; }
        [InverseProperty(nameof(Favorite.Product))]
        public virtual ICollection<Favorite> Favorites { get; set; }
        [InverseProperty(nameof(OrderDetail.Product))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [InverseProperty(nameof(Preview.Product))]
        public virtual ICollection<Preview> Previews { get; set; }
        [InverseProperty(nameof(ShoppingCart.Product))]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
