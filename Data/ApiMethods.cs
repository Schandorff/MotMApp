﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Manofthematch.Models;
using System.Runtime.InteropServices;
using Plugin.Connectivity;

namespace Manofthematch.Data
{
    class ApiMethods
    {
        const string ContentUrl = "https://motmapi.nikolajsimonsen.com/Umbraco/API/TestApi/";
        const string TestUrl = "http://localhost:6801/Umbraco/API/TestApi/";
        readonly Authorization authorization = new Authorization();

        public async Task<List<Club>> GetAllClubs(string method, [Optional] int id)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new List<Club>();
            }
            else
            {
                HttpClient client = new HttpClient();
                var Token = await authorization.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.GetAsync(ContentUrl + method + "?" + "rootID=" + id);
                List<Club> response = JsonConvert.DeserializeObject<List<Club>>(await answer.Content.ReadAsStringAsync());
                return response;
            }
            
        }

        public async Task<Club> GetClub(string method, [Optional] int id)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Club();
            }
            else
            {
                HttpClient client = new HttpClient();
                var Token = await authorization.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.GetAsync(ContentUrl + method + "?" + "cID=" + id);
                Club response = JsonConvert.DeserializeObject<Club>(await answer.Content.ReadAsStringAsync());
                return response;
            }
        }

        public async Task<List<Player>> GetMatchPlayers(string method, int id)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new List<Player>();
            }
            else
            {
                HttpClient client = new HttpClient();
                var Token = await authorization.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.GetAsync(ContentUrl + method + "?" + "mID=" + id);
                List<Player> response =
                JsonConvert.DeserializeObject<List<Player>>(await answer.Content.ReadAsStringAsync());
                return response;
            }
        }

        public async Task<Team> GetTeam(string method, [Optional] int id)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Team();
            }
            else
            {
                HttpClient client = new HttpClient();
                var Token = await authorization.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.GetAsync(ContentUrl + method + "?" + "tID=" + id);
                Team response = JsonConvert.DeserializeObject<Team>(await answer.Content.ReadAsStringAsync());
                return response;
            }
        }

        public async Task<Vote> PostVote(string method, int id, Vote vote)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Vote();
            }
            else
            {
                HttpClient client = new HttpClient();
                Vote postVote = new Vote();
                postVote.playerId = vote.playerId;
                postVote.deviceId = vote.deviceId;
                var Token = await authorization.GetToken();

                StringContent message = new StringContent(JsonConvert.SerializeObject(postVote), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.PostAsync(ContentUrl + method + "?" + "mID=" + id, message);
                Vote response = JsonConvert.DeserializeObject<Vote>(await answer.Content.ReadAsStringAsync());
                return response;
            }
        }

        public async Task<object> UpdateVote(string method, int id, Vote vote)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new List<Club>();
            }
            else
            {
                HttpClient client = new HttpClient();
                Vote updateVote = new Vote();
                updateVote.playerId = vote.playerId;
                updateVote.deviceId = vote.deviceId;
                updateVote.voteId = vote.voteId;
                var Token = await authorization.GetToken();
                StringContent message = new StringContent(JsonConvert.SerializeObject(updateVote), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.PutAsync(ContentUrl + method + "?" + "vID=" + id, message);
                var response = JsonConvert.DeserializeObject<object>(await answer.Content.ReadAsStringAsync());
                return response;
            }
        }

        public async Task<List<Vote>> GetMatchVotes(string method, int id)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new List<Vote>();
            }
            else
            {
                HttpClient client = new HttpClient();
                var Token = await authorization.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.GetAsync(ContentUrl + method + "?" + "mID=" + id);
                List<Vote> response = JsonConvert.DeserializeObject<List<Vote>>(await answer.Content.ReadAsStringAsync());
                return response;
            }
        }

        public async Task<Admin> ClubAdminLogin(string method, Admin clubAdmin)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Admin();
            }
            else
            {
                HttpClient client = new HttpClient();
                var Token = await authorization.GetClubAdminToken(clubAdmin);
                StringContent message = new StringContent(JsonConvert.SerializeObject(clubAdmin), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.PutAsync(ContentUrl + method, message);
                Admin response = JsonConvert.DeserializeObject<Admin>(await answer.Content.ReadAsStringAsync());
                return response;
            }
        }

        public async Task<List<Team>> GetClubAdminTeams(string method, int id, Admin clubAdmin)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new List<Team>();
            }
            else
            {
                HttpClient client = new HttpClient();
                var Token = await authorization.GetClubAdminToken(clubAdmin);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.GetAsync(ContentUrl + method + "?" + "cID=" + id);
                List<Team> response = JsonConvert.DeserializeObject<List<Team>>(await answer.Content.ReadAsStringAsync());
                return response;
            }
        }

        public async Task<Match> GetMatch(string method, [Optional] int id)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Match();
            }
            else
            {
                HttpClient client = new HttpClient();
                var Token = await authorization.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.GetAsync(ContentUrl + method + "?" + "mID=" + id);
                Match response = JsonConvert.DeserializeObject<Match>(await answer.Content.ReadAsStringAsync());
                return response;
            }
        }

        public async Task<Match> UpdateMatchById(string method, int id, Match match)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Match();
            }
            else
            {
                HttpClient client = new HttpClient();
                var Token = await authorization.GetToken();
                StringContent message = new StringContent(JsonConvert.SerializeObject(match), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                var answer = await client.PutAsync(ContentUrl + method + "?" + "mID=" + id, message);
                Match response = JsonConvert.DeserializeObject<Match>(await answer.Content.ReadAsStringAsync());
                return response;
            }
        }            
    }
}
