using SoftCha.Models.Skills;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftCha.Models.Employee
{
    public class EmployeeDetailModel
    {

        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string surname { get; set; }

        [Required]
        public DateTime hired { get; set; }
        
        public string skills{ get; set; }

    }
}
