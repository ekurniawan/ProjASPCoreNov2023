using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyASPCore.Models;

namespace MyASPCore.ViewModels
{
    public class EmployeeCreateViewModel
    {
        public int DepartmentID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "FirstName tidak boleh kosong")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "LastName tidak boleh kosong")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("Email Address")]
        [Required]
        [EmailAddress]
        //[RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Format Email tidak sesuai")]
        public string Email { get; set; } = string.Empty;

        public List<SelectListItem> Departments { get; set; } = new List<SelectListItem>();
    }
}