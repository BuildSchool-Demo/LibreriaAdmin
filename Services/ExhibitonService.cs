using LibreriaAdmin.Repository;
using LibreriaAdmin.ViewModels;
using LibreriaAdmin.Models;
using LibreriaAdmin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace LibreriaAdmin.Services
{
    public class ExhibitonService : IExhibitonService
    {
        private readonly LibreriaRepository _dbRepository;
        private readonly IConfiguration _config;

        public ExhibitonService(IConfiguration config)
        {
            _dbRepository = new LibreriaRepository();
            _config = config;

        }

        public BaseModel.BaseResult<List<ExhibitonViewModel>> ExhibitonGetAll()
        {
            var result = new BaseModel.BaseResult<List<ExhibitonViewModel>>();

            result.Body = _dbRepository.GetAll<Exhibition>()
                                       .Select(x => new ExhibitonViewModel()
            {
                ExhibitionId = x.ExhibitionId,
                ExhibitionStartTime = x.ExhibitionStartTime.ToString("yyyy/MM/dd"),
                ExhibitionEndTime = x.ExhibitionEndTime.ToString("yyyy/MM/dd"),
                ExhibitionIntro = x.ExhibitionIntro,
                MasterUnit = x.MasterUnit,
                ExhibitionPrice = x.ExhibitionPrice,
                ExCustomerId = x.ExCustomerId,
                ExPhoto = x.ExPhoto,
                ExName = x.ExName,
                ReviewState = x.ReviewState

            }).ToList();
            return result;
        }

        public BaseModel.BaseResult<List<RentalViewModel>> RentalGetAll()
        {
            var result = new BaseModel.BaseResult<List<RentalViewModel>>();
            result.Body = (from o in _dbRepository.GetAll<ExhibitionOrder>()
                           join c in _dbRepository.GetAll<ExhibitionCustomer>()
                           on o.ExCustomerId equals c.ExCustomerId
                           join e in _dbRepository.GetAll<Exhibition>()
                           on o.ExCustomerId equals e.ExCustomerId
                           select new RentalViewModel()
                           {
                               ExOrderId = o.ExOrderId,
                               StartDate = o.StartDate,
                               EndDate = o.EndDate,
                               Price = o.Price,
                               PaymentState = o.PaymentState,
                               ExCustomerName = c.ExCustomerName,
                               ExCustomerPhone = c.ExCustomerPhone,
                               ExCustomerEmail = c.ExCustomerEmail,
                              
                           }).ToList();
            
            return result;
        }

        public string Send(string sender, string recipient, string subject, string body)
        {
            try
            {
                string id = _config["Gmail:Id"];
                string password = _config["Gmail:Password"];
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(id, password),
                    EnableSsl = true
                };
                client.Send(sender, recipient, subject, body);
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
            
            return "OK";
        }
    }
}
