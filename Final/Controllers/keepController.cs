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
    public class keepsController : ControllerBase
    {
        private readonly Services.keepService _ks;

        public keepsController(keepService ks)
        {
            _ks = ks;
        }
        [HttpGet]
        public ActionResult<List<keep>> getAllKeeps()
        {
            try
            {
                List<keep> keeps = _ks.getAllKeeps();
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // [HttpGet("{id}")]
        // public ActionResult<keep> getKeepById(int id)
        // {
        //     try
        //     {
        //         keep oneKeep = _ks.getKeepById(id);
        //         return Ok(oneKeep);
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<keep>> getKeepById(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                keep oneKeep = _ks.getKeepById(id, user?.Id);
                return Ok(oneKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<keep>> createKeep([FromBody] keep keepers)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                keepers.creatorId = user.Id;
                keep keep = _ks.createKeep(keepers);
                keepers.creator = user;
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<keep>> Update(int id, [FromBody] keep keepers)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                keepers.Id = id;
                keep keep = _ks.Update(keepers, user);
                return Ok(keep);
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
                string message = _ks.Delete(id, user);
                return Ok(message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}