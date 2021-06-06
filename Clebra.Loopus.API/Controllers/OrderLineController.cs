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
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineDataService orderLineDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public OrderLineController(IOrderLineDataService orderLineDataService)
        {
            this.orderLineDataService = orderLineDataService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid orderLineId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var orderLine = await orderLineDataService.GetAsync(g => g.Id == orderLineId);
                if (orderLine != null)
                    return Ok(orderLine);
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
                var orderLines = await orderLineDataService.GetListAsync(g => g.IsActive);

                if (orderLines?.Count() > 0)
                    return Ok(orderLines);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] OrderLine entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await orderLineDataService.SubmitChangeAsync(entity);
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
