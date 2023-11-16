using Department_project_DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Department_prject_PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "this field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "this field is required")]
        public string code { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public IEnumerable<Employee> Employees { get; set; }
    }
}
