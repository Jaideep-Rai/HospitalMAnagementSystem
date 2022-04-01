using BAL.Interfaces.DynamicTeportingTool;
using Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Newtonsoft.Json;
using DTO.Models.DynamicReportingTool;
using System.Threading.Tasks;

namespace API.Controllers.DynamicTeportingTool
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicReportingToolController : ControllerBase
    {
        private readonly bIDynamicReportingTool _bIDynamicReportingTool;
        public DynamicReportingToolController(bIDynamicReportingTool bIDRT)
        {
            _bIDynamicReportingTool = bIDRT;

        }
        /// <summary>
        /// AUTHOR: Jaideep Roy
        /// created Date:6/12/2020
        /// This API get all the table data
        /// </summary>
        /// <param name="tableName"></param>
        [HttpGet]
        [Route("getAllDataByTableName/{tableName}")]
        public IActionResult GetDataFromTableName(string tableName)
        {
            string errors = null;
            try
            {
                return Ok(_bIDynamicReportingTool.GetAllTableDataList(tableName));
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorCodes.ProcessException(ex, "API", "DynamicReportingToolController", "GetFormDetails", errors));
            }
        }

        /// <summary>
        /// AUTHOR: Jaideep Roy
        /// created Date:15/12/2020
        /// This API saves the report to the db table name 'reportcontent'
        /// </summary>
        [HttpPost]
        [Route("SaveReport")]
        public IActionResult SaveReportContent([FromForm] DynamicReportingTool_DTO data)
        {
            try
            {
                return Ok(_bIDynamicReportingTool.SaveReport(data));

            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "API",
                    "DynamicReportingToolController", "SaveReportContent"));
            }

        }

        /// <summary>
        /// AUTHOR: Jaideep Roy
        /// created Date:20/12/2020
        /// This API get row data from the table reportcontent by the report id
        /// </summary>
        /// <param name="reportId"></param>
        [HttpGet]
        [Route("getAllReportJson/{reportId}")]
        public async Task<IActionResult> GetJsonDataFromTable(string reportId)
        {
            string errors = null;
            try
            {
                var result = await _bIDynamicReportingTool.GetJsonReportForUser(reportId);
                if(result.Rows.Count<=0)
                {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorCodes.ProcessException(ex, "API", "DynamicReportingToolController", "GetFormDetails", errors));
            }
        }

        /// <summary>
        /// AUTHOR: Jaideep Roy
        /// created Date:28/12/2020
        /// This API deletes row data from the table reportcontent by the report id
        /// </summary>
        /// <param name="reportId"></param>
        [HttpDelete]
        [Route("archiveReports/{reportID}")]
        public IActionResult archiveSelectedReports(int reportID)
        {
            try
            {
                return Ok(_bIDynamicReportingTool.archiveSelectedReports(reportID));
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "API",
                    "DynamicReportingToolController", "archiveReports"));
            }

        }

        /// <summary>
        /// AUTHOR: Jaideep Roy
        /// created Date:29/12/2020
        /// This API get all row data from table reportcontent
        /// </summary>
        [HttpGet]
        [Route("getAllData")]
        public IActionResult GetAllActiveDataFromReportContent()
        {
            string errors = null;
            try
            {
                return Ok(_bIDynamicReportingTool.GetAllDataFromReportContent());
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorCodes.ProcessException(ex, "API", "DynamicReportingToolController", "getAllActiveData", errors));
            }
        }

        /// <summary>
        /// AUTHOR: Jaideep Roy
        /// created Date:6/12/2020
        /// This API restores the row data to reportcontent by the report id
        /// </summary>
        /// <param name="reportId"></param>
        [HttpPost]
        [Route("restoreReports/{reportID}")]
        public IActionResult restoreSelectedReports(int reportID)
        {
            try
            {
                return Ok(_bIDynamicReportingTool.restoreSelectedReports(reportID));
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "API",
                    "DynamicReportingToolController", "restoreReports"));
            }

        }

        /// <summary>
        /// AUTHOR: Jaideep Roy
        /// created Date:4/01/2021
        /// This API saves the query id with the aliasname of the output from the query id
        /// </summary>
        [HttpPost]
        [Route("SaveReportAPIurl")]
        public IActionResult SaveReporturl([FromForm]Report_DTO data)
        {
            try
            {
                return Ok(_bIDynamicReportingTool.SaveReportUrl(data));

            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "API",
                    "DynamicReportingToolController", "SaveReportAPIurl"));
            }

        }

        /// <summary>
        /// AUTHOR: Jaideep Roy
        /// created Date:6/12/2020
        /// This API get all the query id and the aliasnames
        /// </summary>
        [HttpGet]
        [Route("getAllAPIUrl")]
        public IActionResult GetAllReporturl()
        {
            string errors = null;
            try
            {
                return Ok(_bIDynamicReportingTool.GetReporturl());
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorCodes.ProcessException(ex, "API", "DynamicReportingToolController", "getAllAPIUrl", errors));
            }
        }


        /// <summary>
        /// AUTHOR: Jaideep Roy
        /// created Date:4/01/2021
        /// This API get all the data using query id
        /// </summary>
        [HttpGet]
        [Route("getUrlDataById/{queryid}")]
        public async Task<IActionResult> GetUrlDataFromTable(int queryid)
        {
            string errors = null;
            try
            {
                var result = await _bIDynamicReportingTool.GetUrlLinkAndJsonAliasName(queryid);
                if (result.Rows.Count <= 0)
                {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorCodes.ProcessException(ex, "API", "DynamicReportingToolController", "getUrlDataById", errors));
            }
        }
    }


}
