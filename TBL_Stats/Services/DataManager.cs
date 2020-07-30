﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBL_Stats.Models;

namespace TBL_Stats.Services
{
    public class DataManager
    {
        IRestService restService;

        public DataManager(IRestService service)
        {
            restService = service;
        }

        public Task<Team> GetTeamAsync()
        {
            return restService.GetTeamStatsAsync();
        }

        public Task<List<Skater>> GetSkatersAsync()
        {
            return restService.GetRosterAsync();
        }

        public Task<Skater> GetSkaterAsync(Skater skater)
        {
            return restService.GetSkaterAsync(skater);
        }
    }
}
