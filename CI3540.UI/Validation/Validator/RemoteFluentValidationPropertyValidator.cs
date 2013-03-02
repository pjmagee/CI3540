using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace CI3540.UI.Validation.Validator
{
    /// <summary>
    /// Taken from the following blog
    /// http://nripendra-newa.blogspot.co.uk/2011/05/fluent-validation-remote-validation.html
    /// Enables remote validation while using the FluentValidator Framework
    /// </summary>
    public class RemoteFluentValidationPropertyValidator : FluentValidationPropertyValidator
    {
        private RemoteValidator RemoteValidator
        {
            get { return (RemoteValidator) Validator; }
        }

        public RemoteFluentValidationPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext, PropertyRule rule, IPropertyValidator validator) : base(metadata, controllerContext, rule, validator)
        {
            ShouldValidate = false;
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (!ShouldGenerateClientSideRules()) 
                yield break;

            var formatter = new MessageFormatter().AppendPropertyName(Rule.PropertyName);
            string message = formatter.BuildMessage(RemoteValidator.ErrorMessageSource.GetString());

            yield return new ModelClientValidationRemoteRule(message, RemoteValidator.Url, RemoteValidator.HttpMethod, RemoteValidator.AdditionalFields);
        }
    }
}