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
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictDataService districtDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public DistrictController(IDistrictDataService districtDataService)
        {
            this.districtDataService = districtDataService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid districtId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var district = await districtDataService.GetAsync(g => g.Id == districtId);
                if (district != null)
                    return Ok(district);
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
                var districts = await districtDataService.GetListAsync(g => g.IsActive);

                if (districts?.Count() > 0)
                    return Ok(districts);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] District entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await districtDataService.SubmitChangeAsync(entity);
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
