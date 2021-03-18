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
        private readonly IRepository _dbRepository;

        private IMapper _mapper;

        public OrderService(IRepository repository)
        {
            _dbRepository = repository;

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMappings>());
            this._mapper = config.CreateMapper();
        }

        public OrderViewModel.OrderListResult GetAll()
        {
            IEnumerable<OrderViewModel.OrderSingleResult> DataSets; //repository抓出的資料

            //抓取所有訂單
            var OrderDataSets = _dbRepository.GetAll<Order>().OrderByDescending(order => order.OrderDate);
            DataSets = this._mapper.Map<IEnumerable<OrderViewModel.OrderSingleResult>>(OrderDataSets);

            //抓取所有會員
            var MemberSets = _dbRepository.GetAll<Member>();




            //放入OrderVMList
            List<OrderViewModel.OrderSingleResult> OrderVMList;
            OrderVMList = (from Order in _dbRepository.GetAll<Order>()
                           join Member in _dbRepository.GetAll<Member>()
                           on Order.MemberId equals Member.MemberId
                           orderby Order.OrderDate descending
                           select new OrderViewModel.OrderSingleResult()
                           {
                               OrderId = Order.OrderId,
                               ShippingDate = Order.ShippingDate,
                               OrderDate = Order.OrderDate,
                               MemberId = Order.MemberId,
                               MemberUserName = Member.MemberUserName,
                               ShipName = Order.ShipName,
                               ShipCity = Order.ShipCity,
                               ShipRegion = Order.ShipRegion,
                               ShipAddress = Order.ShipAddress,
                               ShipPostalCode = Order.ShipPostalCode,
                               InvoiceType = Order.InvoiceType,
                               InvoiceInfo = Order.InvoiceInfo,
                               CreateTime = Order.CreateTime,
                               UpdateTime = Order.UpdateTime,
                               PaymentType = Order.PaymentType,
                               PaymentState = Order.PaymentState,
                           }).ToList();

            var result = new OrderViewModel.OrderListResult();
            result.OrderList = OrderVMList;



            return result;
        }
        public OrderViewModel.OrderListResult Getbytoday()
        {
            OrderViewModel.OrderListResult result = new OrderViewModel.OrderListResult();
            var nowday = DateTime.Now.Day;

            var data = _dbRepository.GetAll<Order>().Where(x => x.OrderDate.Day == nowday);
            var resultVMs = this._mapper.Map<IEnumerable<OrderViewModel.OrderSingleResult>>(data).ToList();
            result.OrderList = resultVMs;

            return result;
        }

    }
}
