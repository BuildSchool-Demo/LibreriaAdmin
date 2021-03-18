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

            result.MemberList = _dbRepository.GetAll<Member>()
                .Select(x => new MemberViewModel.MemberSingleResult()
                {
                    memberId = x.MemberId,
                    memberUserName = x.MemberUserName,
                    mobileNumber = x.MobileNumber,
                    homeNumber = x.HomeNumber,
                    address = x.Address,
                    email = x.Email,
                    memberName = x.MemberName,
                    memberPassword = x.MemberPassword,
                    birthday = x.Birthday,
                    gender = x.Gender,
                    idnumber = x.Idnumber
                }).ToList();


                
            return result;
        }
    }
}
