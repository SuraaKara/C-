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
        public class CommentController : ControllerBase
        {
            private readonly ICommentDataService commentDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public CommentController(ICommentDataService commentDataService)
            {
                this.commentDataService = commentDataService;
            }

            [HttpGet("[action]")]
            public async Task<IActionResult> GetAsync([FromHeader] Guid commentId)
            {
                try
                {
                logger.Info("İşlem Gerçekleşti");
                var comment = await commentDataService.GetAsync(g => g.Id == commentId);
                    if (comment != null)
                        return Ok(comment);
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
                var comment = await commentDataService.GetListAsync(g => g.IsActive);

                    if (comment?.Count() > 0)
                        return Ok(comment);
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
            public async Task<IActionResult> SubmitChangeAsync([FromBody] Comment entity)
            {
                try
                {
                logger.Info("İşlem Gerçekleşti");
                await commentDataService.SubmitChangeAsync(entity);
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

