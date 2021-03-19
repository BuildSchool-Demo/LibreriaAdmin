using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    [Table("ShoppingCart")]
    public partial class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        [Column("memberId")]
        public int MemberId { get; set; }
        public int Count { get; set; }

        [ForeignKey(nameof(MemberId))]
        [InverseProperty("ShoppingCarts")]
        public virtual Member Member { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ShoppingCarts")]
        public virtual Product Product { get; set; }
    }
}
