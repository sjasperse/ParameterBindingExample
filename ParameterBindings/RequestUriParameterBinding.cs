using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace ParameterBindingExample.ParameterBindings
{
    public class RequestUriParameterBinding : HttpParameterBinding
    {
        public RequestUriParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor)
        {
        }

        /// <summary>
        /// Checks to see if this binding applies to this descriptor.
        /// 
        /// In this case, it checks if both the type is Uri and the parameter name is requestUri
        /// </summary>
        public static bool AppliesTo(HttpParameterDescriptor descriptor)
        {
            return descriptor.ParameterType == typeof(Uri) && descriptor.ParameterName == "requestUri";
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            this.SetValue(actionContext, actionContext.Request.RequestUri);

            return Task.FromResult<object>(null);
        }
    }
}