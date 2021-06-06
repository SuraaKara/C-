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
    public class BigTextController : ControllerBase
    {
        private readonly IBigTextDataService bigTextDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public BigTextController(IBigTextDataService bigTextDataService)
        {
            this.bigTextDataService = bigTextDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid bigTextId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var bigText = await bigTextDataService.GetAsync(g => g.Id == bigTextId);
                if (bigText != null)
                    return Ok(bigText);
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
                var bigText = await bigTextDataService.GetListAsync(g => g.IsActive);

                if (bigText?.Count() > 0)
                    return Ok(bigText);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] BigText entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await bigTextDataService.SubmitChangeAsync(entity);
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
