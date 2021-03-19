using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibreriaAdmin.ViewModels;

namespace LibreriaAdmin.Interfaces
{
    public interface IExhibitonService
    {
        /// <summary>
        /// 取的所有展覽資料
        /// </summary>
        /// <returns></returns>
        ExhibitonViewModel.ExhibitonListResult ExhibitonGetAll();

        /// <summary>
        /// 取得所有租借訂單內容及顧客資料
        /// </summary>
        /// <returns></returns>
        RentalViewModel.RentalListResult RentalGetAll();

        /// <summary>
        /// 寄信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        string Send(string sender, string recipient, string subject, string body);

        /// <summary>
        /// 取得展覽資料及顧客資料
        /// </summary>
        /// <param name="exhibitonId"></param>
        /// <returns></returns>
        ExhibitonEmailViewModel.EmailListResult EmailGetAll(int exhibitonId);

        /// <summary>
        /// 取得客戶Email
        /// </summary>
        /// <param name="exhibitonId"></param>
        /// <returns></returns>
        ExhibitonSendMailViewModel.GetByCustomerEmailRequest GetCustomerData(int exhibitonId);
    }
}
