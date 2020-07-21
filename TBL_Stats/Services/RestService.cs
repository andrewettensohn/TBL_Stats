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
                HttpResponseMessage response = await client.GetAsync(uri + "?expand=team.stats");
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

        public async Task<List<Skater>> GetSkatersAsync()
        {
            List<Skater> skaters = new List<Skater>();
            Uri uri = new Uri(string.Format(Constants.teamUri, string.Empty));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri + "?expand=team.roster");
                if (response.IsSuccessStatusCode)
                {
                    JObject teamInfo = JObject.Parse(await response.Content.ReadAsStringAsync());
                    JToken roster = teamInfo["teams"][0]["roster"]["roster"];

                    foreach(JToken skaterInfo in roster)
                    {
                        Skater skater = new Skater
                        {
                            SkaterId = (int)skaterInfo["person"]["id"],
                            Name = (string)skaterInfo["person"]["fullName"]
                        };
                        skaters.Add(skater);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return skaters;
        }

        public async Task<Skater> GetSkaterAsync(int skaterId)
        {
            Skater skater = new Skater();
            Uri uri = new Uri(string.Format(Constants.personUri, string.Empty));

            try
            {
                HttpResponseMessage response = await client.GetAsync($"{uri}{skaterId}/stats?stats=statsSingleSeason");
                if (response.IsSuccessStatusCode)
                {
                    JObject skaterStatsInfo = JObject.Parse(await response.Content.ReadAsStringAsync());
                    JToken skaterStats = skaterStatsInfo["stats"][0]["splits"][0]["stat"];
                    skater.Games = (int)skaterStats["games"];
                    skater.Goals = (int)skaterStats["goals"];
                    skater.Assists = (int)skaterStats["assists"];
                    //8476826/stats?stats=statsSingleSeason
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return skater;
        }
    }
}
