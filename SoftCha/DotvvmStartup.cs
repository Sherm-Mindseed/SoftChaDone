using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;
using SoftCha.Services;

namespace SoftCha
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {

            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/Default.dothtml");
            config.RouteTable.Add("CRUD_Employee", "Employee", "Views/CRUD/Employee.dothtml");
            config.RouteTable.Add("CRUD_Skill", "Skills", "Views/CRUD/Skill.dothtml");
            config.RouteTable.Add("CRUD_Job", "Jobs", "Views/CRUD/Job.dothtml");
            config.RouteTable.Add("CRUD_Employee_Detail", "Employee/detail/{Id}", "Views/CRUD/Employee/Detail.dothtml");
            config.RouteTable.Add("CRUD_Employee_Edit", "Employee/edit/{Id}", "Views/CRUD/Employee/Edit.dothtml");
            config.RouteTable.Add("CRUD_Skill_Create", "Skills/create", "Views/CRUD/Skill/Create.dothtml");
            config.RouteTable.Add("CRUD_Skill_Detail", "Skills/detail/{Id}", "Views/CRUD/Skill/Detail.dothtml");
            config.RouteTable.Add("CRUD_Skill_Edit", "Skills/edit/{Id}", "Views/CRUD/Skill/Edit.dothtml");
            config.RouteTable.Add("CRUD_Job_Create", "Job/create", "Views/CRUD/Job/Create.dothtml");
            config.RouteTable.Add("CRUD_Job_Detail", "Job/detail/{Id}", "Views/CRUD/Job/Detail.dothtml");
            config.RouteTable.Add("CRUD_Job_Edit", "Skills/Job/{Id}", "Views/CRUD/Job/Edit.dothtml");
            config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
			config.Resources.Register("Styles", new StylesheetResource()
            {
                Location = new UrlResourceLocation("~/styles.css")
            });
        }

		public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
            options.AddHotReload();
            options.Services.AddScoped<EmployeeService>();
            options.Services.AddScoped<SkillService>();
            options.Services.AddScoped<JobServices>();
		}
    }
}
