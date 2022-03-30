using System.Collections.Generic;

namespace DTO.Models.Album
{
    public class AlbumDTO
    {
        public int id { get; set; }
        public string albumname { get; set; }
        public string coverimage { get; set; }
        public string status { get; set; }
        public string albumtype { get; set; }
    }
    public class AlbumDTOVM
    {
        public string[] Errors { get; set; }
        public bool HasError => Errors != null && Errors.Length > 0;
        public IList<AlbumDTO> AlbumDTO { get; set; }
    }
}
