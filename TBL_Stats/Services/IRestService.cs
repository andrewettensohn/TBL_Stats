﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TBL_Stats.Models;

namespace TBL_Stats.Services
{
    public interface IRestService
    {
        Task<Team> GetTeamNameAsync();
        Task<List<Skater>> GetSkatersAsync();
    }
}