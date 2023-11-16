using Deapartment_project_BLL.Interfaces;
using Department_project_DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deapartment_project_BLL.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
       

        public IDepartmentRepository _Department { get; set ; }
        public IEmployeeRepository _Employee { get ; set ; }
        public CompanyContext Dbcontext { get; set; }

        public UnitOfWorkRepository( CompanyContext dbcontext )
        {
            _Department = new DepartmentRepository(dbcontext);
            _Employee = new EmployeeRepository(dbcontext);
            Dbcontext = dbcontext;
        }

        public int Complete()
        {
            return Dbcontext.SaveChanges();
        }
       

        public void Dispose()
        {
            Dbcontext.Dispose();
        }
    }
}
