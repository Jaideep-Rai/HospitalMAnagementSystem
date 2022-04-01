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
                Await_MyCommand.Clear_CommandParameter();
                return await  Await_MyCommand.Select_Table(query, CommandType.Text);
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
                Await_MyCommand.Clear_CommandParameter();
                string InsertReportContentQuery = "INSERT INTO reportcontent(reportid,reportname,reportcontent,derivedvariable,viewaspercentvariables,userrole,reportdescription,status,createdon,createdby,updatedon,updatedby)" +
                "values(@prm_reportid,@prm_reportname, @prm_reportcontent,@prm_derivedvariable, @prm_viewaspercentvariables,@prm_userrole,@prm_reportdescription,@prm_status,curdate(),@prm_createdby,curdate(),@prm_updatedby); ";

                string UpdateReportContentQuery = "Update reportcontent set reportname =@prm_reportname, " +
                    "reportcontent = @prm_reportcontent," +
                    "derivedvariable=@prm_derivedvariable," +
                    "viewaspercentvariables=@prm_viewaspercentvariables," +
                    "userrole=@prm_userrole," +
                    "reportdescription=@prm_reportdescription," +
                    "status=@prm_status," +
                    "createdon=curdate()," +
                    "createdby=@prm_createdby," +
                    "updatedon=curdate()," +
                    "updatedby=@prm_updatedby" +
                    "where reportid= @prm_reportid";
                Await_MyCommand.Add_Parameter_WithValue("@prm_reportid", data.reportid);
                Await_MyCommand.Add_Parameter_WithValue("@prm_reportname", data.reportname);
                Await_MyCommand.Add_Parameter_WithValue("@prm_reportcontent", data.reportcontent);
                Await_MyCommand.Add_Parameter_WithValue("@prm_derivedvariable", data.derivedvariable);
                Await_MyCommand.Add_Parameter_WithValue("@prm_viewaspercentvariables", data.viewaspercentvariables);
                Await_MyCommand.Add_Parameter_WithValue("@prm_userrole", data.userrole);
                Await_MyCommand.Add_Parameter_WithValue("@prm_reportdescription", data.reportdescription);
                Await_MyCommand.Add_Parameter_WithValue("@prm_status", "Active");
                Await_MyCommand.Add_Parameter_WithValue("@prm_createdby", "admin");
                Await_MyCommand.Add_Parameter_WithValue("@prm_updatedby", "admin");
                if(data.publishas == "new")
                {
                    Await_MyCommand.Execute_Query(InsertReportContentQuery, CommandType.Text);
                }
                else
                {
                    Await_MyCommand.Execute_Query(UpdateReportContentQuery, CommandType.Text);
                }


               
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


        public async Task<DataTable> GetJsonReportForUser(string reportId)
        {
            try
            {
                Await_MyCommand.Clear_CommandParameter();
                return await Await_MyCommand.Select_Table($"select * from reportcontent where reportid = '{reportId}'", CommandType.Text);
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

        public async Task<DataTable> archiveSelectedReports(int reportId)
        {
            try
            {
                Await_MyCommand.Clear_CommandParameter();
                string updateReportActionQuery = "update reportcontent SET status = 'Deactive' where reportId = " + reportId;
                return await Await_MyCommand.Select_Table(updateReportActionQuery, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "bDynamicReportingTool", "deleteSelectedReports"));
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
                Await_MyCommand.Clear_CommandParameter();
                return await Await_MyCommand.Select_Table("SELECT * FROM reportcontent", CommandType.Text); ;
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

        public async Task<DataTable> restoreSelectedReports(int reportId)
        {
            try
            {
                Await_MyCommand.Clear_CommandParameter();
                string updateReportActionQuery = "update reportcontent SET status = 'Active' where reportId = " + reportId;
                return await Await_MyCommand.Select_Table(updateReportActionQuery, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "bDynamicReportingTool", "restoreSelectedReports"));
            }

            finally
            {
                Close_MyDbContext();
            }
        }

        public string SaveReportUrl(Report_DTO data)
        {
            try
            {
                Await_MyCommand.Clear_CommandParameter();
                string updateReportAliasnameQuery = "UPDATE querytable SET jsonaliasname= @prm_jsonAliasName where queryid=@prm_queryid";
                Await_MyCommand.Add_Parameter_WithValue("@prm_queryid", data.queryid);
                Await_MyCommand.Add_Parameter_WithValue("@prm_jsonAliasName", data.jsonaliasname);

                Await_MyCommand.Execute_Query(updateReportAliasnameQuery, CommandType.Text);
                return "true";
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "bDynamicReportingTool", "SaveReportUrl"));
            }

            finally
            {
                Close_MyDbContext();
            }
        }

        public async Task<DataTable> GetReporturl()
        {
            try
            {
                Await_MyCommand.Clear_CommandParameter();
                return await Await_MyCommand.Select_Table("select * from querytable", CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "bDynamicReportingTool", "GetReporturl"));
            }

            finally
            {
                Close_MyDbContext();
            }
        }

        public async Task<DataTable> GetUrlLinkAndJsonAliasName(int queryid)
        {
            try
            {
                Await_MyCommand.Clear_CommandParameter();
                return await Await_MyCommand.Select_Table($"select * from querytable where queryid = '{queryid}'", CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "BAL", "bDynamicReportingTool", "GetUrlLinkAndJsonAliasName"));
            }

            finally
            {
                Close_MyDbContext();
            }
        }

    }
}
