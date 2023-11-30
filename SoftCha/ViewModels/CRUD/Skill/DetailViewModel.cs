using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using SoftCha.Services;
using SoftCha.Models.Employee;
using SoftCha.Models.Skills;

namespace SoftCha.ViewModels.CRUD.Skill
{
    public class DetailViewModel : MasterPageViewModel
    {
        private readonly SkillService skillService;

        public SkillDetailModel Skill { get; set; }

        [FromRoute("Id")]
        public int Id { get; private set; }

        public DetailViewModel(SkillService skillService)
        {
            this.skillService = skillService;
        }

        public override async Task PreRender()
        {
            Skill= await skillService.GetSkillByIdAsync(Id);
            await base.PreRender();
        }

        public async Task DeleteStudent()
        {
            await skillService.DeleteUserAsync(Id);
            Context.RedirectToRoute("Default", replaceInHistory: true);
        }
    }
}
