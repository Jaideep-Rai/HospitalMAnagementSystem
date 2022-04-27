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
        Task<DataResponse> Insert(UserMasterDTO UserMaster);
        Task<bool> Delete(int id);
    }
}
