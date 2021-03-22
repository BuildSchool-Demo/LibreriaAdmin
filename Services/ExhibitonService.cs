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
        private readonly IRepository _dbRepository;
        private readonly IConfiguration _config;

        public ExhibitonService(IConfiguration config, IRepository repository)
        {
            _dbRepository = repository;
            _config = config;

        }

        public ExhibitonViewModel.ExhibitonListResult ExhibitonGetAll()
        {
            var result = new ExhibitonViewModel.ExhibitonListResult();

            result.ExhibitonList = _dbRepository.GetAll<Exhibition>()
                                  .Select(x => new ExhibitonViewModel.ExhibitonSingleResult()
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

        public RentalViewModel.RentalListResult RentalGetAll()
        {
            var result = new RentalViewModel.RentalListResult();
            result.RentalList = (from o in _dbRepository.GetAll<ExhibitionOrder>()
                      join c in _dbRepository.GetAll<ExhibitionCustomer>()
                      on o.ExCustomerId equals c.ExCustomerId
                      join e in _dbRepository.GetAll<Exhibition>()
                      on o.ExCustomerId equals e.ExCustomerId
                      select new RentalViewModel.RentalSingleResult()
                      {
                          ExOrderId = o.ExOrderId,
                          StartDate = o.StartDate,
                          EndDate = o.EndDate,
                          Price = o.Price,
                          PaymentState = o.PaymentState,
                          ExCustomerName = c.ExCustomerName,
                          ExCustomerPhone = c.ExCustomerPhone,
                          ExCustomerEmail = c.ExCustomerEmail,
                          ExhibitonData = new RentalViewModel.ExhibitonDataModel
                          {
                              ExName = e.ExName,
                              ExhibitionStartTime = e.ExhibitionStartTime.ToString("yyyy/MM/dd"),
                              ExhibitionEndTime = e.ExhibitionEndTime.ToString("yyyy/MM/dd")
                          }
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
                client.Send(sender, recipient, subject, body + " " + Environment.NewLine + _config["MailCheckUrl"]);
                return "信件已寄出";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
            
            return "信件尚未寄出";
        }

        public ExhibitonSendMailViewModel.GetByCustomerEmailRequest GetCustomerData(int exhibitionId)
        {
            var result = (from c in _dbRepository.GetAll<ExhibitionCustomer>()
                        join e in _dbRepository.GetAll<Exhibition>()
                        on c.ExCustomerId equals e.ExCustomerId
                        where e.ExhibitionId == exhibitionId
                        select new ExhibitonSendMailViewModel.GetByCustomerEmailRequest()
                        {
                            exCustomerEmail = c.ExCustomerEmail,
                            customerName = c.ExCustomerName
                        }).FirstOrDefault();

            return result;
        }


        public ExhibitonEmailViewModel.EmailListResult EmailGetAll(int id)
        {
            var result = new ExhibitonEmailViewModel.EmailListResult();
            result.EmailList = (from e in _dbRepository.GetAll<Exhibition>()
                      join c in _dbRepository.GetAll<ExhibitionCustomer>()
                      on e.ExCustomerId equals c.ExCustomerId
                      where (e.ExhibitionId == id)
                      select new ExhibitonEmailViewModel.EmailSingleResult()
                      {
                          ExhibitionId = e.ExhibitionId,
                          ExhibitionStartTime = e.ExhibitionStartTime,
                          ExhibitionEndTime = e.ExhibitionEndTime,
                          ExhibitionIntro = e.ExhibitionIntro,
                          MasterUnit = e.MasterUnit,
                          ExhibitionPrice = e.ExhibitionPrice,
                          EditModifyDate = e.EditModifyDate,
                          ExCustomerId = e.ExCustomerId,
                          ExPhoto = e.ExPhoto,
                          ExName = e.ExName,
                          ReviewState = e.ReviewState,
                          ExCustomerName = c.ExCustomerName,
                          ExCustomerPhone = c.ExCustomerPhone,
                          ExCustomerEmail = c.ExCustomerEmail,
                      }).ToList();

            return result;
        }
        public ExhibitonViewModel.ExhibitonListResult GetTodayExhibiton()
        {
            var result = new ExhibitonViewModel.ExhibitonListResult();
            var today = DateTime.Now.Day;
            result.ExhibitonList = _dbRepository.GetAll<Exhibition>().Where(x => x.EditModifyDate.Day == today)
                                  .Select(x => new ExhibitonViewModel.ExhibitonSingleResult()
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
                                      ReviewState = x.ReviewState,
                                      EditModifyDate =x.EditModifyDate


                                  }).ToList();
            return result;
        }

    }
}
