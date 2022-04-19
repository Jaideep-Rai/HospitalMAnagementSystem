using BAL.Interfaces.MedicineMaster;
using DTO.MedicineMaster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.MedicineMaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineMasterController : ControllerBase
    {
        public readonly IMedicineMasterService _iMedicineMasterService;
        public MedicineMasterController(IMedicineMasterService iMedicineMasterService)
        {
            _iMedicineMasterService = iMedicineMasterService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest("Please provide role id.");

            return Ok(await _iMedicineMasterService.Delete(id));
        }
        [HttpPost]
        public async Task<IActionResult> Insert(MedicineMasterDTO medicineMaster)
        {
            return Ok(await _iMedicineMasterService.Insert(medicineMaster));
        }

        [HttpPut]
        public async Task<IActionResult> Update(MedicineMasterDTO medicineMaster)
        {
            return Ok(await _iMedicineMasterService.Update(medicineMaster));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _iMedicineMasterService.Get(id));
        }

    }
}
