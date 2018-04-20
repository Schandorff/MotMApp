using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Manofthematch.Models;
using System.Runtime.InteropServices;

namespace Manofthematch.Data
{
    public class Authorization
    {
        const string AuthUrl = "https://motmapi.nikolajsimonsen.com/umbraco/oauth/token";
        const string ContentUrl = "https://motmapi.nikolajsimonsen.com/Umbraco/API/TestApi/";
        //private string Username = "voteuser@nikolajsimonsen.com";
        //private string Password = "motmvoteuser";
        private string Credentials = "grant_type=password&username=npesdk@gmail.com&password=1234567890";
        //AuthToken.AT AT = new AuthToken.AT();

        
        //Gets a new bearer token based on the credentials
        public async Task<AuthToken> GetToken(){

            HttpClient client = new HttpClient();

            var response = await client.PostAsync(AuthUrl, new StringContent(Credentials, Encoding.UTF8, "application/json"));

            AuthToken Token = JsonConvert.DeserializeObject<AuthToken>(await response.Content.ReadAsStringAsync());
            //client.DefaultRequestHeaders.Add("Authorization", Authororization.access_token);

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Authororization.access_token);

            //return JsonConvert.DeserializeObject<AT>(await response.Content.ReadAsStringAsync());
            return Token;
        }

        //async void GetAuth(object sender, EventArgs e)
        //{
        //    Authorization authorize = new Authorization();
        //    AuthToken Token = new AuthToken();
        //    Token = await authorize.GetToken();
        //}

        //public async Task<List<Club>> GetAllClubs(string method, [Optional] int id)
        //{
        //    HttpClient client = new HttpClient();
        //    var Token = await GetToken();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.AccessToken);
        //    var answer = await client.GetAsync(ContentUrl + method + "?" + "rootID=" + id);
        //    //List<Club> response = JsonConvert.DeserializeObject<List<Club>>(await answer.Content.ReadAsStringAsync());
        //    List<Club> response = JsonConvert.DeserializeObject<List<Club>>(await answer.Content.ReadAsStringAsync());
        //    return response;
        //}

        public async Task<List<Club>> GetAllClubs(string method, [Optional] int id)
        {
            HttpClient client = new HttpClient();
            var Token = await GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            var answer = await client.GetAsync(ContentUrl + method + "?" + "rootID=" + id);
            List<Club> response = JsonConvert.DeserializeObject<List<Club>>(await answer.Content.ReadAsStringAsync());
            return response;
        }


    }
}
