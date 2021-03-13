using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class Exhibition
    {
        public int ExhibitionId { get; set; }
        public DateTime ExhibitionStartTime { get; set; }
        public DateTime ExhibitionEndTime { get; set; }
        public string ExhibitionIntro { get; set; }
        public string MasterUnit { get; set; }
        public decimal ExhibitionPrice { get; set; }
        public DateTime EditModifyDate { get; set; }
        public int ExCustomerId { get; set; }
        public string ExPhoto { get; set; }
        public string ExName { get; set; }
        public bool ReviewState { get; set; }
    }
}
