using LibreriaAdmin.Interfaces;
using LibreriaAdmin.Models;
using LibreriaAdmin.Repository;
using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Services
{
    public class MemberService : IMemberService
    {
        private readonly IRepository _dbRepository;

        public MemberService(IRepository repository)
        {
            _dbRepository = repository;
        }

        public MemberViewModel.MemberListResult GetAll()
        {
            var result = new MemberViewModel.MemberListResult();

            result.MemberList =
                (from Member in _dbRepository.GetAll<Member>()
                 select new MemberViewModel.MemberSingleResult()
                 {
                     memberId = Member.MemberId,
                     memberUserName = Member.MemberUserName,
                     mobileNumber = Member.MobileNumber,
                     homeNumber = Member.HomeNumber,
                     address = Member.Address,
                     email = Member.Email,
                     memberName = Member.MemberName,
                     memberPassword = Member.MemberPassword,
                     birthday = Member.Birthday,
                     gender = Member.Gender,
                     idnumber = Member.Idnumber
                 }).ToList();


            return result;
        }

        public OrderViewModel.OrderListResult GetByMemberId(int id)
        {
            var result = new OrderViewModel.OrderListResult();

            result.OrderList = (from o in _dbRepository.GetAll<Order>().Where(x => x.MemberId == id)
                                join od in _dbRepository.GetAll<OrderDetail>()
                                on o.OrderId equals od.OrderId
                                select new OrderViewModel.OrderSingleResult
                                {
                                    
                                });

        }
    }
}
