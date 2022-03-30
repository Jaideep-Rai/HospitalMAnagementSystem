using BAL.Interfaces.AlbumMaster;
using Common.Utilities;
using DTO.Models.Album;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace API.Controllers.AlbumMaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumMasterController : ControllerBase
    {
        private readonly bIAlbumMaster _bIAlbumMaster;
        private object media;

        public AlbumMasterController(bIAlbumMaster bIAlbumMaster)
        {
            _bIAlbumMaster = bIAlbumMaster;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] AlbumDTO data)
        {
            if (Request.Form.Files.Count > 0)
            {
                foreach (var file in Request.Form.Files)
                {
                    var ext = Path.GetExtension(file.FileName) == "" ? ".webp" : file.ContentType.Contains("image") ?
                        ".webp" : Path.GetExtension(file.FileName);
                    var filename = file.FileName.Split(".")[0];
                    var folderName = Path.Combine("assets", "MediaLibrary");
                    var filepath = Path.Combine(folderName, Guid.NewGuid() + "_" + filename.Replace(" ", "") + ext);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    data.coverimage = filepath;
                }

            }
            return Ok(await _bIAlbumMaster.EditAlbumAsync(null, data));
        }



        [HttpGet("{id?}")]
        public async Task<IActionResult> GetAsync(int? id)
        {
            return Ok(await _bIAlbumMaster.GetAlbumAsync(id));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PutAsync(int? id, [FromForm] AlbumDTO data)
        {
            if (Request.Form.Files.Count > 0)
            {
                foreach (var file in Request.Form.Files)
                {
                    var ext = Path.GetExtension(file.FileName)=="" ? ".webp":file.ContentType.Contains("image")?
                        ".webp": Path.GetExtension(file.FileName);
                    var filename = file.FileName.Split(".")[0];
                    var folderName = Path.Combine("assets", "MediaLibrary");
                    var filepath = Path.Combine(folderName, Guid.NewGuid() + "_" + filename.Replace(" ", "")+ext);
                     using (var stream = new FileStream(filepath, FileMode.Create))
                         {
                           file.CopyTo(stream);
                         }
                    data.coverimage = filepath;
                }
                
            }
            return Ok(await _bIAlbumMaster.EditAlbumAsync(id, data));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _bIAlbumMaster.DeleteAlbumAsync(id));
        }
        [HttpPost("{id}/{status}")]
        public async Task<IActionResult> ChangeStatusAsync(int id, string status)
        {
            return Ok(await _bIAlbumMaster.ChangeStatusAsync(id, status));
        }
    }
}
