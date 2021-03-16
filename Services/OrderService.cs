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
    public class OrderService : IOrderService
    {
        private readonly LibreriaRepository _dbRepository;

        private IMapper _mapper;

        public OrderService()
        {
            _dbRepository = new LibreriaRepository();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMappings>());
            this._mapper = config.CreateMapper();
        }

        public OrderViewModel.OrderListResult GetAll()
        {
            var data = _dbRepository.GetAll<Order>();

            var resultVMs = this._mapper.Map<IEnumerable<OrderViewModel.OrderSingleResult>>(data).ToList();
            var result = new OrderViewModel.OrderListResult();
            result.OrderList = resultVMs;

            return result;
        }
        public OrderViewModel.OrderListResult GetByTime(OrderViewModel.OrderDateResult request)
        {
            OrderViewModel.OrderListResult result = new OrderViewModel.OrderListResult();
            var nowMonth = DateTime.Now.Month;
            var monthNum = nowMonth - 2;

            var data = _dbRepository.GetAll<Order>().Where(x => x.OrderDate.Month == monthNum);
            var resultVMs = this._mapper.Map<IEnumerable<OrderViewModel.OrderSingleResult>>(data).ToList();
            result.OrderList = resultVMs;

            return result;
        }

    }
}
