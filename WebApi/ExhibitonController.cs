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
        public BaseModel.BaseResult<List<RentalViewModel>> GetRentalDate()
        {
            var result = new BaseModel.BaseResult<List<RentalViewModel>>();

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
        public BaseModel.BaseResult<List<ExhibitonViewModel>> GetExhibitonData()
        {
            var result = new BaseModel.BaseResult<List<ExhibitonViewModel>>();
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
        public IActionResult SendMail([FromBody] ExhibitonSendMailViewModel mailVM)
        {
            string result = _service.Send(mailVM.sender, mailVM.recipient, mailVM.subject, mailVM.body);
            return Content(result);
        }

        [HttpGet]
        public BaseModel.BaseResult<List<ExhibitonEmailViewModel>> GetEmailData()
        {
            var result = new BaseModel.BaseResult<List<ExhibitonEmailViewModel>>();
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
    }
}
