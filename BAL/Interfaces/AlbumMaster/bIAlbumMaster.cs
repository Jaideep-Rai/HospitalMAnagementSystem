using DTO.Models;
using DTO.Models.Album;
using System.Threading.Tasks;

namespace BAL.Interfaces.AlbumMaster
{
    public interface bIAlbumMaster
    {
        Task<DataResponse> EditAlbumAsync(int? id, AlbumDTO data);
        Task<AlbumDTOVM> GetAlbumAsync(int? albumId);
        Task<bool> DeleteAlbumAsync(int id);
        Task<object> ChangeStatusAsync(int id, string status);
    }
}
