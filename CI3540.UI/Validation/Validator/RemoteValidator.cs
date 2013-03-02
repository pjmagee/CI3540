using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Validators;

namespace CI3540.UI.Validation.Validator
{
    /// <summary>
    /// Modifications based for Routes
    /// http://stackoverflow.com/questions/1752721/asp-net-mvc-routedata-and-arrays
    /// May not change, depends on HTTP Request Header referrer URL, seems to be working without issue
    /// </summary>
    public class RemoteValidator : PropertyValidator
    {
        public string Url { get; private set; }
        public string HttpMethod { get; private set; }
        public string AdditionalFields { get; private set; }

        public RemoteValidator(string errorMessage, string action, string controller, HttpVerbs httpVerb = HttpVerbs.Get, string additionalFields = ""): base(errorMessage)
        {
            var httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                var request = new HttpRequest("/", "http://ubasolutions.com", "");
                var response = new HttpResponse(new StringWriter());
                httpContext = new HttpContext(request, response);
            }

            var httpContextBase = new HttpContextWrapper(httpContext);
            var routeData = new RouteData();
            var requestContext = new RequestContext(httpContextBase, routeData);

            var helper = new UrlHelper(requestContext);
            
            Url = helper.Action(action, controller);
            HttpMethod = httpVerb.ToString();
            AdditionalFields = additionalFields;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //This is not a server side validation rule. So, should not effect at the server side.
            return true;
        }
    }
}