using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using SoftCha.Services;
using SoftCha.Models.Employee;
using SoftCha.Models.Jobs;

namespace SoftCha.ViewModels.CRUD.Job
{
    public class DetailViewModel : MasterPageViewModel
    {
        private readonly JobServices jobService;

        public JobDetailModel Job { get; set; }

        [FromRoute("Id")]
        public int Id { get; private set; }

        public DetailViewModel(JobServices jobService)
        {
            this.jobService = jobService;
        }

        public override async Task PreRender()
        {
            Job= await jobService.GetJobByIdAsync(Id);
            await base.PreRender();
        }

        /* public async Task DeleteStudent()
         {
             await employeeService.DeleteEmployeeAsync(Id);
             Context.RedirectToRoute("Default", replaceInHistory: true);
         }*/
    }
}
