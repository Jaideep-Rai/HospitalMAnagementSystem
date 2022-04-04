using DTO.MedicineMaster;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces.MedicineMaster
{
    public interface IMedicineMasterService
    {
        Task<DataResponse> Insert(MedicineMasterDTO medicineMaster);
        Task<DataResponse> Update(MedicineMasterDTO medicineMaster);
        Task<bool> Delete(int id);
        Task<DataTable> Get(int? id);

    }
}
