using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using SoftCha.Services;
using SoftCha.Models.Employee;
using SoftCha.Models.Skills;
using System.Text;

namespace SoftCha.ViewModels.CRUD.Employee
{
    public class EditViewModel : MasterPageViewModel
    {
        private readonly EmployeeService employeeService;
        private readonly SkillService skillService;
        public List<SkillListModel> SourceList { get; set; }= new List<SkillListModel>();
        public List<SkillListModel> TargetList { get; set; } = new List<SkillListModel>();
        public EmployeeDetailModel Employee { get; set; }
        public string sAdd { get; set; }
        [FromRoute("Id")]
        public int Id { get; private set; }

        public EditViewModel(EmployeeService employeeService,SkillService skillService)
        {
            this.skillService = skillService;
            this.employeeService = employeeService;
        }


        public override async Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                SourceList = await skillService.GetAllSkillsAsync();
                if (Id != 0)
                {
                    Employee = await employeeService.GetEmployeeByIdAsync(Id);
                }
                List<string> skillsToAdd = Employee.skills.Split(",").ToList<string>();
                SkillDetailModel temp = null;
                foreach (var sk in skillsToAdd)
                {
                    if (!sk.Equals(""))
                    {
                        temp = await skillService.GetSkillByIdAsync(int.Parse(sk));
                        TargetList.Add(new SkillListModel { idskills =temp.idskills,Name=temp.Name});
                    }
                }
                SourceList.RemoveAll(x=>TargetList.Any(y=>y.idskills ==x.idskills));
            }
            await base.PreRender();
        }

        public async Task EditStudent()
        {
            var skill = Employee.skills;

            StringBuilder stringBuilder = new StringBuilder();
            foreach (SkillListModel s in TargetList)
            {
                stringBuilder.Append(s.idskills.ToString());
                stringBuilder.Append(",");
            }
            Employee.skills = stringBuilder.ToString();
            await employeeService.UpdateEmployeeAsync(Employee);
            Context.RedirectToRoute("CRUD_Employee_Detail", new { Id });
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
                SkillDetailModel skilltoadd = new SkillDetailModel { Created = DateTime.UtcNow, Name = name };
                SkillDetailModel skillid = await skillService.InsertUserAsync(skilltoadd);
                SourceList.Add(new SkillListModel { idskills = skillid.idskills, Name = skillid.Name });

            }

        }
    }
}
