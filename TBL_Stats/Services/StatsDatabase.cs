using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBL_Stats.Models;

namespace TBL_Stats.Services
{
    public class StatsDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public StatsDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            //_database.CreateTableAsync<Skater>().Wait();
            _database.CreateTableAsync<SkaterStats>().Wait();
            _database.CreateTableAsync<GoalieStats>().Wait();
        }
        
        public async Task<int> SyncSkaterStats(Skater skater)
        {
            if(skater.IsGoalie)
            {
                return await _database.InsertOrReplaceAsync(skater.RegularSeasonGoalieStats);
            }
            else
            {
                return await _database.InsertOrReplaceAsync(skater.RegularSeasonSkaterStats);
            }
        }

        public async Task<SkaterStats> GetSavedSkaterStatsAsync(int skaterId)
        {
            return await _database.GetAsync<SkaterStats>(skaterId);
        }

        public async Task<GoalieStats> GetSavedGoalieStatsAsync(int skaterId)
        {
            return await _database.GetAsync<GoalieStats>(skaterId);
        }
    }
}
