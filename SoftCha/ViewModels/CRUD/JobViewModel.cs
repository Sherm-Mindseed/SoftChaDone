using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using SoftCha.Models.Jobs;
using SoftCha.Services;

namespace SoftCha.ViewModels.CRUD
{
    public class JobViewModel : MasterPageViewModel
    {

        private readonly JobServices JobService;

        [Bind(Direction.ServerToClient)]
        public List<JobListModel> Job { get; set; }

        public JobViewModel(JobServices JobService)
        {
            this.JobService = JobService;
        }
        public override async Task PreRender()
        {
            Job = await JobService.GetAllJobsAsync();
            await base.PreRender();
        }

    }
}
