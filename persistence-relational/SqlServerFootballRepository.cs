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
            return await _dataContext.Players.FindAsync(playerId);
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            return await _dataContext.Players.ToListAsync();
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
            return await _dataContext.Rosters
                .FirstOrDefaultAsync(r => r.Year == year);
        }
    }
}