using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using SoftCha.Models.Employee;
using SoftCha.Models.Jobs;
using SoftCha.Services;

namespace SoftCha.ViewModels.CRUD.Job
{
    public class CreateViewModel : MasterPageViewModel
    {
        private readonly JobServices jobService;

        public JobDetailModel Job { get; set; } = new JobDetailModel();

        public CreateViewModel(JobServices jobService)
        {
            this.jobService = jobService;
        }


        public async Task AddStudent()
        {
            await jobService.InsertJobAsync(Job);
            Context.RedirectToRoute("CRUD_Job");
        }
    }
}
