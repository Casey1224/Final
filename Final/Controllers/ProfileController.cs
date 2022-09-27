using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Final.Models;
using Final.Services;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfileService _ps;
        private readonly keepService _ks;
        private readonly vaultService _vs;

        public ProfilesController(ProfileService ps, keepService ks, vaultService vs)
        {
            _ps = ps;
            _ks = ks;
            _vs = vs;
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Account>> GetOne(string id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                Account profile = _ps.GetOne(id, user?.Id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public ActionResult<List<keep>> getKeepByProfileId(string id)
        {
            try
            {

                List<keep> keeps = _ks.getKeepByProfileId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/vaults")]
        public ActionResult<List<Vault>> getVaultsByProfId(string id)
        {
            try
            {
                List<Vault> vaults = _vs.getVaultsByProfId(id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}