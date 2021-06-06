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
    public class ProductController : ControllerBase
    {
        private readonly IProductDataService productDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ProductController(IProductDataService productDataService)
        {
            this.productDataService = productDataService;
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader]Guid productId)
        {
            try
            {
                logger.Info("��lem Ger�ekle�ti");
                var product = await productDataService.GetAsync(g => g.Id == productId);
                if (product != null)
                    return Ok(product);
                else
                    return NoContent();
            }
            catch (Exception e)
            {
                logger.Error(e, "HATA");
                return BadRequest(e);
            }

        }
        

        [HttpGet( "[action]")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                logger.Info("��lem Ger�ekle�ti");
                var products = await productDataService.GetListAsync(g => g.IsActive);

                if (products?.Count() > 0)
                    return Ok(products);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody]Product entity)
        {
            try
            {
                logger.Info("��lem Ger�ekle�ti");
                await productDataService.SubmitChangeAsync(entity);
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