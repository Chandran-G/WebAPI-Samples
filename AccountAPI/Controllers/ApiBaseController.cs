using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Account.API.Controllers
{
   
        public class ApiBaseController : ApiController
        {
            protected string GetLink<T>(string routeName, object values) where T : IHttpController
            {
                return Url.Link(routeName, values);
            }
        }
    
}
