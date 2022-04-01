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
        Task<DataTable> GetJsonReportForUser(string reportId);
        Task<DataTable> archiveSelectedReports(int reportId);
        Task<DataTable> GetAllDataFromReportContent();
        Task<DataTable> restoreSelectedReports(int reportId);
        string SaveReportUrl(Report_DTO data);
        Task<DataTable> GetReporturl();
        Task<DataTable> GetUrlLinkAndJsonAliasName(int queryid);
    }
}
