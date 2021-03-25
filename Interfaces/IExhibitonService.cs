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
        string Send(string sender, string recipient, string subject, string body,int id);

        /// <summary>
        /// 取得展覽資料、顧客資料、租借日期
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


        ExhibitonViewModel.ExhibitonListResult GetTodayExhibiton();

      
        /// <summary>
        /// 客戶回覆資料確認，修改審核狀態及客戶驗證
        /// </summary>
        /// <param name="ExVM"></param>
        /// <returns></returns>
        bool ConfirmEmail(ExhibitonEmailViewModel.EmailSingleResult ExVM);

        /// <summary>
        /// 儲存修改展覽資料
        /// </summary>
        /// <param name="ExVM"></param>
        /// <returns></returns>
        Task<bool> ModifyExhibition(ExhibitonEmailViewModel.ModifyExhibitionModel ExVM);

        /// <summary>
        /// 取得租借日期
        /// </summary>
        /// <param name="exhibitionId"></param>
        /// <returns></returns>
        //RentalViewModel.RentalListResult GetRentalDate(int exhibitionId);
    }
}
