using DTO.BillMasterDTO;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces.BillMaster
{
    public interface IBillMaster
    {
        Task<DataResponse> Insert(BillMasterDTO billMaster);
        Task<DataResponse> Update(BillMasterDTO billMaster);
        Task<bool> Delete(int id);
        Task<DataTable> Get(int? id);
    }
}
