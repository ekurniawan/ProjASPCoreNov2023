using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackendApp.DTO
{
    public class DepartmentCreateDTO
    {
        [Required]
        public string? DepartmentName { get; set; }
    }
}