using DTO.MedicineMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces.MedicineMaster
{
    public interface IMedicineMasterService
    {
        Task<bool> Insert(MedicineMasterDTO medicineMaster);
        Task<bool> Delete(int id);

    }
}
