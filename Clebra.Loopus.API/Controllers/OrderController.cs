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
    public class OrderController : ControllerBase
    {
        private readonly IOrderDataService orderDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public OrderController(IOrderDataService orderDataService)
        {
            this.orderDataService = orderDataService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid orderId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var order = await orderDataService.GetAsync(g => g.Id == orderId);
                if (order != null)
                    return Ok(order);
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
                var orders = await orderDataService.GetListAsync(g => g.IsActive);

                if (orders?.Count() > 0)
                    return Ok(orders);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] Order entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await orderDataService.SubmitChangeAsync(entity);
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
