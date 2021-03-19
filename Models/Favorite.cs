using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    [Table("Favorite")]
    public partial class Favorite
    {
        [Key]
        public int FavoriteId { get; set; }
        public int ProductId { get; set; }
        [Column("memberId")]
        public int MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        [InverseProperty("Favorites")]
        public virtual Member Member { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Favorites")]
        public virtual Product Product { get; set; }
    }
}
