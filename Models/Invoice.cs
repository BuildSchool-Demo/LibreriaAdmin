using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        public string TypeName { get; set; }
        public string TypeInfo { get; set; }
    }
}
