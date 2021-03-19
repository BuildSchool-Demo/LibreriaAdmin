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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }

        [HttpPost]
        public IActionResult SendMail([FromBody] ExhibitonSendMailViewModel.SendMailSingleResult mailVM)
        {
            string result = _service.Send(mailVM.sender, mailVM.recipient, mailVM.subject, mailVM.body);
            return Content(result);
        }

        [HttpGet]
        public BaseModel.BaseResult<ExhibitonEmailViewModel.EmailListResult> GetEmailData()
        {
            var result = new BaseModel.BaseResult<ExhibitonEmailViewModel.EmailListResult>();
            try
            {
                int i = 1;
                result.Body = _service.EmailGetAll(i);
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
        public BaseModel.BaseResult<List<ExhibitonViewModel>> GetTodayExhibiton()
        {
            var result = new BaseModel.BaseResult<List<ExhibitonViewModel>>();

            try
            {
                result.Body = _service.ExhibitonGetToday();
                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;
                return result;
            }

        }
    }
}
