using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Interfaces
{
    public interface IMemberService
    {
        MemberViewModel.MemberListResult GetAll();

        MemberViewModel.MemberListResult GetAllLength();
        OrderViewModel.OrderListResult GetByMemberId(int id);

        bool Edit(MemberViewModel.MemberSingleResult memberVM);

        public bool Remove(int MemberId);
    }
}
