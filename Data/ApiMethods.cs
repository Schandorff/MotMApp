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
    class ApiMethods
    {
        const string ContentUrl = "https://motmapi.nikolajsimonsen.com/Umbraco/API/TestApi/";
        Authorization authorization = new Authorization();

        public async Task<List<Club>> GetAllClubs(string method, [Optional] int id)
        {
            HttpClient client = new HttpClient();
            var Token = await authorization.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            var answer = await client.GetAsync(ContentUrl + method + "?" + "rootID=" + id);
            List<Club> response = JsonConvert.DeserializeObject<List<Club>>(await answer.Content.ReadAsStringAsync());
            return response;
        }

        public async Task<Club> GetClub(string method, [Optional] int id)
        {
            HttpClient client = new HttpClient();
            var Token = await authorization.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            var answer = await client.GetAsync(ContentUrl + method + "?" + "cID=" + id);
            Club response = JsonConvert.DeserializeObject<Club>(await answer.Content.ReadAsStringAsync());
            return response;
        }

        public async Task<List<Player>> GetMatchPlayers(string method, int id)
        {
            HttpClient client = new HttpClient();
            var Token = await authorization.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            var answer = await client.GetAsync(ContentUrl + method + "?" + "mID=" + id);
            List<Player> response = JsonConvert.DeserializeObject<List<Player>>(await answer.Content.ReadAsStringAsync());
            return response;
        }
        public async Task<Team> GetTeam(string method, [Optional] int id)
        {
            HttpClient client = new HttpClient();
            var Token = await authorization.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            var answer = await client.GetAsync(ContentUrl + method + "?" + "tID=" + id);
            Team response = JsonConvert.DeserializeObject<Team>(await answer.Content.ReadAsStringAsync());
            return response;
        }
    }
}
