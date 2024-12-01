using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Text;
using WebApi.Entities;
using WebApi.Interfaces;
using WebApi.Mappings;

namespace WebApi.Controllers
{
    [ApiController]
    public class DataValidationController : ControllerBase
    {
        private readonly IDataValidationService _dataValidationService;

        public DataValidationController(IDataValidationService dataValidationService)
        {
            _dataValidationService = dataValidationService;
        }

        [HttpPost("api/data/validate/")]
        [Consumes(MediaTypeNames.Text.Plain)]
        public async Task<ActionResult> ValidateDataFile()
        {
            string? requestBodyData = null;

            //TODO: Works only with UTF-8. Add additional support later
            using (StreamReader streamReader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                requestBodyData = await streamReader.ReadToEndAsync();
            }

            //Map request body data to a list of user data
            List<UserData> userDataList = UserDataMapping.FromRequestBodyDataToUserDataList(requestBodyData);

            DataFileValidity dataFileValidity = _dataValidationService.ValidateUserData(userDataList);
            
            if (dataFileValidity.FileValid)
            {
                return Ok(new{ FileValid = true });
            }

            return BadRequest(dataFileValidity);
        }
    }
}
