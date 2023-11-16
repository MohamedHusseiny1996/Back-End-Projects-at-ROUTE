using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department_project_DAL.Entities
{
    public class Department
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string code { get; set; }

        public DateTime? CreatedDate { get; set; }= DateTime.Now;
        public IEnumerable<Employee>  Employees { get; set; }
    }
}
