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
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void Delete(Order t)
        {
            _orderDal.Delete(t);
        }

        public Order GetById(int id)
        {
           return _orderDal.GetById(id);
        }

        public List<Order> GetList()
        {
            return _orderDal.GetList();
        }

        public void Insert(Order t)
        {
            _orderDal.Insert(t);
        }

        public void Update(Order t)
        {
            _orderDal.Update(t);
        }
    }
}
