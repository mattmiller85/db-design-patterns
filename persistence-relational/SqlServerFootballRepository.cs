using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app;
using Microsoft.EntityFrameworkCore;
using models;

namespace persistence_relational
{
    public class SqlServerFootballRepository : IFootballRepository
    {
        private readonly WvuFootballDataContext _dataContext;

        public SqlServerFootballRepository(WvuFootballDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Player> GetPlayerById(string playerId)
        {
            return await _dataContext.Players
                .Include(p => p.Position)
                .Include(p => p.PlayerRosters)
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            return await _dataContext.Players
                .Include(p => p.Position)
                .ToListAsync();
        }

        public async Task<List<Player>> FindPlayersByPositionId(string positionId)
        {
            return await _dataContext.Players
                .Where(p => p.Position.Id == positionId)
                .ToListAsync();
        }

        public async Task<List<Position>> GetPositions()
        {
            return await _dataContext.Positions.ToListAsync();
        }

        public async Task<Roster> GetRoster(int year)
        {
            var players = await _dataContext.PlayerRosters
                .Include(r => r.Roster)
                .Include(r => r.Player)
                .ThenInclude(p => p.Position)
                .Where(r => r.Roster.Year == year)
                .ToListAsync();

            var roster = players.FirstOrDefault()?.Roster;
            if (roster != null)
            {
                roster.PlayerRosters = null;
                roster.Players = players.Select(p => p.Player).ToList();
                foreach (var rosterPlayer in roster.Players)
                {
                    rosterPlayer.PlayerRosters = null;
                }
            }

            return players.Any() ? roster : (await _dataContext.Rosters.FirstOrDefaultAsync(r => r.Year == year));
        }
    }
}