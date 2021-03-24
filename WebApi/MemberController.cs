using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public BaseModel.BaseResult<MemberViewModel.MemberListResult> GetAll()
        {
            var result = new BaseModel.BaseResult<MemberViewModel.MemberListResult>();

            try
            {
                result.Body = _memberService.GetAll();
                return result;
            }
            catch(Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;

                return result;
            }
        }

        [HttpPost]
        public BaseModel.BaseResult<MemberViewModel.MemberSingleResult> Edit(MemberViewModel.MemberSingleResult memberVM)
        {
            BaseModel.BaseResult<MemberViewModel.MemberSingleResult> result = new BaseModel.BaseResult<MemberViewModel.MemberSingleResult>();
            result.Body = memberVM;

            try
            {
                result.IsSuccess = _memberService.Edit(memberVM);
                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;

                return result;
            }


        }

        [HttpGet("{id}")]
        public BaseModel.BaseResult<OrderViewModel.OrderListResult> GetByMemberId(int id)
        {
            var result = new BaseModel.BaseResult<OrderViewModel.OrderListResult>();

            try
            {
                result.Body = _memberService.GetByMemberId(id);
                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;

                return result;
            }
        }


        [HttpPost]
        public bool DeleteItem(MemberViewModel.GetByIdRequest request)
        {
            bool isSuccess = _memberService.Remove(request.MemberId);
            return isSuccess;
        }
    }
}
