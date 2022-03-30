using BAL.Interfaces.DynamicTeportingTool;
using Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Newtonsoft.Json;
using DTO.Models.DynamicReportingTool;

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

        [HttpGet]
        [Route("getAllReportJson")]
        public IActionResult GetJsonDataFromTable()
        {
            string errors = null;
            try
            {
                return Ok(_bIDynamicReportingTool.GetJsonReportForUser());
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorCodes.ProcessException(ex, "API", "DynamicReportingToolController", "GetFormDetails", errors));
            }
        }

        [HttpGet]
        [Route("getAllData")]
        public IActionResult GetAllDataFromReportContent()
        {
            string errors = null;
            try
            {
                return Ok(_bIDynamicReportingTool.GetAllDataFromReportContent());
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorCodes.ProcessException(ex, "API", "DynamicReportingToolController", "GetFormDetails", errors));
            }
        }

        [HttpPost]
        [Route("ChangeAction/{reportID}")]
        public IActionResult ChangeAction(int reportID)
        {
            try
            {
                return Ok(_bIDynamicReportingTool.UpdateReportToShow(reportID));
            }
            catch (Exception ex)
            {
                throw new System.Exception(Common.Utilities.ErrorCodes.ProcessException(ex, "API",
                    "DynamicReportingToolController", "ChangeAction"));
            }

        }
    }


}
