using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TBL_Stats.Models;

namespace TBL_Stats.Services
{
    public class RestService : IRestService
    {
        HttpClient client;
        public Team Team { get; private set; }

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<Team> GetTeamNameAsync()
        {
            Team = new Team();
            Uri uri = new Uri(string.Format(Constants.teamUri, string.Empty));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    JObject teamInfo = JObject.Parse(await response.Content.ReadAsStringAsync());
                    JToken teamStats = teamInfo["teams"][0]["teamStats"][0]["splits"][0]["stat"];

                    Team.TeamName = (string)teamInfo["teams"][0]["name"];
                    Team.GamesPlayed = (int)teamStats["gamesPlayed"];
                    Team.Wins = (int)teamStats["wins"];
                    Team.Losses = (int)teamStats["losses"];
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Team;
        }
    }
}
