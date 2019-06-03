using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIReact.Controllers
{
    public class BaseController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult BuildJsonResponse(int responseCode, object message, object data = null, object error = null)
        {
            var response = new
            {
                Code = responseCode,
                Message = message,
                Errors = error,
                Data = data
            };

            switch (responseCode)
            {
                case 400:
                    return BadRequest(response);
                case 401:
                case 404:
                case 409:
                case 500:
                    return StatusCode(responseCode, response);
                case 200:
                case 201:
                    return Ok(response);
                default:
                    return new JsonResult(new
                    {
                        Code = responseCode,
                        Message = message,
                        Errors = error,
                        Data = data
                    });
            }
        }
    }
}