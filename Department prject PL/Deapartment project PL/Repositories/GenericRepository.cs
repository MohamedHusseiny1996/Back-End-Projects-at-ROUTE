using Deapartment_project_BLL.Interfaces;
using Department_project_DAL.Contexts;
using Department_project_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deapartment_project_BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public CompanyContext _Context { get; set; }
        public GenericRepository(CompanyContext context)
        {
            _Context = context;
        }

       

        public void Create(T entity)
        => _Context.Add(entity);
           
        

        public void Delete(T entity)
       => _Context.Remove(entity);
           
        

        public IEnumerable<T> GetAll()=>
            _Context.Set<T>().ToList();


        public T GetById(int id) =>
            _Context.Set<T>().Find(id);

        public IEnumerable<T> Search(string SearchValue)
        {
            if(typeof(T) == typeof( Department) )
            {
                
                IEnumerable<Department> departments = _Context.departments.Where(d => d.Name.Trim().ToLower().Substring(0, SearchValue.Length) == SearchValue.Trim().ToLower());
                return (IEnumerable<T>)departments;

            }
            else if(typeof(T) == typeof(Employee) )
            {
                IEnumerable<Employee> employees = _Context.employees.Where(d => d.Name.Trim().ToLower().Substring(0, SearchValue.Length) == SearchValue.Trim().ToLower());
                return (IEnumerable<T>)employees;
            }
            return _Context.Set<T>().ToList();//default return
          
        }

        public void Update(T entity)
        =>  _Context.Update(entity);
           
        
    }
}
