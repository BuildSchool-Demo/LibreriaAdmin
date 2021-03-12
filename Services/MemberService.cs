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
        private readonly LibreriaRepository _dbRepository;

        public MemberService()
        {
            _dbRepository = new LibreriaRepository();
        }

        public List<MemberViewModel> GetAll()
        {
            var result = (
                from member in _dbRepository.GetAll<Member>()
                select new MemberViewModel()
                {
                    memberUserName = member.MemberUserName,
                    MobileNumber = member.MobileNumber,
                    HomeNumber = member.HomeNumber,
                    Address = member.Address,
                    Email = member.Email,
                    memberName = member.MemberName,
                    memberPassword = member.MemberPassword,
                    birthday = member.Birthday,
                    Gender = member.Gender,
                    IDnumber = member.Idnumber
                }).ToList();

            return result;
        }
    }
}
