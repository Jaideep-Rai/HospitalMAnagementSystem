using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.BillMasterDTO
{
    public class BillMasterDTO
    {
        public int Id { get; set; }
        public string Patientname { get; set; }
        public int Charges { get; set; }
        public bool Status { get; set; }
    }
}
