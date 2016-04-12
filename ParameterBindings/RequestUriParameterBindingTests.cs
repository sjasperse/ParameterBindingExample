using System;
using System.Web.Http.Controllers;
using Moq;
using NUnit.Framework;

namespace ParameterBindingExample.ParameterBindings
{
    [TestFixture]
    public class RequestUriParameterBindingTests
    {
        private Type parameterType;
        private string parameterName;
        private Mock<HttpParameterDescriptor> descriptor;
        private HttpActionContext actionContext;
        private RequestUriParameterBinding binding;

        [SetUp]
        public void Setup()
        {
            this.parameterName = "requestUri";
            this.parameterType = typeof(Uri);
            this.descriptor = new Mock<HttpParameterDescriptor>();
            this.actionContext = ContextUtil.CreateActionContext();
            this.binding = new RequestUriParameterBinding(this.descriptor.Object);

            this.descriptor.SetupGet(x => x.ParameterName).Returns(() => this.parameterName);
            this.descriptor.SetupGet(x => x.ParameterType).Returns(() => this.parameterType);
        }

        [Test]
        public void RequestUriParameterBinding_AppliesTo_IsTrueWithDefaultSetup()
        {
            var r = RequestUriParameterBinding.AppliesTo(this.descriptor.Object);

            Assert.IsTrue(r);
        }

        [Test]
        public void RequestUriParameterBinding_AppliesTo_IsFalseWithInvalidParameterType()
        {
            this.parameterType = typeof(object);

            var r = RequestUriParameterBinding.AppliesTo(this.descriptor.Object);

            Assert.IsFalse(r);
        }

        [Test]
        public void RequestUriParameterBinding_AppliesTo_IsFalseWithInvalidParameterName()
        {
            this.parameterName = "invalid";

            var r = RequestUriParameterBinding.AppliesTo(this.descriptor.Object);

            Assert.IsFalse(r);
        }

        [Test]
        public void RequestUriParameterBinding_ExecuteBindingAsync_WillSetTheValue()
        {
            // verify the value isn't already there
            Assert.IsFalse(this.actionContext.ActionArguments.ContainsKey(this.descriptor.Object.ParameterName));

            this.binding.ExecuteBindingAsync(
                metadataProvider: null,
                actionContext: this.actionContext,
                cancellationToken: new System.Threading.CancellationToken())
                .Wait();

            Assert.IsTrue(this.actionContext.ActionArguments.ContainsKey(this.descriptor.Object.ParameterName));
        }
    }
}