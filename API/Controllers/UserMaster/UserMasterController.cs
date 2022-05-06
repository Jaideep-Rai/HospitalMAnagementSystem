using BAL.Interfaces.UserMaster;
using DTO.Models;
using DTO.UserMaster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.UserMaster
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserMasterController : Controller
    {
        public readonly IUserMasterService _iUserMasterService;
        public UserMasterController(IUserMasterService iUserMasterService)
        {
            _iUserMasterService = iUserMasterService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginInputs userLoginInputs)
        {
           return Ok( await _iUserMasterService.Login(userLoginInputs));
        }
         
        //[HttpPost]
        //public async Task<IActionResult> Insert(UserMasterDTO UserMaster)
        //{
        //    return Ok(await _iUserMasterService.Insert(UserMaster));
        //}
        
        //[HttpDelete]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    return Ok(await _iUserMasterService.Delete(id));
        //}
    }
}
