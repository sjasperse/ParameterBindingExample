using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace ParameterBindingExample.Controllers
{
    [TestFixture]
    public class ExampleControllerTests
    {
        [Test]
        public void ExampleController_Get_ReturnsJsonWithRequestUri()
        {
            var requestUri = "http://somethingsomething/api/example";

            var controller = new ExampleController();
            controller.Request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, requestUri);

            var result = controller.Get();
            var json = result
                                .ExecuteAsync(new CancellationToken()).Result
                                .Content.ReadAsStringAsync().Result;
            var jsonObj = JsonConvert.DeserializeObject<JToken>(json);

            Assert.AreEqual(requestUri, jsonObj.Value<string>("uri"));

        }
    }
}