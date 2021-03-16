using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class ExhibitonSendMailViewModel
    {
        public string sender { get; set; }
        public string recipient { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
