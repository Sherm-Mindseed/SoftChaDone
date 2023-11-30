using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using SoftCha.Services;
using SoftCha.Models.Skills;

namespace SoftCha.ViewModels.CRUD.Skill
{
    public class EditViewModel : MasterPageViewModel
    {
        private readonly SkillService skillService;

        public SkillDetailModel Skill { get; set; }

        [FromRoute("Id")]
        public int Id { get; private set; }

        public EditViewModel(SkillService skillService)
        {
            this.skillService = skillService;
        }


        public override async Task PreRender()
        {
            if (Id != 0)
            {
                Skill = await skillService.GetSkillByIdAsync(Id);
            }
            await base.PreRender();
        }

        public async Task EditStudent()
        {
            await skillService.UpdateUserAsync(Skill);
            Context.RedirectToRoute("CRUD_Skill_Detail", new { Id });
        }
    }
}
