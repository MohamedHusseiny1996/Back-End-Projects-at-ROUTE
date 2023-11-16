using Department_project_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deapartment_project_BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        public T GetById(int id);
        public IEnumerable<T> GetAll();
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public IEnumerable<T> Search(string SearchValue);
    }
}
