using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using SoftCha.Models.Employee;
using SoftCha.Models.Skills;
using SoftCha.Services;

namespace SoftCha.ViewModels.CRUD.Employee
{
    public class CreateViewModel : MasterPageViewModel
    {
        private readonly EmployeeService employeeService;
        private readonly SkillService skillService;
        public List<SkillListModel> SourceList { get; set; }
        public List<SkillListModel> TargetList { get; set; } = new List<SkillListModel>();
        public EmployeeDetailModel Employee { get; set; } = new EmployeeDetailModel { hired = DateTime.UtcNow.Date };
        public string sAdd { get; set; }   
        public CreateViewModel(EmployeeService studentService,SkillService skillService)
        {
            employeeService = studentService;
            this.skillService = skillService;
            
            
        }

        public override async Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                SourceList = await skillService.GetAllSkillsAsync();
            }
            await base.PreRender();
        }


        public async Task AddStudent()
        {
            var skill = Employee.skills;
           
            StringBuilder stringBuilder = new StringBuilder();
            foreach (SkillListModel s in TargetList)
            {
                stringBuilder.Append(s.idskills.ToString());
                stringBuilder.Append(",");
            }
            Employee.skills = stringBuilder.ToString();
            await employeeService.InsertEmployeeAsync(Employee);
            Context.RedirectToRoute("CRUD_Employee");
        }

    public void AddToTargetList(SkillListModel item)
        {
            TargetList.Add(item);
            SourceList.Remove(item);
            
        }

    public void RemoveFromTargetList(SkillListModel item)
        {
            SourceList.Add(item);
            TargetList.Remove(item);
            
        }
    public async Task AddNewSkill(String name)
        {
            var skillcheck = await skillService.GetJobByName(name);
            if (skillcheck.Name == null)
            {
                SkillDetailModel skilltoadd = new SkillDetailModel { Created = DateTime.UtcNow, Name =name };
                SkillDetailModel skillid = await skillService.InsertUserAsync(skilltoadd);
                SourceList.Add(new SkillListModel { idskills = skillid.idskills, Name = skillid.Name });
                
            }
            
        }

    }
}
