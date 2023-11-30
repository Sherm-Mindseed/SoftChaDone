using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using SoftCha.Models.Skills;
using SoftCha.Services;

namespace SoftCha.ViewModels.CRUD
{
    public class SkillViewModel : MasterPageViewModel
    {

        private readonly SkillService skillService;

        [Bind(Direction.ServerToClient)]
        public List<SkillListModel> Skills { get; set; }

        public SkillViewModel(SkillService skillService)
        {
            this.skillService = skillService;
        }
        public override async Task PreRender()
        {
            Skills = await skillService.GetAllSkillsAsync();
            await base.PreRender();
        }

    }
}
