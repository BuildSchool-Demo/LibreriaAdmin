using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ExhibitonController : ControllerBase
    {
        public IExhibitonService _service;

        public ExhibitonController(IExhibitonService service)
        {
            _service = service;
        }

        [HttpGet]
        public BaseModel.BaseResult<RentalViewModel.RentalListResult> GetRentalDate()
        {
            var result = new BaseModel.BaseResult<RentalViewModel.RentalListResult>();

            try
            {
                result.Body = _service.RentalGetAll();
                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;
                return result;
            }

        }

        [HttpGet]
        public BaseModel.BaseResult<ExhibitonViewModel.ExhibitonListResult> GetExhibitonData()
        {
            var result = new BaseModel.BaseResult<ExhibitonViewModel.ExhibitonListResult>();
            try
            {
                result.Body = _service.ExhibitonGetAll();
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
        public IActionResult SendMail([FromBody] ExhibitonSendMailViewModel.SendMailSingleResult mailVM)
        {
            string result = _service.Send(mailVM.sender, mailVM.recipient, mailVM.subject, mailVM.body, mailVM.exhibitionId);
            return Content(result);
        }

        [HttpGet("{exhibitionId}")]
        public BaseModel.BaseResult<ExhibitonSendMailViewModel.GetByCustomerEmailRequest> GetByCustomerEmail (int exhibitionId)
        {
            var result = new BaseModel.BaseResult<ExhibitonSendMailViewModel.GetByCustomerEmailRequest>();
            try
            {
                result.Body = _service.GetCustomerData(exhibitionId);
                return result;
            }
            catch(Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }

        [HttpGet("{exhibitionId}")]
        public BaseModel.BaseResult<ExhibitonEmailViewModel.EmailListResult> EmailGetAll(int exhibitionId)
        {
            var result = new BaseModel.BaseResult<ExhibitonEmailViewModel.EmailListResult>();
            try
            {
                
                result.Body = _service.EmailGetAll(exhibitionId);
                
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
        public BaseModel.BaseResult<ExhibitonEmailViewModel.EmailSingleResult> ConfirmEmail(ExhibitonEmailViewModel.EmailSingleResult ExVM)
        {
            BaseModel.BaseResult<ExhibitonEmailViewModel.EmailSingleResult> result = new BaseModel.BaseResult<ExhibitonEmailViewModel.EmailSingleResult>();
            result.Body = ExVM;
            try
            {
                result.IsSuccess = _service.ConfirmEmail(ExVM);
                return result;
            }
            catch(Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }

        [HttpGet]
        public BaseModel.BaseResult<ExhibitonViewModel.ExhibitonListResult> GetTodayExhibiton()
        {
            var result = new BaseModel.BaseResult<ExhibitonViewModel.ExhibitonListResult>();

            try
            {
                result.Body = _service.GetTodayExhibiton();
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
        public BaseModel.BaseResult<Task<bool>> ModifyConfirm([FromForm] ExhibitonEmailViewModel.ModifyExhibitionModel ExVM)
        {
            var result = new BaseModel.BaseResult<Task<bool>>();

            try
            {
                result.Body = _service.ModifyConfirm(ExVM);
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
        public BaseModel.BaseResult<ExhibitonViewModel.Deleted> IsDeleted(ExhibitonViewModel.Deleted ExVM)
        {
            BaseModel.BaseResult<ExhibitonViewModel.Deleted> result = new BaseModel.BaseResult<ExhibitonViewModel.Deleted>();
            result.Body = ExVM;
            try
            {
                result.IsSuccess = _service.ConfirmDeleted(ExVM);
                return result;
            }
            catch(Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }
    }
}
