using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.MedicineMaster
{
    public class MedicineMasterDTO
    {
        public int id { get; set; }
        public string medicinename { get; set; }
        public int mg { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
    }
}
