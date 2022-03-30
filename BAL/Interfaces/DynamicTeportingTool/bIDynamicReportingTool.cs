using DTO.Models.DynamicReportingTool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces.DynamicTeportingTool
{
    public interface bIDynamicReportingTool
    {
        Task<DataTable> GetAllTableDataList(string tableName);
        string SaveReport(DynamicReportingTool_DTO data);
        Task<DataTable> GetJsonReportForUser( );
        Task<DataTable> GetAllDataFromReportContent();
        Task<string> UpdateReportToShow(int reportId);
    }
}
