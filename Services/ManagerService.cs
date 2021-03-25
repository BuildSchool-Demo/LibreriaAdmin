using LibreriaAdmin.Interfaces;
using LibreriaAdmin.Models;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
                    ManagerID = x.ManagerId,
                    ManagerName = x.ManagerName,
                    ManagerPassword = x.ManagerPassword,
                    ManagerUserName = x.ManagerUsername,
                    ManagerRoleID = x.ManagerRoleId

                }).ToList();
            return result;
        }
      
        public BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult> GetManagerAuthentication(LoginViewModel loginVM)
        {
            var result = new BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult>();
            var manager = _repository.GetAll<Manager>().Where(x => x.ManagerName == loginVM.Username && x.ManagerPassword == loginVM.Password).FirstOrDefault();
            try
            {
                if (manager != null)
                {
                    result.IsSuccess = true;
                    result.Msg = manager.ManagerRoleId.ToString();

                }
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
                ex.ToString();
            }

            return result;
        }
        public BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult> CreateManager([FromBody] ManagerViewModel.ManagerSingleResult manager)
        {
            var result = new BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult>();
            Manager newManager = null;

            newManager = new Manager
            {
                ManagerName = manager.ManagerName,
                ManagerPassword = manager.ManagerPassword,
                ManagerUsername = manager.ManagerUserName,
                ManagerRoleId = manager.ManagerRoleID
            };

            try
            {
                _repository.Create<Manager>(newManager);
                if (newManager  != null)
                {
                    result.IsSuccess = true;

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result.IsSuccess = false;
            }
            return result;
        }

        public BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult> DeleteManager(int id)
        {
            var result = new BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult>();

            var targetmanager = _repository.GetAll<Manager>().Where(x => x.ManagerId == id).FirstOrDefault();          


            try
            {
                _repository.Delete<Manager>(targetmanager);
                if (targetmanager != null)
                {
                    result.IsSuccess = true;

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result.IsSuccess = false;
            }
            return result;
        }

        public BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult> EditManager(int id, [FromBody] ManagerViewModel.ManagerSingleResult manager)
        {
            var result = new BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult>();

            var targetmanager = _repository.GetAll<Manager>().Where(x => x.ManagerId == id).FirstOrDefault();

            targetmanager.ManagerId = id;
            targetmanager.ManagerName = manager.ManagerName;
            targetmanager.ManagerPassword = manager.ManagerPassword;
            targetmanager.ManagerRoleId = manager.ManagerRoleID;
            targetmanager.ManagerUsername = manager.ManagerUserName;

            try
            {
                _repository.Update<Manager>(targetmanager);
                if (targetmanager != null)
                {
                    result.IsSuccess = true;

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result.IsSuccess = false;
            }
            return result;
        }
    }
}
