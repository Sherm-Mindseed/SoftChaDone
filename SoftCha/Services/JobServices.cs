using Newtonsoft.Json;
using SoftCha.Models.Jobs;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SoftCha.Services
{
    public class JobServices
    {
        private readonly string URLbase = "https://localhost:7204/api/Job";

        public async Task<List<JobListModel>> GetAllJobsAsync()
        {
            List<JobListModel> employees = new List<JobListModel>();
            using (var httpClient = new HttpClient())
            {
                string URL = URLbase + "/GetJobs";
                HttpResponseMessage res = await httpClient.GetAsync(URL);
                string apiRes = await res.Content.ReadAsStringAsync();
                if(res.StatusCode != HttpStatusCode.NotFound)
                {
                    employees = JsonConvert.DeserializeObject<List<JobListModel>>(apiRes).Select(
                        s => new JobListModel()
                        {
                            Id = s.Id,
                            Name = s.Name,
                        }
                        ).ToList();
                }
                
            }
            return employees;
        }

        public async Task<JobDetailModel> GetJobByIdAsync(int Id)
        {
            string URL = URLbase + "/GetJobById?id=" + Id;
            JobDetailModel jobDetailModel = new JobDetailModel();

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(URL);
                string apiRes = await res.Content.ReadAsStringAsync();
                jobDetailModel = JsonConvert.DeserializeObject<JobDetailModel>(apiRes);
            }
            return jobDetailModel;
        }

        public async Task InsertJobAsync(JobDetailModel jobDetailModel)
        {
            string URL = URLbase + "/InsertJob";

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(jobDetailModel), Encoding.UTF8, "application/json");
                HttpResponseMessage res = await httpClient.PostAsync(URL, content);
                string apiRes = await res.Content.ReadAsStringAsync();
            }
        }

        public async Task UpdateJobAsync(JobDetailModel jobDetailModel)
        {
            string URL = URLbase + "/UpdateUser";

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(jobDetailModel), Encoding.UTF8, "application/json");
                HttpResponseMessage res = await httpClient.PutAsync(URL, content);
                string apiRes = await res.Content.ReadAsStringAsync();
            }
        }

        public async Task<JobDetailModel> GetJobByName(string name)
        {
            string URL = URLbase + "/GetSkillByName";
            JobDetailModel jobDetailModel =new JobDetailModel();
            using var httpClient = new HttpClient();
            {
                HttpResponseMessage res = await httpClient.GetAsync(URL);
                string apiRes = await res.Content.ReadAsStringAsync();
                jobDetailModel = JsonConvert.DeserializeObject<JobDetailModel>(apiRes);
            }
            return jobDetailModel;
        }
    }
}
