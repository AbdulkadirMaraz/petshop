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
    public class CartItemManager : ICartItemService
    {
        ICartItemDal _cartItemDal;

        public CartItemManager(ICartItemDal cartItemDal)
        {
            _cartItemDal = cartItemDal;
        }

        public void Delete(CartItem t)
        {
            _cartItemDal.Delete(t);
        }

        public CartItem GetById(int id)
        {
            return _cartItemDal.GetById(id);
        }

        public List<CartItem> GetList()
        {
            return _cartItemDal.GetList();
        }

        public void Insert(CartItem t)
        {
            _cartItemDal.Insert(t);
        }

        public void Update(CartItem t)
        {
            _cartItemDal.Update(t);
        }
    }
}
