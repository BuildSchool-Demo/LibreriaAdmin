using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class ExhibitionOrder
    {
        public int ExOrderId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int ExCustomerId { get; set; }
        public string PaymentState { get; set; }

        public virtual ExhibitionCustomer ExCustomer { get; set; }
    }
}
