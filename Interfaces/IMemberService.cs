using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Interfaces
{
    public interface IMemberService
    {
        List<MemberViewModel> GetAll();
    }
}
