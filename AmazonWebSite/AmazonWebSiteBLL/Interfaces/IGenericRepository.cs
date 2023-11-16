using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteBLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        public T GetById(int id);
        public List<T> GetAll();
        public int Add(T Entity);
        public int Update(T Entity);
        public int Delete(T Entity);


    }
}
