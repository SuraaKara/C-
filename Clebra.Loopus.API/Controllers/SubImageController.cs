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
    public class SubImageController : ControllerBase
    {
      
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        private ISubImageDataService subImageDataService;

        public SubImageController(ISubImageDataService subImageDataService)
        {
            this.subImageDataService = subImageDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid subImageId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var subImage = await subImageDataService.GetAsync(g => g.Id == subImageId);
                if (subImage != null)
                    return Ok(subImage);
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
                var subImage = await subImageDataService.GetListAsync(g => g.IsActive);

                if (subImage?.Count() > 0)
                    return Ok(subImage);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] SubImage entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await subImageDataService.SubmitChangeAsync(entity);
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
