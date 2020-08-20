using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TBL_Stats.Models;

namespace TBL_Stats.Services
{
    public class DataManager
    {
        RestService restService;

        public DataManager(RestService service)
        {
            restService = service;
        }

        static StatsDatabase database;
        public static StatsDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new StatsDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Stats.db3"));
                }
                return database;
            }
        }

        public async Task<int> SyncSkaterStatsAsync()
        {
            List<Skater> roster = await GetRosterAsync();
            List<Skater> skaterStats = new List<Skater>();
            foreach(Skater skater in roster)
            {
                skaterStats.Add(await GetSkaterStatsBySeasonAsync("20192020", skater));

            }

            int recordCount = 0;
            foreach(Skater skater in skaterStats)
            {
                recordCount += await database.SyncSkaterStats(skater);
            }

            return recordCount;
        }

        public async Task<Skater> GetSavedSkater(Skater skater)
        {

            if(skater.IsGoalie)
            {
                skater.RegularSeasonGoalieStats = await database.GetSavedGoalieStatsAsync(skater.SkaterId);
            }
            else
            {
                skater.RegularSeasonSkaterStats = await database.GetSavedSkaterStatsAsync(skater.SkaterId);
            }
            return skater;
        }

        public Task<Team> GetTeamAsync()
        {
            return restService.GetTeamStatsAsync();
        }

        public Task<List<Skater>> GetRosterAsync()
        {
            return restService.GetRosterAsync();
        }

        public Task<Skater> GetSkaterStatsBySeasonAsync(string season, Skater skater)
        {
            return restService.GetSkaterStatsBySeasonAsync(skater, season);
        }
    }
}
