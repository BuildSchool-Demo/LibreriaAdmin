using LibreriaAdmin.Interfaces;
using LibreriaAdmin.Models;
using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IRepository _repository;
        public ManagerService(IRepository repository)
        {
            _repository = repository;
        }

        public ManagerViewModel.ManagerListResult GetAllManagers()
        {
            var result = new ManagerViewModel.ManagerListResult();
            result.ManagerList = _repository.GetAll<Manager>()
                .Select(x => new ManagerViewModel.ManagerSingleResult()
                {
                    ManagerName = x.ManagerName,
                    ManagerPassword = x.ManagerPassword,
                    ManagerUserName = x.ManagerUsername,
                    ManagerRoleID = x.ManagerRoleId

                }).ToList();
            return result;
        }
      
        public BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult> GetManager(ManagerViewModel.ManagerSingleResult ManagerVM)
        {
            var result = new BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult>();
            var manager = _repository.GetAll<Manager>().Where(x => x.ManagerName == ManagerVM.ManagerName).FirstOrDefault();
            try
            {
                if (manager != null)
                {
                    result.IsSuccess = true;

                }
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
            }

            return result;
        }
    }
}
