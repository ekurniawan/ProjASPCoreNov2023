using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackendApp.DTO
{
    public class DepartmentDTO
    {
        public int DepartmentID { get; set; }

        [Required]
        public string? DepartmentName { get; set; }
    }
}