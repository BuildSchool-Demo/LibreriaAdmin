using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    
    public class ExhibitonController : Controller
    {
        public IExhibitonService _service;

        public ExhibitonController(IExhibitonService service)
        {
            _service = service;
        }

        [Route("api/[controller]/[action]")]
        [HttpGet]
        public BaseModel.BaseResult<List<RentalViewModel>> GetRentalDate()
        {
            return _service.RentalGetAll();
        }

        public IActionResult RentalIndex()
        {
            return View();
        }

        [Route("api/[controller]/[action]")]
        [HttpGet]
        public BaseModel.BaseResult<List<ExhibitonViewModel>> GetExhibitonData()
        {

            return  _service.ExhibitonGetAll();
        }
        public IActionResult ExhibitonIndex()
        {
            
            return View();
        }

        public IActionResult Email()
        {
            return View();
        }

        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendMail([Bind("sender,recipient,subject,body")] ExhibitonSendMailViewModel mailVM)
        {
            string result = _service.Send(mailVM.sender, mailVM.recipient, mailVM.subject, mailVM.body);
            return Content(result);
        }
    }
}
