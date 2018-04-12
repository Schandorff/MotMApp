using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Manofthematch.Data
{
    public class TeamManager
    {
        const string AuthUrl = "https://motmapi.nikolajsimonsen.com/umbraco/oauth/token";
        //private string Username = "voteuser@nikolajsimonsen.com";
        //private string Password = "motmvoteuser";
        private string BodyType = "grant_type=password&username=npesdk@gmail.com&password=1234567890";

        public async Task<AT> GetToken(){

            HttpClient client = new HttpClient();


            var response = await client.PostAsync(AuthUrl, new StringContent(JsonConvert.SerializeObject(BodyType),Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<AT>(await response.Content.ReadAsStringAsync());
        }
        public class AT {
            public string AccesToken { get; set; }
            public string TokenType {  get; set; }
            public int ExpiryTime { get; set; }
        }
    }
}
