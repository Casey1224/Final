using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Final.Models;
using Final.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Final.Models.keep;

namespace Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly vaultService _vs;
        private readonly vaultKeepService _vks;

        public VaultsController(vaultService vs, vaultKeepService vks)
        {
            _vs = vs;
            _vks = vks;
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
        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<keepvm>>> getAllVaultKeeps(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                List<keepvm> vaultKeeps = _vs.getAllVaultKeeps(id, user.Id);
                return Ok(vaultKeeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}