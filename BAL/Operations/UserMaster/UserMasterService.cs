using BAL.Interfaces.UserMaster;
using Common.DbContext;
using DTO.Models;
using DTO.UserMaster;
using Microsoft.AspNetCore.Identity; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace BAL.Operations.UserMaster
{
    public class UserMasterService : IUserMasterService
    {
        readonly UserManager<ApplicationUser> _userManager; 
        readonly RoleManager<IdentityRole> _roleManager;

        public UserMasterService(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager ?? throw new NullReferenceException(nameof(userManager)); 
            _roleManager = roleManager ?? throw new NullReferenceException(nameof(roleManager));
        }
        
        public async Task<DataResponse> Login(UserLoginInputs userLoginInputs)
        {
            var _user = await _userManager.FindByEmailAsync(userLoginInputs.Email);
            if(_user == null)
            {
                return new DataResponse("Email Id not found", false);
            }

            var _isValidLogin = await _userManager.CheckPasswordAsync(_user, userLoginInputs.Password);
            if (_isValidLogin)
            {
                // getting role of USer
                var _role = await _userManager.GetRolesAsync(_user);
                _user.Roles = _role.ToList();
                return new DataResponse("Login success", true)
                {
                    Data = _user
                };
            }
            return new DataResponse("Invalid login attempt!", false);
        }

        //public async Task<bool> Delete(int id)
        //{
        //    _MyCommand.Clear_CommandParameter();
        //    _MyCommand.Add_Parameter_WithValue("prm_id", id);
        //    return await _MyCommand.Execute_Query("user_delete", CommandType.StoredProcedure);
        //}

        //public async Task<DataResponse> Insert(UserMasterDTO UserMaster)
        //{
        //    try
        //    {
        //        return await _MyCommand.AddOrEditWithStoredProcedure("user_insert", null, UserMaster, "prm_");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        
    }
}
