using System;
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


    }
}