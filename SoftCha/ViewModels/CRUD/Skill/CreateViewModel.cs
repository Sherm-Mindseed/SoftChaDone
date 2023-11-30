using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using SoftCha.Models.Employee;
using SoftCha.Models.Skills;
using SoftCha.Services;

namespace SoftCha.ViewModels.CRUD.Skill
{
    public class CreateViewModel : MasterPageViewModel
    {
        private readonly SkillService skillService;

        public SkillDetailModel Skill { get; set; } = new SkillDetailModel { Created = DateTime.UtcNow.Date };

        public CreateViewModel(SkillService studentService)
        {
            skillService = studentService;
        }


        public async Task AddStudent()
        {
            await skillService.InsertUserAsync(Skill);
            Context.RedirectToRoute("Default");
        }
    }
}
