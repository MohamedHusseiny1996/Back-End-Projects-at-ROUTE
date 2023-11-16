using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department_project_DAL.Entities
{
    [Index("Name")]
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime HiringDate { get; set; }= DateTime.Now;
        public string? ImageUrl { get; set; }

        [Required,DataType(DataType.Currency)]
        public double Salary { get; set; }
        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
