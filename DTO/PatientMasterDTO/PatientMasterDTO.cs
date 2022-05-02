using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.PatientMasterDTO
{
    public class PatientMasterDTO
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string dateofbirth { get; set; }
        public string bloodgroup { get; set; }
        public string maritalstatus { get; set; }
        public string phoneno { get; set; }
        public string address { get; set; }
    }
}
