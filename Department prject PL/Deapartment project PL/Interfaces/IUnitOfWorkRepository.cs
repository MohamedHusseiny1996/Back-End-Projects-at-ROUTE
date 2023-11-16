using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deapartment_project_BLL.Interfaces
{
    public interface IUnitOfWorkRepository:IDisposable
    {
        public IDepartmentRepository _Department { get; set; }
        public IEmployeeRepository _Employee { get; set; }
        public int Complete();
        
        
    }
}
