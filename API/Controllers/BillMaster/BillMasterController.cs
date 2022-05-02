using BAL.Interfaces.BillMaster;
using DTO.BillMasterDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.BillMaster
{
        [Route("api/[controller]")]
        [ApiController] 
        public class BillMasterController : ControllerBase
        {
            public readonly IBillMaster _iBillMaster;
            public BillMasterController(IBillMaster iBillMaster)
            {
                _iBillMaster = iBillMaster;
            }
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                if (id == 0)
                    return BadRequest("Please provide id.");

                return Ok(await _iBillMaster.Delete(id));
            }
            [HttpPost]
            public async Task<IActionResult> Insert(BillMasterDTO billMaster)
            {
                return Ok(await _iBillMaster.Insert(billMaster));
            }

            [HttpPut]
            public async Task<IActionResult> Update(BillMasterDTO billMaster)
            {
                return Ok(await _iBillMaster.Update(billMaster));
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                return Ok(await _iBillMaster.Get(id));
            }
        }
    }
