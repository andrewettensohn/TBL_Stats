using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public async Task<Team> GetTeamStatsAsync()
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
                    Team.OverTime = (int)teamStats["ot"];
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Team;
        }

        public async Task<List<Skater>> GetRosterAsync()
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
                            Name = (string)skaterInfo["person"]["fullName"],
                            PositionShort = (string)skaterInfo["position"]["abbreviation"]
                            
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

        //public async Task<Skater> GetSkaterAsync(Skater skater)
        //{
        //    Uri uri = new Uri(string.Format(Constants.personUri, string.Empty));

        //    try
        //    {
        //        HttpResponseMessage response = await client.GetAsync($"{uri}{skater.SkaterId}/stats?stats=statsSingleSeason");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            JObject skaterStatsInfo = JObject.Parse(await response.Content.ReadAsStringAsync());
        //            JToken skaterStats = skaterStatsInfo["stats"][0]["splits"][0]["stat"];

        //            if(skater.PositionShort != "G")
        //            {
        //                skater.Games = (int)skaterStats["games"];
        //                skater.Goals = (int)skaterStats["goals"];
        //                skater.Assists = (int)skaterStats["assists"];
        //            }
        //            else
        //            {
        //                //TODO: Goalie Logic, might be a good idea to do a view just for goalies
        //            }

        //            skater = await GetYearByYearSkaterStatsAsync(skater);
        //            skater.SelectedYearRange = skater.YearRange.LastOrDefault();
                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"\tERROR {0}", ex.Message);
        //    }

        //    return skater;
        //}

        public async Task<Skater> GetYearByYearSkaterStatsAsync(Skater skater)
        {
            skater.YearRange = new List<string>();
            Uri uri = new Uri(string.Format(Constants.personUri, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{uri}{skater.SkaterId}/stats?stats=yearByYear");
                if (response.IsSuccessStatusCode)
                {
                    JObject skaterStatsInfo = JObject.Parse(await response.Content.ReadAsStringAsync());
                    JToken skaterStats = skaterStatsInfo["stats"][0]["splits"];
                    foreach (JToken split in skaterStats.Children())
                    {
                        if(split["team"]["id"] != null && (int)split["team"]["id"] == 14)
                        {
                            skater.YearRange.Add((string)split["season"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return skater;
        }

        public async Task<Skater> GetSkaterStatsBySeasonAsync(string season, Skater skater)
        {
            Uri uri = new Uri(string.Format(Constants.personUri, string.Empty));
            //Skater skater = new Skater();

            try
            {
                HttpResponseMessage response = await client.GetAsync($"{uri}{skater.SkaterId}/stats?stats=statsSingleSeason&season={season}");
                if (response.IsSuccessStatusCode)
                {
                    JObject skaterStatsInfo = JObject.Parse(await response.Content.ReadAsStringAsync());
                    JToken skaterStats = skaterStatsInfo["stats"][0]["splits"][0]["stat"];

                    skater.IsGoalie = (skater.PositionShort == "G");
                    skater.Games = (int)skaterStats["games"];

                    if (!skater.IsGoalie)
                    {
                        skater.Goals = (int)skaterStats["goals"];
                        skater.Assists = (int)skaterStats["assists"];
                    }
                    else
                    {
                        skater.Shutouts = (int)skaterStats["shutouts"];
                        skater.Saves = (int)skaterStats["saves"];
                        skater.SavePercentage = (int)skaterStats["savePercentage"];
                    }

                    skater = await GetYearByYearSkaterStatsAsync(skater);
                    skater.SelectedYearRange = skater.YearRange.LastOrDefault();

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
