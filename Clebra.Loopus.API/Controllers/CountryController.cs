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
    public class CountryController : ControllerBase
    {
        private readonly ICountryDataService countryDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public CountryController(ICountryDataService countryDataService)
        {
            this.countryDataService = countryDataService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid countryId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var country = await countryDataService.GetAsync(g => g.Id == countryId);
                if (country != null)
                    return Ok(country);
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
                var countries = await countryDataService.GetListAsync(g => g.IsActive);

                if (countries?.Count() > 0)
                    return Ok(countries);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] Country entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await countryDataService.SubmitChangeAsync(entity);
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
