using BAL.Interfaces.BedMaster;
using DTO.BedMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.BedMaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedMasterController : ControllerBase
    {
        public readonly IBedMasterService _iBedMasterService;
        public BedMasterController(IBedMasterService iBedMasterService)
        {
            _iBedMasterService = iBedMasterService;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest("Please provide id.");

            return Ok(await _iBedMasterService.Delete(id));
        }
        [HttpPost]
        public async Task<IActionResult> Insert(BedMasterDTO bedMaster)
        {
            return Ok(await _iBedMasterService.Insert(bedMaster));
        }

        [HttpPut]
        public async Task<IActionResult> Update(BedMasterDTO bedMaster)
        {
            return Ok(await _iBedMasterService.Update(bedMaster));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _iBedMasterService.Get(id));
        }
    }
}
