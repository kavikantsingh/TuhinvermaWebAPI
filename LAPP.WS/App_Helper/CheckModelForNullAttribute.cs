using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LAPP.WS.Controllers.Board
{
    [AttributeUsage(AttributeTargets.Method,Inherited =true)]
    public class CheckModelForNullAttribute : ActionFilterAttribute
    {
        private readonly Func<Dictionary<string, object>, bool> _validate;

        public CheckModelForNullAttribute() : this(arguments =>
     arguments.ContainsValue(null))
        { }

        public CheckModelForNullAttribute(Func<Dictionary<string, object>, bool> checkCondition)
        {
            _validate = checkCondition;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (_validate(actionContext.ActionArguments))
            {
                var nullArgs = actionContext.ActionArguments.Where(kv => kv.Value == null).Select(kv => string.Format("The argument '{0}' cannot be null", kv.Key)).ToArray();
                if (nullArgs.Any())
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join("\n", nullArgs));
                }
            }
        }
    }
}