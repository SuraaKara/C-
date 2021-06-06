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
    public class ColorController : ControllerBase
    {
        private readonly IColorDataService colorDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ColorController(IColorDataService colorDataService)
        {
            this.colorDataService = colorDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid colorId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var colors = await colorDataService.GetAsync(g => g.Id == colorId);
                if (colors != null)
                    return Ok(colors);
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
                var colors = await colorDataService.GetListAsync(g => g.IsActive);

                if (colors?.Count() > 0)
                    return Ok(colors);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] Color entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await colorDataService.SubmitChangeAsync(entity);
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
