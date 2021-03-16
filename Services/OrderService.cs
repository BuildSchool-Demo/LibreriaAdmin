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

        public BaseModel.BaseResult<OrderViewModel.OrderListResult> GetAll()
        {
            var data = _dbRepository.GetAll<Order>();

            var resultVMs = this._mapper.Map<IEnumerable<OrderViewModel.OrderSingleResult>>(data).ToList();
            var result = new BaseModel.BaseResult<OrderViewModel.OrderListResult>();
            result.Body.OrderList = resultVMs;

            return result;
        }
        public BaseModel.BaseResult<OrderViewModel.OrderListResult> GetByTime(OrderViewModel.OrderDateResult request)
        {
            BaseModel.BaseResult<OrderViewModel.OrderListResult> result = new BaseModel.BaseResult<OrderViewModel.OrderListResult>();
            var nowMonth = DateTime.Now.Month;
            var monthNum = nowMonth - 2;
            var OrderTimeList = _dbRepository.GetAll<Order>().Where(x => x.OrderDate.Month == monthNum).Select(x => new OrderViewModel.OrderSingleResult()
            {
                OrderId = x.OrderId,
                ShippingDate = x.ShippingDate,
                OrderDate = x.OrderDate,
                MemberId = x.MemberId,
                ShipName = x.ShipName,
                ShipCity = x.ShipCity,
                ShipRegion = x.ShipRegion,
                ShipAddress = x.ShipAddress,
                ShipPostalCode = x.ShipPostalCode,
                InvoiceType = x.InvoiceType,
                InvoiceInfo = x.InvoiceInfo,
                CreateTime = x.CreateTime,
                UpdateTime = x.UpdateTime,
                PaymentType = x.PaymentType,
                PaymentState = x.PaymentState,
                Member = x.Member,
                OrderDetails = x.OrderDetails
            }).ToList();
            result.Body = new OrderViewModel.OrderListResult()
            {
                OrderList = OrderTimeList
            };
            return result;
        }

    }
}
