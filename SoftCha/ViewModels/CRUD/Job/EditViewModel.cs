using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using SoftCha.Services;
using SoftCha.Models.Jobs;

namespace SoftCha.ViewModels.CRUD.Job
{
    public class EditViewModel : MasterPageViewModel
    {
        private readonly JobServices jobService;

        public JobDetailModel Job { get; set; }

        [FromRoute("Id")]
        public int Id { get; private set; }

        public EditViewModel(JobServices jobService)
        {
            this.jobService = jobService;
        }


        public override async Task PreRender()
        {
            if (Id != 0)
            {
                Job = await jobService.GetJobByIdAsync(Id);
            }
            await base.PreRender();
        }

        public async Task EditStudent()
        {
            await jobService.UpdateJobAsync(Job);
            Context.RedirectToRoute("CRUD_Skill_Detail", new { Id });
        }
    }
}
