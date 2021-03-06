using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EmployeeWebApi.Models
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                var auntheticationToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodeauntheticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(auntheticationToken));
                var userPassword = decodeauntheticationToken.Split(':');
                var user = userPassword[0];
                var password = userPassword[1];

                if (EmployeeSecurity.Login(user,password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user),null );
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }

        }
    }
}