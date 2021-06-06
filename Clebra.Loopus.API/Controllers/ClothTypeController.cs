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
    public class ClothTypeController : ControllerBase
    {
        private readonly IClothTypeDataService clothTypeDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ClothTypeController(IClothTypeDataService clothTypeDataService)
        {
            this.clothTypeDataService = clothTypeDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid clothTypeId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var clothType = await clothTypeDataService.GetAsync(g => g.Id == clothTypeId);
                if (clothType != null)
                    return Ok(clothType);
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
                var clothType = await clothTypeDataService.GetListAsync(g => g.IsActive);

                if (clothType?.Count() > 0)
                    return Ok(clothType);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] ClothType entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await clothTypeDataService.SubmitChangeAsync(entity);
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
