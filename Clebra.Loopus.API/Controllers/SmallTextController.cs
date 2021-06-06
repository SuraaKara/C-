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
    public class SmallTextController : ControllerBase
    {
        private readonly ISmallTextDataService smallTextDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SmallTextController(ISmallTextDataService smallTextDataService)
        {
            this.smallTextDataService = smallTextDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid smallTextId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var smallText = await smallTextDataService.GetAsync(g => g.Id == smallTextId);
                if (smallText != null)
                    return Ok(smallText);
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
                var smallText = await smallTextDataService.GetListAsync(g => g.IsActive);

                if (smallText?.Count() > 0)
                    return Ok(smallText);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] SmallText entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await smallTextDataService.SubmitChangeAsync(entity);
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
