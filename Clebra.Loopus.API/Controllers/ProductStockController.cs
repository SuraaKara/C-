
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
    public class ProductStockController : ControllerBase
    {
        private readonly IProductStockDataService productStockDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ProductStockController(IProductStockDataService productStockDataService)
        {
            this.productStockDataService = productStockDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid productStockId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var productStock = await productStockDataService.GetAsync(g => g.Id == productStockId);
                if (productStock != null)
                    return Ok(productStock);
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
                var productStocks = await productStockDataService.GetListAsync(g => g.IsActive);

                if (productStocks?.Count() > 0)
                    return Ok(productStocks);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] ProductStock entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await productStockDataService.SubmitChangeAsync(entity);
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
