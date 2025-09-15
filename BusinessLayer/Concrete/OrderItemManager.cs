using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class OrderItemManager : IOrderItemService
    {
        IOrderItemDal _orderItemDal;

        public OrderItemManager(IOrderItemDal orderItemDal)
        {
            _orderItemDal = orderItemDal;
        }

        public void Delete(OrderItem t)
        {
            _orderItemDal.Delete(t);
        }

        public OrderItem GetById(int id)
        {
            return _orderItemDal.GetById(id);
        }

        public List<OrderItem> GetList()
        {
            return _orderItemDal.GetList();
        }

        public void Insert(OrderItem t)
        {
            _orderItemDal.Insert(t);
        }

        public void Update(OrderItem t)
        {
            _orderItemDal.Update(t);
        }
    }
}
