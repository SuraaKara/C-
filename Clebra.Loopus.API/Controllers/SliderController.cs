using Clebra.Loopus.Model;
using Clebra.Loopus.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Clebra.Loopus.API.Controllers
{
    [Authorize, ApiController, Route("[controller]")]
    public class SliderController : ControllerBase
    {
        private readonly ISliderDataService sliderDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SliderController(ISliderDataService sliderDataService)
        {
            this.sliderDataService = sliderDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid sliderId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var slider = await sliderDataService.GetAsync(g => g.Id == sliderId);
                if (slider != null)
                    return Ok(slider);
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
                var slider = await sliderDataService.GetListAsync(g => g.IsActive);

                if (slider?.Count() > 0)
                    return Ok(slider);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] Slider entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await sliderDataService.SubmitChangeAsync(entity);
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
