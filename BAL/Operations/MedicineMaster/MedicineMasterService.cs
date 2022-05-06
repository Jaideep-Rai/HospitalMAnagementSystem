using Common.DbContext;
using DTO.MedicineMaster;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces.MedicineMaster
{
    public class MedicineMasterService : MyDbContext, IMedicineMasterService
    {
        public async Task<bool> Delete(int id)
        {
            _MyCommand.Clear_CommandParameter();
            _MyCommand.Add_Parameter_WithValue("prm_id", id);
            return await _MyCommand.Execute_Query("medicine_delete", CommandType.StoredProcedure);
        }

        public async Task<DataTable> Get(int? id)
        {
            _MyCommand.Clear_CommandParameter();
            _MyCommand.Add_Parameter_WithValue("prm_id",id == 0 ? null : id);
            DataTable result= await _MyCommand.Select_Table("medicine_get", CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// Author: Nido Then
        /// Date: 01-Apr-2022
        /// Desc: insert medicine in master table.
        /// </summary>
        /// <param name="medicineMaster"></param>
        /// <returns></returns>

        public async Task<DataResponse> Insert(MedicineMasterDTO medicineMaster)
        {
            try
            {
                return await _MyCommand.AddOrEditWithStoredProcedure("medicine_insert", null, medicineMaster, "prm_");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<DataResponse> Update(MedicineMasterDTO medicineMaster)
        {
            try
            {
                return await _MyCommand.AddOrEditWithStoredProcedure("medicine_update", null, medicineMaster, "prm_");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
