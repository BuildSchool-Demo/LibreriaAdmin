using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    [Table("Exhibition")]
    public partial class Exhibition
    {
        [Key]
        [Column("ExhibitionID")]
        public int ExhibitionId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExhibitionStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExhibitionEndTime { get; set; }
        [Required]
        public string ExhibitionIntro { get; set; }
        [Required]
        [StringLength(50)]
        public string MasterUnit { get; set; }
        [Column(TypeName = "money")]
        public decimal ExhibitionPrice { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EditModifyDate { get; set; }
        public int ExCustomerId { get; set; }
        [Required]
        public string ExPhoto { get; set; }
        [Required]
        [StringLength(50)]
        public string ExName { get; set; }
        public bool ReviewState { get; set; }
        [Column("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
