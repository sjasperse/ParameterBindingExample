using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParameterBindingExample.Controllers
{
    public class ExampleController : ApiController
    {
        public IHttpActionResult Get()
        {
            return this.Json(new { uri = this.Request.RequestUri });
        }
    }
}
