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
    public class AddressController : ControllerBase
    {
        private readonly IAddressDataService addressDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public AddressController(IAddressDataService addressDataService)
        {
            this.addressDataService = addressDataService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid addressId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var address = await addressDataService.GetAsync(g => g.Id == addressId);
                if (address != null)
                    return Ok(address);
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
                var address = await addressDataService.GetListAsync(g => g.IsActive);

                if (address?.Count() > 0)
                    return Ok(address);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] Address entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await addressDataService.SubmitChangeAsync(entity);
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
