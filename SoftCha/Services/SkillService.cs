using Newtonsoft.Json;
using SoftCha.Models.Jobs;
using SoftCha.Models.Skills;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SoftCha.Services
{
    public class SkillService
    {
        private readonly string URLbase = "https://localhost:7204/api/Skills";

        public async Task<List<SkillListModel>> GetAllSkillsAsync()
        {
            List<SkillListModel> skill = new List<SkillListModel>();
            using (var httpClient = new HttpClient())
            {
                string URL = URLbase + "/GetSkills";
                HttpResponseMessage res = await httpClient.GetAsync(URL);
                string apiRes = await res.Content.ReadAsStringAsync();
                if (res.StatusCode != HttpStatusCode.NotFound)
                {
                    skill = JsonConvert.DeserializeObject<List<SkillListModel>>(apiRes).Select(
                        s => new SkillListModel()
                        {
                            idskills = s.idskills,
                            Name = s.Name,

                        }
                        ).ToList();
                }
            }
            return skill;
        }

        public async Task<SkillDetailModel> GetJobByName(string name)
        {
            string URL = URLbase + "/GetSkillByName?name="+name;
            SkillDetailModel jobDetailModel = new SkillDetailModel();
            using var httpClient = new HttpClient();
            {
                HttpResponseMessage res = await httpClient.GetAsync(URL);
                string apiRes = await res.Content.ReadAsStringAsync();
                jobDetailModel = JsonConvert.DeserializeObject<SkillDetailModel>(apiRes);
            }
            return jobDetailModel;
        }

        public async Task<SkillDetailModel> GetSkillByIdAsync(int Id)
        {
            string URL = URLbase + "/GetSkillById?id=" + Id;
            SkillDetailModel skillDetailModel = new SkillDetailModel();

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(URL);
                string apiRes = await res.Content.ReadAsStringAsync();
                skillDetailModel = JsonConvert.DeserializeObject<SkillDetailModel>(apiRes);
            }
            return skillDetailModel;
        }

        public async Task<SkillDetailModel> InsertUserAsync(SkillDetailModel skillDetailModel)
        {
            string URL = URLbase + "/InsertSkill";
            SkillDetailModel skill=null;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(skillDetailModel), Encoding.UTF8, "application/json");
                HttpResponseMessage res = await httpClient.PostAsync(URL, content);
                string apiRes = await res.Content.ReadAsStringAsync();
                try
                {
                     skill=JsonConvert.DeserializeObject<SkillDetailModel>(apiRes);
                    
                }
                catch
                {

                }
            }

            return skill;
        }

        public async Task UpdateUserAsync(SkillDetailModel skillDetailModel)
        {
            string URL = URLbase + "/UpdateSkill";

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(skillDetailModel), Encoding.UTF8, "application/json");
                HttpResponseMessage res = await httpClient.PutAsync(URL, content);
                string apiRes = await res.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteUserAsync(int Id)
        {
            string URL = URLbase + "/DeleteSkill/" + Id;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(URL);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
