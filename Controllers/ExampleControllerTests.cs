using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using Moq;
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
            var requestUri = new Uri("http://somethingsomething/api/example");

            var controller = new ExampleController();
            controller.Request = (new Mock<HttpRequestMessage>()).Object;

            var result = controller.Get(requestUri);
            var json = result
                                .ExecuteAsync(new CancellationToken()).Result
                                .Content.ReadAsStringAsync().Result;
            var jsonObj = JsonConvert.DeserializeObject<JToken>(json);

            Assert.AreEqual(requestUri, jsonObj.Value<string>("uri"));

        }
    }
}