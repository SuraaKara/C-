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
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryDataService productCategoryDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ProductCategoryController(IProductCategoryDataService productCategoryDataService)
        {
            this.productCategoryDataService = productCategoryDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid productCategoryId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var productCategory = await productCategoryDataService.GetAsync(g => g.Id == productCategoryId);
                if (productCategory != null)
                    return Ok(productCategory);
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
                var productCategories = await productCategoryDataService.GetListAsync(g => g.IsActive);

                if (productCategories?.Count() > 0)
                    return Ok(productCategories);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] ProductCategory entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await productCategoryDataService.SubmitChangeAsync(entity);
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
