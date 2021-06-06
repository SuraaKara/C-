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
    public class  YarnTypeController : ControllerBase
    {
        private readonly IYarnTypeDataService yarnTypeDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public YarnTypeController(IYarnTypeDataService yarnTypeDataService)
        {
            this.yarnTypeDataService = yarnTypeDataService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid yarnTypeId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var yarnType = await yarnTypeDataService.GetAsync(g => g.Id == yarnTypeId);
                if (yarnType != null)
                    return Ok(yarnType);
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
                var yarnTypes = await yarnTypeDataService.GetListAsync(g => g.IsActive);

                if (yarnTypes?.Count() > 0)
                    return Ok(yarnTypes);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] YarnType entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await yarnTypeDataService.SubmitChangeAsync(entity);
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
