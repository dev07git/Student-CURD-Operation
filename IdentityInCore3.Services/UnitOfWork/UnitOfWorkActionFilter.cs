using IdentityInCore3.DAL.Models;
using IdentityInCore3.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IdentityInCore3.Services
{
    public class UnitOfWorkActionFilter : ActionFilterAttribute
    {

        private readonly Core3DBContext _dbContext;
     

        public UnitOfWorkActionFilter(Core3DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase))
                return;
            if (context.Exception == null)
            {

                _dbContext.Database.CommitTransaction();

            }
            else
            {
                _dbContext.Database.RollbackTransaction();

            }
            if (context.Exception != null)
            {
                Result<string> returnModel = new Result<string>();
                returnModel.ErrorCode = (int)HttpStatusCode.BadRequest;
                returnModel.ErrorDetail = context.Exception.ToString();
                context.ExceptionHandled = true;
                context.Result = new BadRequestObjectResult(returnModel);
            }
            return;

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase))
                return;
            _dbContext.Database.BeginTransaction();

        }
    }
}
