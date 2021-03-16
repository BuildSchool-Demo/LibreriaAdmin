using LibreriaAdmin.Models;
using LibreriaAdmin.Repository;
using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibreriaAdmin.Interfaces;


namespace LibreriaAdmin.Services
{
    public class OrderService
    {
        private readonly LibreriaRepository _dbRepository;

        public OrderService()
        {
            _dbRepository = new LibreriaRepository();
        }
        public BaseModel.BaseResult<List<OrderViewModel.OrderSingleResult>> GetAll()
        {
            var result = new BaseModel.BaseResult<List<OrderViewModel.OrderSingleResult>>();
            
            result.Body = _dbRepository.GetAll<Order>().Select(x => new OrderViewModel.OrderSingleResult()
            {
                OrderId=x.OrderId,
                ShippingDate=x.ShippingDate,
                OrderDate=x.OrderDate,
                MemberId=x.MemberId,
                ShipName=x.ShipName,
                ShipCity=x.ShipCity,
                ShipRegion=x.ShipRegion,
                ShipAddress=x.ShipAddress,
                ShipPostalCode=x.ShipPostalCode,
                InvoiceType=x.InvoiceType,
                InvoiceInfo=x.InvoiceInfo,
                CreateTime=x.CreateTime,
                UpdateTime=x.UpdateTime,
                PaymentType=x.PaymentType,
                PaymentState=x.PaymentState,
                Member=x.Member,
                OrderDetails=x.OrderDetails

            }).ToList();
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
