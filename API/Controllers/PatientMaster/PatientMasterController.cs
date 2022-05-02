using BAL.Interfaces.PatientMaster;
using DTO.PatientMasterDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.PatientMaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMasterController : ControllerBase
    {
        public readonly IPatientMasterService _iPatientMasterService;
        public PatientMasterController(IPatientMasterService iPatientMasterService)
        {
            _iPatientMasterService = iPatientMasterService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(PatientMasterDTO patientMaster)
        {
            return Ok(await _iPatientMasterService.Insert(patientMaster));
        }

        [HttpPut]
        public async Task<IActionResult> Update(PatientMasterDTO patientMaster)
        {
            return Ok(await _iPatientMasterService.Update(patientMaster));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _iPatientMasterService.Get(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest("Please provide id.");

            return Ok(await _iPatientMasterService.Delete(id));
        }
        
    }
}
