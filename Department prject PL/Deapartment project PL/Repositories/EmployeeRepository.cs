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
    public class EmployeeRepository: GenericRepository<Employee>, IEmployeeRepository
    {
       
        public EmployeeRepository(CompanyContext context):base(context) 
        {
           
        }

        
    }
}

