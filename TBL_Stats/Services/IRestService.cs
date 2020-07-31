using System.Collections.Generic;
using System.Threading.Tasks;
using TBL_Stats.Models;

namespace TBL_Stats.Services
{
    public interface IRestService
    {
        Task<Team> GetTeamStatsAsync();
        Task<List<Skater>> GetRosterAsync();
        Task<Skater> GetSkaterAsync(Skater skater);
        Task<Skater> GetYearByYearSkaterStatsAsync(Skater skater);

        Task<Skater> GetSkaterStatsBySeasonAsync(string season, Skater skater);
    }
}