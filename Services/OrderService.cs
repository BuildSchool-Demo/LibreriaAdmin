using LibreriaAdmin.Models;
using LibreriaAdmin.Repository;
using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibreriaAdmin.Interfaces;
using AutoMapper;
using LibreriaAdmin.Mappings;

namespace LibreriaAdmin.Services
{
    public class OrderService
    {
        private readonly LibreriaRepository _dbRepository;

        private IMapper _mapper;

        public OrderService()
        {
            _dbRepository = new LibreriaRepository();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMappings>());
            this._mapper = config.CreateMapper();
        }

        public BaseModel.BaseResult<List<OrderViewModel.OrderSingleResult>> GetAll()
        {
            var data = _dbRepository.GetAll<Order>();

            var resultVMs = this._mapper.Map<IEnumerable<OrderViewModel.OrderSingleResult>>(data).ToList();
            var result = new BaseModel.BaseResult<List<OrderViewModel.OrderSingleResult>>();
            result.Body = resultVMs;

            return result;
        }

    }
}
