using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class Preview
    {
        public int PreviewId { get; set; }
        public int ProductId { get; set; }
        public string ImgUrl { get; set; }
        public int Sort { get; set; }

        public virtual Product Product { get; set; }
    }
}
