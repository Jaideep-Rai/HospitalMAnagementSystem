using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.BedMaster
{
    public class BedMasterDTO
    {
        public int id { get; set; }
        public string bedtype { get; set; }
        public int capacity { get; set; }
        public string description { get; set; }
        public int charge { get; set; }
        public string status { get; set; }
    }
}
