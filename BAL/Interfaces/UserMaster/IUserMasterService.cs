using DTO.Models;
using DTO.UserMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces.UserMaster
{
    public interface IUserMasterService
    {
        Task<DataResponse> Login(UserLoginInputs userLoginInputs);
    }
}
