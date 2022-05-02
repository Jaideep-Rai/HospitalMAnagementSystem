using DTO.Models;
using DTO.PatientMasterDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces.PatientMaster
{
    public interface IPatientMasterService
    {
        Task<DataResponse> Insert(PatientMasterDTO patientMaster);
        Task<DataResponse> Update(PatientMasterDTO patientMaster);
        Task<bool> Delete(int id);
        Task<DataTable> Get(int? id);
    }
}
