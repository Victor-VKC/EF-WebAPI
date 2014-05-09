using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Server.Entity;
using Server.Services.DAL;
using OrderHeader = Data.Models.SalesOrderHeader;
using OrderItem = Data.Models.SalesOrderDetail;

namespace Server.Services
{
    public class SalesOrderService
    {
        private readonly IRepository<SalesOrderHeader> _orderHeaderRepository;
        private readonly IRepository<SalesOrderDetail> _orderItemsRepository;

        public SalesOrderService() : this(new Repository<SalesOrderHeader>(), new Repository<SalesOrderDetail>())
        {}

        public SalesOrderService(IRepository<SalesOrderHeader> ordersRepository, IRepository<SalesOrderDetail> itemsRepository)
        {
            _orderHeaderRepository = ordersRepository;
            _orderItemsRepository = itemsRepository;
        }

        public int CreateOrder(int customerId)
        {
            var number = _orderHeaderRepository.GetAll().Max(item => item.SalesOrderID) + 1;
            _orderHeaderRepository.Add(new SalesOrderHeader()
            {
                SalesOrderID = number,
                CustomerID = customerId,
                ModifiedDate = DateTime.Now,
                OrderDate = DateTime.Now,
                OnlineOrderFlag = false,
                DueDate = DateTime.Now,
                PurchaseOrderNumber = String.Format("SO#{0}", number),
                Status = 0,
                rowguid = Guid.NewGuid()
            });
            _orderHeaderRepository.Commit();
            return number;
        }

        public void AddProduct(int orderId, int productId, short quantity)
        {
            var price = new Repository<Product>().Get(item => item.ProductID == productId).ListPrice;
            var id = _orderItemsRepository.GetAll().Max(item => item.SalesOrderDetailID) + 1;
            _orderItemsRepository.Add(new SalesOrderDetail()
            {
                SalesOrderDetailID = id, 
                LineTotal = quantity,
                ModifiedDate = DateTime.Now,
                OrderQty = quantity,
                ProductID = productId,
                rowguid = Guid.NewGuid(),
                SalesOrderID = orderId,
                UnitPrice = price
            });
            _orderItemsRepository.Commit();
        }

        public void DelProduct(int orderId, int productId)
        {
            _orderItemsRepository.Delete(item => item.SalesOrderID == orderId && item.ProductID == productId);
            _orderItemsRepository.Commit();
        }

        public void SetStatus(int orderId, byte status)
        {
            var order = _orderHeaderRepository.Get(item => item.SalesOrderID == orderId);
            order.Status = status;
            _orderHeaderRepository.Update(order);
            _orderItemsRepository.Commit();
        }
    }
}
