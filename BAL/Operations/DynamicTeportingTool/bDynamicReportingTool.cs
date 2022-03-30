using BAL.Interfaces.DynamicTeportingTool;
using Common.DbContext;
using DTO.Models.DynamicReportingTool;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Operations.DynamicTeportingTool
{
    public class bDynamicReportingTool : MyDbContext, bIDynamicReportingTool
    {
        public async Task<DataTable> GetAllTableDataList(string tableName)
        {
            try
            {
                string query = "SELECT * FROM " + tableName;
                _MyCommand.Clear_CommandParameter();
                return await  _MyCommand.Select_Table(query, CommandType.Text);
                //return list;
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "bDynamicReportingTool", "GetAllList"));
            }

            finally
            {
                Close_MyDbContext();
            }
        }


        public string SaveReport(DynamicReportingTool_DTO data)
        {
            try
            {
                _MyCommand.Clear_CommandParameter();
                string InsertReportContentQuery = "INSERT INTO reportcontentmaster(reportname,reportcontent,createdOn,createdBy,updatedOn,updatedBy)" +
                "values(@prm_reportname, @prm_reportcontent,curdate(),@prm_createdBy,curdate(),@prm_updatedBy); ";

                _MyCommand.Add_Parameter_WithValue("@prm_reportname", data.reportname);
                _MyCommand.Add_Parameter_WithValue("@prm_reportcontent", data.reportcontent);
                //_MyCommand.Add_Parameter_WithValue("@prm_createdOn", );
                _MyCommand.Add_Parameter_WithValue("@prm_createdBy", "admin");
                //_MyCommand.Add_Parameter_WithValue("@prm_updatedOn", curdate());
                _MyCommand.Add_Parameter_WithValue("@prm_updatedBy", "admin");

                _MyCommand.Execute_Query(InsertReportContentQuery, CommandType.Text);
                return "true";
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "bDynamicReportingTool", "GetAllList"));
            }

            finally
            {
                Close_MyDbContext();
            }
        }


        public async Task<DataTable> GetJsonReportForUser()
        {
            try
            {
                _MyCommand.Clear_CommandParameter();
                return await _MyCommand.Select_Table("select * from reportcontentmaster where action = 1", CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "bDynamicReportingTool", "GetJsonReportForUser"));
            }

            finally
            {
                Close_MyDbContext();
            }
        }

        public async Task<DataTable> GetAllDataFromReportContent()
        {
            try
            {
                _MyCommand.Clear_CommandParameter();
                return await _MyCommand.Select_Table("select * from reportcontentmaster", CommandType.Text); ;
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "bDynamicReportingTool", "GetAllDataFromReportContent"));
            }

            finally
            {
                Close_MyDbContext();
            }
        }

        public async Task<string> UpdateReportToShow( int reportId)
        {
            try
            {
                _MyCommand.Clear_CommandParameter();
                string updateReportActionQuery = "update reportcontentmaster SET `action` = 0 where `action` = 1";
                string upadteActionQuery = "update reportcontentmaster SET `action` = 1 where `reportId` = " + reportId;
               //var result=  ;
               
                if(await _MyCommand.Execute_Query(updateReportActionQuery, CommandType.Text))
                {
                    _MyCommand.Execute_Query(upadteActionQuery, CommandType.Text);
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "bDynamicReportingTool", "GetAllList"));
            }

            finally
            {
                Close_MyDbContext();
            }
        }
    }
}
