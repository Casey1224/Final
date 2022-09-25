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
    public class VaultsController : ControllerBase
    {
        private readonly vaultService _vs;

        public VaultsController(vaultService vs)
        {
            _vs = vs;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> getVaultById(int id)
        {

            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                Vault oneVault = _vs.getVaultById(id, user?.Id);
                return Ok(oneVault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> createVault([FromBody] Vault vaulters)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                vaulters.creatorId = user.Id;
                Vault vault = _vs.createVault(vaulters);
                vaulters.creator = user;
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault vaulters)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                vaulters.Id = id;
                Vault vault = _vs.Update(vaulters, user);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                string message = _vs.Delete(id, user);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}