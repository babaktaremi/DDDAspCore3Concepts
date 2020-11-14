using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebFrameWork.Api;
using WebFrameWork.StatusCodeDescription;

namespace WebFrameWork.Filters
{
    public class NotFoundResultAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!(context.Result is NotFoundObjectResult notFoundObjectResult)) return;
            var apiResult = new ApiResult<object>(false, ApiResultStatusCode.NotFound, notFoundObjectResult.Value);
            context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
        }
    }
}
