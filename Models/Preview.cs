using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    [Table("Preview")]
    public partial class Preview
    {
        [Key]
        public int PreviewId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        public int Sort { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Previews")]
        public virtual Product Product { get; set; }
    }
}
