using LibreriaAdmin.Interfaces;
using LibreriaAdmin.Models;
using LibreriaAdmin.Repository;
using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Services
{
    public class MemberService : IMemberService
    {
        private readonly IRepository _dbRepository;

        public MemberService(IRepository repository)
        {
            _dbRepository = repository;
        }

        public MemberViewModel.MemberListResult GetAll()
        {
            try
            {
                var result = new MemberViewModel.MemberListResult();

                var source = _dbRepository.GetAll<Member>().AsEnumerable();
                var orders = _dbRepository.GetAll<Order>().AsEnumerable();
                var products = _dbRepository.GetAll<Product>().AsEnumerable();
                var orderDetails = _dbRepository.GetAll<OrderDetail>().AsEnumerable().Select(x => new
                {
                    OrderDetailId = x.OrderDetailId,
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    Price = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.UnitPrice ?? 0,
                    Quantity = x.Quantity
                }).ToList();

                result.MemberList = (from member in source

                                     let orderList = orders.Where(x => x.MemberId == member.MemberId).ToList()
                                     let orderDetailList = orderDetails.Where(x => orderList.Select(y => y.OrderId).Contains(x.OrderId)).ToList()
                                     select new MemberViewModel.MemberSingleResult()
                                     {
                                         memberId = member.MemberId,
                                         memberUserName = member.MemberUserName,
                                         mobileNumber = member.MobileNumber,
                                         homeNumber = member.HomeNumber,
                                         address = member.Address,
                                         email = member.Email,
                                         memberName = member.MemberName,
                                         memberPassword = member.MemberPassword,
                                         birthday = member.Birthday,
                                         gender = member.Gender,
                                         idnumber = member.Idnumber,
                                         ordersum = orderDetailList.Sum(y => y.Price * y.Quantity)
                                     }
                                     ).ToList();





                    //source.Select(x => new MemberViewModel.MemberSingleResult()
                    // {
                    //     memberId = x.MemberId,
                    //     memberUserName = x.MemberUserName,
                    //     mobileNumber = x.MobileNumber,
                    //     homeNumber = x.HomeNumber,
                    //     address = x.Address,
                    //     email = x.Email,
                    //     memberName = x.MemberName,
                    //     memberPassword = x.MemberPassword,
                    //     birthday = x.Birthday,
                    //     gender = x.Gender,
                    //     idnumber = x.Idnumber,
                    //     ordersum = 0
                    // }).ToList();


                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                return new MemberViewModel.MemberListResult(); ;
            }
            
        }

        public OrderViewModel.OrderListResult GetByMemberId(int id)
        {
            List<OrderViewModel.OrderSingleResult> OrderVMList;
            OrderVMList = (from Order in _dbRepository.GetAll<Order>().Where(x => x.MemberId == id)
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
    }
}
