using Clebra.Loopus.Model;
using Clebra.Loopus.Service;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Clebra.Loopus.API.Controllers
{
    [Authorize, ApiController, Route("[controller]")]
    public class NeighborhoodController : ControllerBase
    {
        private readonly INeighborhoodDataService neighborhoodDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public NeighborhoodController(INeighborhoodDataService neighborhoodDataService)
        {
            this.neighborhoodDataService = neighborhoodDataService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid neighborhoodId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var neighborhood = await neighborhoodDataService.GetAsync(g => g.Id == neighborhoodId);
                if (neighborhood != null)
                    return Ok(neighborhood);
                else
                    return NoContent();
            }
            catch (Exception e)
            {
                logger.Error(e, "HATA");
                return BadRequest(e);
            }
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var neighborhoods = await neighborhoodDataService.GetListAsync(g => g.IsActive);

                if (neighborhoods?.Count() > 0)
                    return Ok(neighborhoods);
                else
                    return NoContent();
            }
            catch (Exception e)
            {
                logger.Error(e, "HATA");
                return BadRequest(e);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SubmitChangeAsync([FromBody] Neighborhood entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await neighborhoodDataService.SubmitChangeAsync(entity);
                return Ok();
            }
            catch (Exception e)
            {
                logger.Error(e, "HATA");
                return BadRequest(e);
            }

        }

    }
}
