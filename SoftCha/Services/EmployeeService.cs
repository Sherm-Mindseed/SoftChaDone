using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftCha.Models.Employee;
using System.Reflection.Emit;
using DotVVM.Framework.Compilation.Parser;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System;
using System.Net;

namespace SoftCha.Services
{
    public class EmployeeService
    {
        private readonly string URLbase = "https://localhost:7204/api/Employees";

        public async Task<List<EmployeeListModel>> GetAllEmployeesAsync()
        {
            List<EmployeeListModel> employees = new List<EmployeeListModel>();
            using (var httpClient = new HttpClient())
            {
                string URL = URLbase + "/GetEmployees";

                HttpResponseMessage res = await httpClient.GetAsync(URL);
                Console.WriteLine(res);
                string apiRes = await res.Content.ReadAsStringAsync();
                if (res.StatusCode != HttpStatusCode.NotFound)
                {
                    employees = JsonConvert.DeserializeObject<List<EmployeeListModel>>(apiRes).Select(
                        s => new EmployeeListModel()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Surname = s.Surname,
                        }
                        ).ToList();


                }
                return employees;
            }
        }

        public async Task<EmployeeDetailModel> GetEmployeeByIdAsync(int Id)
        {
            string URL = URLbase + "/GetEmployeeById?id=" + Id;
            EmployeeDetailModel employeeDetailModel = new EmployeeDetailModel();

            using (var httpClient= new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(URL);
                string apiRes= await res.Content.ReadAsStringAsync();
                employeeDetailModel = JsonConvert.DeserializeObject<EmployeeDetailModel>(apiRes);
            }
            return employeeDetailModel;
        }

        public async Task InsertEmployeeAsync(EmployeeDetailModel employeeDetailModel)
        {
            string URL = URLbase + "/InsertEmployee";
            
            using (var httpClient= new HttpClient())
            {
               
                StringContent content= new StringContent(JsonConvert.SerializeObject(employeeDetailModel),Encoding.UTF8,"application/json");
                HttpResponseMessage res= await httpClient.PostAsync(URL, content);
                string apiRes=await res.Content.ReadAsStringAsync();
            }
        }

        public async Task UpdateEmployeeAsync(EmployeeDetailModel employeeDetailModel)
        {
            string URL = URLbase + "/UpdateEmployee";

            using (var httpClient= new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employeeDetailModel),Encoding.UTF8,"application/json");
                HttpResponseMessage res = await httpClient.PutAsync(URL, content);
                string apiRes=await res.Content.ReadAsStringAsync();
            }
        }


        public async Task DeleteUserAsync(int Id)
        {
            string URL = URLbase + "/DeleteEmployee/" + Id;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(URL);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
        }

    }
}
