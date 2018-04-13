using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Manofthematch.Data
{
    public class TeamManager
    {
        const string AuthUrl = "https://motmapi.nikolajsimonsen.com/umbraco/oauth/token";
        const string ContentUrl = "https://motmapi.nikolajsimonsen.com/umbraco/rest/v1/";
        //private string Username = "voteuser@nikolajsimonsen.com";
        //private string Password = "motmvoteuser";
        private string BodyType = "grant_type=password&username=npesdk@gmail.com&password=1234567890";

        
        
        public async Task<AT> GetToken(){

            HttpClient client = new HttpClient();

            var response = await client.PostAsync(AuthUrl, new StringContent(BodyType,Encoding.UTF8, "application/json"));

            var Authororization = JsonConvert.DeserializeObject<AT>(await response.Content.ReadAsStringAsync());
            //client.DefaultRequestHeaders.Add("Authorization", Authororization.access_token);

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Authororization.access_token);

            //return JsonConvert.DeserializeObject<AT>(await response.Content.ReadAsStringAsync());
            return Authororization;
        }

        async void GetAuth(object sender, EventArgs e)
        {
            TeamManager manager = new TeamManager();
            AT Token = new TeamManager.AT();
            Token = await manager.GetToken();
        }

        public async Task<Object> GetContent()
        {
            HttpClient client = new HttpClient();
            var Token = await GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            var answer = await client.GetAsync(ContentUrl + "content/");
            object response = JsonConvert.DeserializeObject<Object>(await answer.Content.ReadAsStringAsync()); 
            return response;
        }

        public class AT {
            public string access_token { get; set; }
            public string token_type {  get; set; }
            public int expires_in { get; set; }
        }
    }
}
