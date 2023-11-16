using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department_project_DAL.Entities
{
    public class UserApplication:IdentityUser
    {
        public DateTime? DateOfCreation { get; set; } = DateTime.Now;
        public bool IsAgree { get; set; }
    }
}
