using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using SoftCha.Models.Employee;
using SoftCha.Services;

namespace SoftCha.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {

        private readonly EmployeeService employeeService;

        [Bind(Direction.ServerToClient)]
        public List<EmployeeListModel> Employees { get; set; }

		public DefaultViewModel(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public override async Task PreRender()
        {
            Employees =  await employeeService.GetAllEmployeesAsync();
            await base.PreRender();
        }

    }
}
