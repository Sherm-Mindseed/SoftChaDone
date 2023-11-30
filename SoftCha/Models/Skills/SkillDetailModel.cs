using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace SoftCha.Models.Skills
{
    public class SkillDetailModel
    {
        public int idskills { get; set; }
        [Required]
        public string Name { get; set; }

        public string Desc { get; set; }
        [Required]
        public DateTime Created { get; set; }

    }
}
