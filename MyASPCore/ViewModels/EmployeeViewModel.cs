using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyASPCore.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
    }
}