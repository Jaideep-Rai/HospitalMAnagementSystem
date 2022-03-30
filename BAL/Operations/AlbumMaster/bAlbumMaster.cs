using BAL.Interfaces.AlbumMaster;
using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Album;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Operations.AlbumMaster
{
    public class bAlbumMaster : AppDbContext, bIAlbumMaster, IDisposable

    {
        public async Task<DataResponse> EditAlbumAsync(int? id, AlbumDTO data)
        {
            _sqlCommand.Clear_CommandParameter();
            _sqlCommand.Add_Parameter_WithValue(data);
            var _spName = "album_update";
            if (!id.HasValue)
            {
                _spName = "album_save";
            }
            return await _sqlCommand.AddOrEditWithStoredProcedure(_spName, null, data, "prm_");
        }
        public async Task<AlbumDTOVM> GetAlbumAsync(int? albumId)
        {
            _sqlCommand.Clear_CommandParameter();
            _sqlCommand.Add_Parameter_WithValue("prm_albumId", albumId==0?null:albumId);
            return new AlbumDTOVM
            {
                AlbumDTO = DataTableVsListOfType.ConvertDataTableToList<AlbumDTO>(
                    await _sqlCommand.Select_Table("album_get", CommandType.StoredProcedure))
            };
        }
        public async Task<bool> DeleteAlbumAsync(int id)
        {
            _sqlCommand.Clear_CommandParameter();
            _sqlCommand.Add_Parameter_WithValue("prm_id", id);
            return await _sqlCommand.Execute_Query("album_delete", CommandType.StoredProcedure);
        }

        public async Task<object> ChangeStatusAsync(int id, string status)
        {
            _sqlCommand.Clear_CommandParameter();
            _sqlCommand.Add_Parameter_WithValue("prm_id", id);
            _sqlCommand.Add_Parameter_WithValue("prm_status", status);
            return await _sqlCommand.Execute_Query("album_changestatus", CommandType.StoredProcedure);
        }

        public void Dispose()
        {
            CloseContext();
        }
    }

}
