using System;
using System.Collections.Generic;
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


        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<keep>>> getAllVaultKeeps(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                List<VaultKeep> vaultKeeps = _vks.getAllVaultKeeps(id, user?.Id);
                return Ok(vaultKeeps);
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
                string message = _vks.Delete(id, user);
                return Ok(message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<VaultKeep>> getVaultKeepById(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                VaultKeep vaultkeep = _vks.getVaultKeepById(id, user?.Id);
                return Ok(vaultkeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}