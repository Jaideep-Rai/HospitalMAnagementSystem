using BAL.Interfaces.BillMaster;
using Common.DbContext;
using DTO.BillMasterDTO;
using DTO.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Operations.BillMaster
{
    public class BillMasterService : MyDbContext, IBillMaster
    {
        public async Task<bool> Delete(int id)
        {
            _MyCommand.Clear_CommandParameter();
            _MyCommand.Add_Parameter_WithValue("prm_id", id);
            return await _MyCommand.Execute_Query("bed_delete", CommandType.StoredProcedure);
        }

        public async Task<DataTable> Get(int? id)
        {
            _MyCommand.Clear_CommandParameter();
            _MyCommand.Add_Parameter_WithValue("prm_id", id == 0 ? null : id);
            DataTable result = await _MyCommand.Select_Table("bed_get", CommandType.StoredProcedure);
            return result;
        }



        public async Task<DataResponse> Insert(BillMasterDTO billMaster)
        {
            try
            {
                return await _MyCommand.AddOrEditWithStoredProcedure("Bed_insert", null, billMaster, "prm_");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<DataResponse> Update(BillMasterDTO billMaster)
        {
            try
            {
                return await _MyCommand.AddOrEditWithStoredProcedure("bed_update", null, billMaster, "prm_");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
