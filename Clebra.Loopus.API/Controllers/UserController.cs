using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;
using Clebra.Loopus.Model;
using Clebra.Loopus.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NLog;

namespace Clebra.Loopus.API.Controllers
{
    [Authorize, ApiController, Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserDataService userDataService;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration configuration;

        public UserController(IUserDataService userDataService, IConfiguration configuration)
        {
            this.userDataService = userDataService;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> SignIn([FromHeader] string username, [FromHeader] string password)
        {
            try
            {
                var userProxy = await userDataService.SignIn(username, password);
                if (userProxy == null)
                {
                    logger.Warn($"SignIn : {ServiceResultMessages.UserNotFound}");
                    return Ok(new LoginResult()
                    {
                        IsSuccess = false,
                        Message = ServiceResultMessages.UserNotFound,
                    });
                }


                var accessTokenExpireMin = configuration["LoopusToken:AccessTokenExpireMin"];
                var accessTokenKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["LoopusToken:AccessTokenKey"]));
                var accessClaims = new List<Claim>
                {
                    new Claim("Uid", userProxy.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, username),
                };
                var accessTokenCredential = new SigningCredentials(accessTokenKey, SecurityAlgorithms.HmacSha256);
                var accessToken = new JwtSecurityToken(
                    issuer: configuration["LoopusToken:Issuer"],
                    audience: configuration["LoopusToken:Audiences"],
                    claims: accessClaims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(accessTokenExpireMin)),
                    signingCredentials: accessTokenCredential
                );

                logger.Info($"SignIn : {ServiceResultMessages.IsProcessSuccess}");

                return Ok(new LoginResult()
                {
                    IsSuccess = true,
                    Message = ServiceResultMessages.IsProcessSuccess,
                    Token = new JwtSecurityTokenHandler().WriteToken(accessToken)
                });
            }
            catch (Exception e)
            {
                logger.Error($"SignIn : {e.Message} - {e.InnerException?.Message}");
                return BadRequest(e);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync([FromHeader] Guid userId)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                var loopusUser = await userDataService.GetAsync(g => g.Id == userId);
                if (loopusUser != null)
                    return Ok(loopusUser);
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
                var loopusUser = await userDataService.GetListAsync(g => g.IsActive);

                if (loopusUser?.Count() > 0)
                    return Ok(loopusUser);
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
        public async Task<IActionResult> SubmitChangeAsync([FromBody] LoopusUser entity)
        {
            try
            {
                logger.Info("İşlem Gerçekleşti");
                await userDataService.SubmitChangeAsync(entity);
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