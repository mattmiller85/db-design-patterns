using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using models;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IFootballRepository _wvuFootballRepository;

        public PlayerController(IFootballRepository wvuFootballRepository)
        {
            _wvuFootballRepository = wvuFootballRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            var players = await _wvuFootballRepository.GetAllPlayers();
            if (players == null || !players.Any())
            {
                return new NotFoundResult(); // 404
            }

            return players;
        }
        
        [HttpGet]
        [Route("{playerId}")]
        public async Task<ActionResult<Player>> Get(string playerId)
        {
            var player = await _wvuFootballRepository.GetPlayerById(playerId);
            if (player == null)
            {
                return new NotFoundResult();
            }

            return player;
        }
    }
}