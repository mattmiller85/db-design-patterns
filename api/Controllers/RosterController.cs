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
    public class RosterController : ControllerBase
    {
        private readonly IFootballRepository _wvuFootballRepository;

        public RosterController(IFootballRepository wvuFootballRepository)
        {
            _wvuFootballRepository = wvuFootballRepository;
        }

        [HttpGet("{year}")]
        public async Task<ActionResult<Roster>> Get(int year)
        {
            var roster = await _wvuFootballRepository.GetRoster(year);
            if (roster == null)
            {
                return new NotFoundResult(); // 404
            }

            return roster;
        }
    }
}