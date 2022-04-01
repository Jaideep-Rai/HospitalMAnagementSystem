using Common.DbContext;
using DTO.MedicineMaster;
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
            Await_MyCommand.Clear_CommandParameter();
            Await_MyCommand.Add_Parameter_WithValue("prm_id", id);
            return await Await_MyCommand.Execute_Query("med_delete", CommandType.StoredProcedure);
        }
        public async Task<bool> Insert(MedicineMasterDTO medicineMaster)
        {
            try
            {
                Await_MyCommand.Clear_CommandParameter();
                Await_MyCommand.Add_Parameter_WithValue("prm_medicinename", medicineMaster.medicinename);
                Await_MyCommand.Add_Parameter_WithValue("prm_mg", medicineMaster.mg);
                Await_MyCommand.Add_Parameter_WithValue("prm_price", medicineMaster.price);
                Await_MyCommand.Add_Parameter_WithValue("prm_quantity", medicineMaster.quantity);
                return await Await_MyCommand.Execute_Query("med_insert", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "MedicineMaster", "Insert"));
            }
            
        }
    }
}
