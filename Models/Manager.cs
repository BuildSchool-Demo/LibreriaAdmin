using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class Manager
    {
        public string ManagerId { get; set; }
        public string ManagerUsername { get; set; }
        public string ManagerPassword { get; set; }
        public string ManagerPhoto { get; set; }
        public string ManagerName { get; set; }
        public string JobTitle { get; set; }
    }
}
