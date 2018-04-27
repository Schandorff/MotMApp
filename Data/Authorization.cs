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
        private string Credentials = "grant_type=password&username=npesdk@gmail.com&password=1234567890";
        
        //Gets a new bearer token based on the credentials
        public async Task<AuthToken> GetToken()
        {
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(AuthUrl, new StringContent(Credentials, Encoding.UTF8, "application/json"));
            AuthToken Token = JsonConvert.DeserializeObject<AuthToken>(await response.Content.ReadAsStringAsync());
            return Token;
        }
    }
}
