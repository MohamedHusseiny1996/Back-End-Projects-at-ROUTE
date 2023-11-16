using Department_project_DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Department_prject_PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string Name { get; set; }
        public DateTime HiringDate { get; set; } = DateTime.Now;
        public IFormFile image { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "this field is required"), DataType(DataType.Currency)]
        public double Salary { get; set; }
        [Required(ErrorMessage = "this field is required")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
