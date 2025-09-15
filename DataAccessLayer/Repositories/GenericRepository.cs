using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class, new()
    {
        //PetShopContext Context = new();
        public void Delete(T t)
        {
            using var Context = new PetShopContext();
            Context.Remove(t);
            Context.SaveChanges();
        }

        public T GetById(int id)
        {
            using var Context = new PetShopContext();
            return Context.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            using var Context = new PetShopContext();
            return Context.Set<T>().ToList();
        }

        public void Insert(T t)
        {
            using var Context = new PetShopContext();
            Context.Add(t);
            Context.SaveChanges();
        }

        public void Update(T t)
        {
            using var Context = new PetShopContext();
            Context.Update(t);
            Context.SaveChanges();
        }
    }
}
