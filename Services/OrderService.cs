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
            //放入OrderVMList
            List<OrderViewModel.OrderSingleResult> OrderVMList;
            OrderVMList = (from Order in _dbRepository.GetAll<Order>()
                           join Member in _dbRepository.GetAll<Member>()
                           on Order.MemberId equals Member.MemberId
                           orderby Order.OrderId descending
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

            OrderDetailViewModel.OrderListResult OrderDetailVMList = GetAllOrderDetail();

            foreach (var OrderVM in OrderVMList)
            {
                OrderVM.OrderDetailList =
                    OrderDetailVMList.OrderDetailList
                    .Where(OrderDetail => OrderDetail.OrderId == OrderVM.OrderId)
                    .ToList();
            }

            var result = new OrderViewModel.OrderListResult();
            result.OrderList = OrderVMList;



            return result;
        }


        public OrderDetailViewModel.OrderListResult GetAllOrderDetail()
        {
            //放入OrderVMList
            List<OrderDetailViewModel.OrderSingleResult> OrderDetailVMList;
            OrderDetailVMList = (
                from OrderDetail in _dbRepository.GetAll<OrderDetail>()
                    //join Order in _dbRepository.GetAll<Order>()
                    //on OrderDetail.OrderId equals Order.OrderId
                join Product in _dbRepository.GetAll<Product>()
                on OrderDetail.ProductId equals Product.ProductId
                //orderby Order.OrderDate descending
                select new OrderDetailViewModel.OrderSingleResult()
                {
                    OrderDetailId = OrderDetail.OrderDetailId,
                    OrderId = OrderDetail.OrderId, //join OrderId
                    Quantity = OrderDetail.Quantity,
                    ProductId = OrderDetail.ProductId, //join Product
                    ProductName = Product.ProductName,
                    UnitPrice = Product.UnitPrice,
                }).ToList();

            var result = new OrderDetailViewModel.OrderListResult();
            result.OrderDetailList = OrderDetailVMList;



            return result;
        }










        public OrderViewModel.OrderListResult GetTodayOrderPrice()
        {

            var today = DateTime.Now.Day;
            //放入OrderVMList
            List<OrderViewModel.OrderSingleResult> OrderVMList;
            OrderVMList = (from Order in _dbRepository.GetAll<Order>().Where(x => x.CreateTime.Day == today)
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

            OrderDetailViewModel.OrderListResult OrderDetailVMList = GetAllOrderDetail();

            foreach (var OrderVM in OrderVMList)
            {
                OrderVM.OrderDetailList =
                    OrderDetailVMList.OrderDetailList
                    .Where(OrderDetail => OrderDetail.OrderId == OrderVM.OrderId)
                    .ToList();
            }

            var result = new OrderViewModel.OrderListResult();
            result.OrderList = OrderVMList;



            return result;
        }


        public bool Edit(OrderViewModel.OrderSingleResult orderVM)
        {
            foreach(var orderDetailVM in orderVM.OrderDetailList)
            {
                OrderDetail orderDetail = _dbRepository.GetAll<OrderDetail>().FirstOrDefault(orderDetail => 
                    orderDetail.OrderDetailId == orderDetailVM.OrderDetailId);
                orderDetail.Quantity = orderDetailVM.Quantity;
                _dbRepository.Update(orderDetail);
            }

            Order order = _dbRepository.GetAll<Order>().FirstOrDefault(order => order.OrderId == orderVM.OrderId);
            order.ShipName = orderVM.ShipName;
            order.ShipCity = orderVM.ShipCity;
            order.ShipRegion = orderVM.ShipRegion;
            order.ShipAddress = orderVM.ShipAddress;
            order.ShipPostalCode = orderVM.ShipPostalCode;
            order.InvoiceType = orderVM.InvoiceType;
            order.InvoiceInfo = orderVM.InvoiceInfo;
            order.UpdateTime = DateTime.UtcNow.AddHours(8);
            order.PaymentType = orderVM.PaymentType;
            order.PaymentState = orderVM.PaymentState;

            _dbRepository.Update(order);
            return true;
        }




    }
}
