using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Interfaces
{
    public interface IManagerService
    {
        /// <summary>
        /// 取得所有管理者
        /// </summary>
        /// <returns></returns>
        ManagerViewModel.ManagerListResult GetAllManagers();
        /// <summary>
        /// 取得單一管理者
        /// </summary>
        /// <returns></returns>
        BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult> GetManager(int managerID);
    }
}
