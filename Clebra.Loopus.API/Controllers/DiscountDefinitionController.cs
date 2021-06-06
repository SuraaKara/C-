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
    public class DiscountDefinitionController : ControllerBase
    {
        private readonly IDiscountDefinitionDataService discountDefinitionDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public DiscountDefinitionController(IDiscountDefinitionDataService discountDefinitionDataService)
        {
            this.discountDefinitionDataService = discountDefinitionDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid discountDefinitionId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var discountDefinition = await discountDefinitionDataService.GetAsync(g => g.Id == discountDefinitionId);
                if (discountDefinition != null)
                    return Ok(discountDefinition);
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
                var discountDefinition = await discountDefinitionDataService.GetListAsync(g => g.IsActive);

                if (discountDefinition?.Count() > 0)
                    return Ok(discountDefinition);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] DiscountDefinition entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await discountDefinitionDataService.SubmitChangeAsync(entity);
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


