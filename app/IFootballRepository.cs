using System.Collections.Generic;
using System.Threading.Tasks;
using models;

namespace app
{
    public interface IFootballRepository
    {
        public Task<Player> GetPlayerById(string playerId);
        
        public Task<List<Player>> GetAllPlayers();
        
        public Task<List<Player>> FindPlayersByPositionId(string positionId);

        public Task<List<Position>> GetPositions();

        public Task<Roster> GetRoster(int year);
    }
}