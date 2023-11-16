using AmazonWebSiteBLL.Interfaces;
using AmazonWebSiteDAL.Contexts;
using AmazonWebSiteDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteBLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>where T : class
    {
        public AmazonContext _Context { get; set; }
        public GenericRepository(AmazonContext context)
        {
            _Context = context;
        }

        

        public int Add(T Entity)
        {
            _Context.Add(Entity);
            return _Context.SaveChanges();
        }

        public int Delete(T Entity)
        {
            _Context.Remove(Entity);
            return _Context.SaveChanges();
        }

        public List<T> GetAll()
        {
          return _Context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            if(typeof(T)==typeof(Items))
            {
                 return (T)(dynamic)_Context.Items.Include(x=>x.image).Where(x=>x.Id==id).FirstOrDefault();

            }
            else if (typeof(T) == typeof(Image))
            {
                return  (T)(dynamic)(_Context.Images.Include(x => x.Item).Where(x => x.Id == id).FirstOrDefault());

            }
            return _Context.Set<T>().Find(0);//default to handle this without speficification design
        }

        public int Update(T Entity)
        {
            
            _Context.Update(Entity);
            return _Context.SaveChanges();
        }
    }
}
