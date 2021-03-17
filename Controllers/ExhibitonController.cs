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

        

        public IActionResult RentalIndex()
        {
            
            return View();
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
        
    }
}
