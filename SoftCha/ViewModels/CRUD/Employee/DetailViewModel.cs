using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using SoftCha.Services;
using SoftCha.Models.Employee;
using SoftCha.Models.Skills;

namespace SoftCha.ViewModels.CRUD.Employee
{
    public class DetailViewModel : MasterPageViewModel
    {
        private readonly EmployeeService employeeService;
        private readonly SkillService skillService;

        public EmployeeDetailModel Employee { get; set; }
        public List<SkillDetailModel> Skills { get; set; }


        

        [FromRoute("Id")]
        public int Id { get; private set; }

        public DetailViewModel(EmployeeService employeeService,SkillService skillService)
        {
            this.employeeService = employeeService;
            this.skillService = skillService;            
        }

        public override async Task PreRender()
        {
            Employee = await employeeService.GetEmployeeByIdAsync(Id);
           
            Skills = new List<SkillDetailModel>();
            if (Employee.skills != null)
            {
                List<string> skillsToAdd = Employee.skills.Split(",").ToList<string>();
                SkillDetailModel temp = null;
                foreach (var sk in skillsToAdd)
                {
                    if (!sk.Equals(""))
                    {
                        temp = await skillService.GetSkillByIdAsync(int.Parse(sk));
                        Skills.Add(temp);
                    }
                }
            }
            

            await base.PreRender();
        }

        public async Task DeleteStudent()
        {
            await employeeService.DeleteUserAsync(Id);
            Context.RedirectToRoute("Default", replaceInHistory: true);
        }
    }
}
