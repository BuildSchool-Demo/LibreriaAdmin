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
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using System.Net.Http;
using System.IO;

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

        public string Send(string sender, string recipient, string subject, string body,int Exid)
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
                client.Send(sender, recipient, subject,"您好" + Environment.NewLine + body + Environment.NewLine + "麻煩請點選以下網址，進行展覽內容修改或確認，謝謝!" + Environment.NewLine + $"https://libreriaadmin.azurewebsites.net/Exhibiton/Email?exhibitionId={Exid}");
                return "信件已寄出";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
            
            
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


        public ExhibitonEmailViewModel.EmailListResult EmailGetAll(int exhibitonId)
        {
            var result = new ExhibitonEmailViewModel.EmailListResult();
            result.EmailList = (from e in _dbRepository.GetAll<Exhibition>()
                      join c in _dbRepository.GetAll<ExhibitionCustomer>()
                      on e.ExCustomerId equals c.ExCustomerId
                      join o in _dbRepository.GetAll<ExhibitionOrder>()
                      on e.ExCustomerId equals o.ExCustomerId
                      where e.ExhibitionId == exhibitonId
                      select new ExhibitonEmailViewModel.EmailSingleResult()
                      {
                          ExhibitionId = e.ExhibitionId,
                          ExhibitionStartTime = e.ExhibitionStartTime.ToString("yyyy/MM/dd"),
                          ExhibitionEndTime = e.ExhibitionEndTime.ToString("yyyy/MM/dd"),
                          ExhibitionIntro = e.ExhibitionIntro,
                          MasterUnit = e.MasterUnit,
                          ExhibitionPrice = e.ExhibitionPrice,
                          ExCustomerId = e.ExCustomerId,
                          ExPhoto = e.ExPhoto,
                          ExName = e.ExName,
                          ReviewState = e.ReviewState,
                          ExCustomerName = c.ExCustomerName,
                          ExCustomerPhone = c.ExCustomerPhone,
                          ExCustomerEmail = c.ExCustomerEmail,
                          CustomerVerify = o.CustomerVerify,
                          RentalDate = new ExhibitonEmailViewModel.RentalDateModel()
                          {
                              StartDate = o.StartDate.ToString("yyyy/MM/dd"),
                              EndDate = o.EndDate.ToString("yyyy/MM/dd")
                          }
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
                                      EditModifyDate = x.EditModifyDate


                                  }).ToList();
            return result;
        }
     
        public bool ConfirmEmail(ExhibitonEmailViewModel.EmailSingleResult ExVM)
        {

            var exhibition = _dbRepository.GetAll<Exhibition>().First(x => x.ExhibitionId == ExVM.ExhibitionId);
            var exhibitionOrder = _dbRepository.GetAll<ExhibitionOrder>().First(x => x.ExCustomerId == ExVM.ExCustomerId);

            exhibition.ReviewState = ExVM.ReviewState;
            _dbRepository.Update(exhibition);
            exhibitionOrder.CustomerVerify = ExVM.CustomerVerify;
            _dbRepository.Update(exhibitionOrder);

            return true;
        }

        public async Task<bool> ModifyExhibition(ExhibitonEmailViewModel.ModifyExhibitionModel ExVM)
        {
            //圖片上傳至imgur並取得圖片網址
            string imageUrl = null;
            if (ExVM.ExPhoto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ExVM.ExPhoto.CopyToAsync(memoryStream);
                    var apiClient = new ApiClient("8b8585e4ec973fc");
                    var httpClient = new HttpClient();
                    var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
                    var imageUpload = await imageEndpoint.UploadImageAsync((Stream)ExVM.ExPhoto);
                    imageUrl = imageUpload.Link;
                }
                   
            }

            var exhibition = _dbRepository.GetAll<Exhibition>().First(x => x.ExhibitionId == ExVM.ExhibitionId);
            var exhibitionCustomer = _dbRepository.GetAll<ExhibitionCustomer>().First(x => x.ExCustomerId == ExVM.ExCustomerId);
            
            exhibition.ExhibitionStartTime = DateTime.ParseExact(ExVM.ExhibitionStartTime, "yyyy/MM/dd", null);
            exhibition.ExhibitionEndTime = DateTime.ParseExact(ExVM.ExhibitionEndTime, "yyyy/MM/dd", null);
            exhibition.ExhibitionIntro = ExVM.ExhibitionIntro;
            exhibition.MasterUnit = ExVM.MasterUnit;
            exhibition.ExhibitionPrice = ExVM.ExhibitionPrice;
            exhibition.ExPhoto = imageUrl;
            exhibition.ExName = ExVM.ExName;
            exhibition.ReviewState = ExVM.ReviewState;
            _dbRepository.Update(exhibition);

            exhibitionCustomer.ExCustomerName = ExVM.ExCustomerName;
            exhibitionCustomer.ExCustomerPhone = ExVM.ExCustomerPhone;
            exhibitionCustomer.ExCustomerEmail = ExVM.ExCustomerEmail;
            _dbRepository.Update(exhibitionCustomer);

            return true;
        }

        //public ExhibitonEmailViewModel.EmailListResult GetRentalDate(int exhibitionId)
        //{
        //    var result = new ExhibitonEmailViewModel.EmailListResult();
        //    result.RentalDate = (from o in _dbRepository.GetAll<ExhibitionOrder>()
        //                            join c in _dbRepository.GetAll<ExhibitionCustomer>()
        //                            on o.ExCustomerId equals c.ExCustomerId
        //                            join e in _dbRepository.GetAll<Exhibition>()
        //                            on o.ExCustomerId equals e.ExCustomerId
        //                            where e.ExhibitionId == exhibitionId
        //                            select new RentalViewModel.GetRentalDate
        //                            {
        //                                StartDate = o.StartDate.ToString("yyyy/MM/dd"),
        //                                EndDate = o.EndDate.ToString("yyyy/MM/dd")
        //                            }).ToList();

        //    return result;
        //}
    }
}
