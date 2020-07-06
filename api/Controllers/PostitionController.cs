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
    public class PositionController : ControllerBase
    {
        private readonly IFootballRepository _wvuFootballRepository;

        public PositionController(IFootballRepository wvuFootballRepository)
        {
            _wvuFootballRepository = wvuFootballRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> Get()
        {
            var positions = await _wvuFootballRepository.GetPositions();
            if (positions == null || !positions.Any())
            {
                return new NotFoundResult(); // 404
            }

            return positions;
        }
    }
}