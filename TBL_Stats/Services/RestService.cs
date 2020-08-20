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
    public class RestService
    {
        HttpClient client;
        public Team Team { get; private set; }
        private Uri PersonUri = new Uri(string.Format(Constants.personUri, string.Empty));

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
                            PositionShort = (string)skaterInfo["position"]["abbreviation"],
                            JerseyNumber = (int)skaterInfo["jerseyNumber"]
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

        public async Task<Skater> GetYearByYearSkaterStatsAsync(Skater skater)
        {
            skater.YearRange = new List<string>();
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{PersonUri}{skater.SkaterId}/stats?stats=yearByYear");
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

        public async Task<Skater> GetSkaterStatsBySeasonAsync(Skater skater, string season)
        {
            skater = await GetRegularSeasonSkaterStats(skater, season);
            skater = await GetSkaterPlayoffStats(skater, season);

            return skater;

        }

        public async Task<Skater> GetRegularSeasonSkaterStats(Skater skater, string season)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{PersonUri}{skater.SkaterId}/stats?stats=statsSingleSeason&season={season}");
                if (response.IsSuccessStatusCode)
                {
                    JObject skaterStatsInfo = JObject.Parse(await response.Content.ReadAsStringAsync());
                    JToken skaterStats = skaterStatsInfo["stats"][0]["splits"][0]["stat"];

                    skater.IsGoalie = (skater.PositionShort == "G");

                    if (!skater.IsGoalie)
                    {
                        skater.RegularSeasonSkaterStats = ConvertJsonToSkaterStats(skaterStats);
                    }
                    else
                    {
                        skater.RegularSeasonGoalieStats = ConvertJsonToGoalieStats(skaterStats);
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

        public async Task<Skater> GetSkaterPlayoffStats(Skater skater, string season)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{PersonUri}{skater.SkaterId}/stats?stats=statsSingleSeasonPlayoffs&season={season}");
                if (response.IsSuccessStatusCode)
                {
                    JObject skaterStatsInfo = JObject.Parse(await response.Content.ReadAsStringAsync());
                    JToken skaterStats = skaterStatsInfo["stats"][0]["splits"][0]["stat"];

                    skater.IsGoalie = (skater.PositionShort == "G");

                    if (!skater.IsGoalie)
                    {
                        skater.PlayoffSkaterStats = ConvertJsonToSkaterStats(skaterStats);
                    }
                    else
                    {
                        skater.PlayoffGoalieStats = ConvertJsonToGoalieStats(skaterStats);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return skater;
        }

        private GoalieStats ConvertJsonToGoalieStats(JToken json)
        {
            return new GoalieStats
            {
                Games = (int)json["games"],
                Shutouts = (int)json["shutouts"],
                Saves = (int)json["saves"],
                SavePercentage = Math.Round((decimal)json["savePercentage"], 2),
                PowerPlaySaves = (int)json["powerPlaySaves"],
                ShotsAgainst = (int)json["shotsAgainst"],
                ShortHandedSaves = (int)json["shortHandedSaves"],
                EvenStrengthSavePercentage = Math.Round((decimal)json["evenStrengthSavePercentage"], 2),
                PowerPlaySavePercentage = Math.Round((decimal)json["powerPlaySavePercentage"], 2),
                ShortHandedSavePercentage = Math.Round((decimal)json["shortHandedSavePercentage"], 2),

            };
        }

        private SkaterStats ConvertJsonToSkaterStats(JToken json)
        {
            return new SkaterStats
            {
                Games = (int)json["games"],
                Goals = (int)json["goals"],
                Assists = (int)json["assists"]
            };
        }
    }
}
