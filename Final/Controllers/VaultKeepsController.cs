using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Final.Models;
using Final.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly Services.vaultKeepService _vks;

        public VaultKeepsController(vaultKeepService vks)
        {
            _vks = vks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> createVaultKeep([FromBody] VaultKeep VaultKeepers)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                VaultKeepers.creatorId = user.Id;
                VaultKeep vaultKeep = _vks.createVaultKeep(VaultKeepers);

                return Ok(vaultKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}