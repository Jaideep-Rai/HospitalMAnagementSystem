using DTO.BedMaster;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces.BedMaster
{
    public interface IBedMasterService
    {
        Task<DataResponse> Insert(BedMasterDTO medicineMaster);
        Task<DataResponse> Update(BedMasterDTO medicineMaster);
        Task<bool> Delete(int id);
        Task<DataTable> Get(int? id);
    }
}
