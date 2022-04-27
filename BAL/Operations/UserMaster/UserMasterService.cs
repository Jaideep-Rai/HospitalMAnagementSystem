using BAL.Interfaces.UserMaster;
using Common.DbContext;
using DTO.Models;
using DTO.UserMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Operations.UserMaster
{
    public class UserMasterService : MyDbContext, IUserMasterService
    {
        public async Task<bool> Delete(int id)
        {
            _MyCommand.Clear_CommandParameter();
            _MyCommand.Add_Parameter_WithValue("prm_id", id);
            return await _MyCommand.Execute_Query("user_delete", CommandType.StoredProcedure);
        }

        public async Task<DataResponse> Insert(UserMasterDTO UserMaster)
        {
            try
            {
                return await _MyCommand.AddOrEditWithStoredProcedure("user_insert", null, UserMaster, "prm_");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
