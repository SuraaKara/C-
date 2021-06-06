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
    public class ProductFileController : ControllerBase
    {
        private readonly IProductFileDataService productFileDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ProductFileController(IProductFileDataService productFileDataService)
        {
            this.productFileDataService = productFileDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid productFileId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var productFile = await productFileDataService.GetAsync(g => g.Id == productFileId);
                if (productFile != null)
                    return Ok(productFile);
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
                var productFiles = await productFileDataService.GetListAsync(g => g.IsActive);

                if (productFiles?.Count() > 0)
                    return Ok(productFiles);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] ProductFile entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await productFileDataService.SubmitChangeAsync(entity);
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
