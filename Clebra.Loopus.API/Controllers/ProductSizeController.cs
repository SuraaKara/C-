using System;
using System.Linq;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;
using Clebra.Loopus.Model;
using Clebra.Loopus.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Clebra.Loopus.API.Controllers
{
    [Authorize, ApiController, Route("[controller]")]
    public class ProductSizeController : ControllerBase
    {
        private readonly IProductSizeDataService sizeDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ProductSizeController(IProductSizeDataService sizeDataService)
        {
            this.sizeDataService = sizeDataService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid sizeId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var size = await sizeDataService.GetAsync(g => g.Id == sizeId);
                if (size != null)
                    return Ok(size);
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
                var sizes = await sizeDataService.GetListAsync(g => g.IsActive);

                if (sizes?.Count() > 0)
                    return Ok(sizes);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] ProductSize entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await sizeDataService.SubmitChangeAsync(entity);
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
