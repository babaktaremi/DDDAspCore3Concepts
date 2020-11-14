using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Utility.Utilities;
using WebFrameWork.Api;
using WebFrameWork.StatusCodeDescription;

namespace WebFrameWork.Filters
{
    public class BadRequestResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!(context.Result is BadRequestObjectResult badRequestObjectResult)) return;

            var modelState = context.ModelState;

            var errors = new ValidationProblemDetails(modelState);

            var message = ApiResultStatusCode.BadRequest.ToDisplay();

            var apiResult = new ApiResult<IDictionary<string, string[]>>(false, ApiResultStatusCode.BadRequest, errors.Errors, message);
            context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}