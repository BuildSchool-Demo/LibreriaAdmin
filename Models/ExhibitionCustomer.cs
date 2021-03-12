using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class ExhibitionCustomer
    {
        public ExhibitionCustomer()
        {
            ExhibitionOrders = new HashSet<ExhibitionOrder>();
        }

        public int ExCustomerId { get; set; }
        public string ExCustomerName { get; set; }
        public string ExCustomerPhone { get; set; }
        public string ExCustomerEmail { get; set; }

        public virtual ICollection<ExhibitionOrder> ExhibitionOrders { get; set; }
    }
}
