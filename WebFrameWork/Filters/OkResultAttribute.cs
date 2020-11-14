﻿using System;
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
   public class OkResultAttribute:ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            switch (context.Result)
            {
                case OkObjectResult okObjectResult:
                {
                    var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, okObjectResult.Value);
                    context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
                    break;
                }
                case OkResult okResult:
                {
                    var apiResult = new ApiResult(true, ApiResultStatusCode.Success);
                    context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
                    break;
                }
                default:return;
            }
        }
    }
}
