using SoftCha.Models.Skills;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftCha.Models.Jobs
{
    public class JobDetailModel
    {
        public int Id { get; set; }
        [Required]
        public string JobName { get; set; }
        [Required]
        public List<SkillDetailModel> Skills { get; set; }
    }
}
