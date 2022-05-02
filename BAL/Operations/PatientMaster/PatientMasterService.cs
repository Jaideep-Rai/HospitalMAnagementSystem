using BAL.Interfaces.PatientMaster;
using Common.DbContext;
using DTO.Models;
using DTO.PatientMasterDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Operations
{
    public class PatientMasterService : MyDbContext, IPatientMasterService
    {
        public async Task<bool> Delete(int id)
        {
            _MyCommand.Clear_CommandParameter();
            _MyCommand.Add_Parameter_WithValue("prm_id", id);
            return await _MyCommand.Execute_Query("patient_delete", CommandType.StoredProcedure);
        }

        public async Task<DataTable> Get(int? id)
        {
            _MyCommand.Clear_CommandParameter();
            _MyCommand.Add_Parameter_WithValue("prm_id", id == 0 ? null : id);
            DataTable result = await _MyCommand.Select_Table("patient_get", CommandType.StoredProcedure);
            return result;
        }



        public async Task<DataResponse> Insert(PatientMasterDTO patientMaster)
        {
            try
            {
                return await _MyCommand.AddOrEditWithStoredProcedure("patient_insert", null, patientMaster, "prm_");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<DataResponse> Update(PatientMasterDTO patientMaster)
        {
            try
            {
                return await _MyCommand.AddOrEditWithStoredProcedure("patient_update", null, patientMaster, "prm_");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }




    }
}