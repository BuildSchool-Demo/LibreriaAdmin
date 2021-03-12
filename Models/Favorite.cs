using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class Favorite
    {
        public int FavoriteId { get; set; }
        public int ProductId { get; set; }
        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
    }
}
