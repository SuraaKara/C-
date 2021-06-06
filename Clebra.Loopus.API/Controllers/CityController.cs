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
    public class CityController : ControllerBase
    {
        private readonly ICityDataService cityDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public CityController(ICityDataService cityDataService)
        {
            this.cityDataService = cityDataService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid cityId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var city = await cityDataService.GetAsync(g => g.Id == cityId);
                if (city != null)
                    return Ok(city);
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
                var cities = await cityDataService.GetListAsync(g => g.IsActive);

                if (cities?.Count() > 0)
                    return Ok(cities);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] City entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await cityDataService.SubmitChangeAsync(entity);
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
