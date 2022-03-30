using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.DynamicReportingTool
{
    public class DynamicReportingTool_DTO
    {
        public string reportid { get; set; }
        public string reportname { get; set; }
        public string reportcontent { get; set; }
        public string derivedvariable { get; set; }
        public string viewaspercentvariables { get; set; }
        public string reportdescription { get; set; }
        public string userrole { get; set; }
        public string status { get; set; }
        public string createdon { get; set; }
        public string createdby { get; set; }
        public string updatedby { get; set; }
        public string updatedon { get; set; }
        public string publishas { get; set; }
    }
    public class Report_DTO
    {
        public int queryid { get; set; }
        public string queryname { get; set; }
        public string query { get; set; }
        public int apiid { get; set; }
        public string apiname { get; set; }
        public string apilink { get; set; }
        public string jsonaliasname { get; set; }
        public DateTime createdon { get; set; }
        public string createdby { get; set; }
    }
}
