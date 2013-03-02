using System.Reflection;
using FluentValidation;
using Ninject.Modules;

namespace CI3540.UI.Validation.Module
{
    /// <summary>
    /// http://fluentvalidation.codeplex.com/
    /// https://github.com/ninject/ninject.web.mvc.fluentvalidation/issues/3
    /// http://fluentvalidation.codeplex.com/documentation
    /// </summary>
    public class FluentValidatorModule : NinjectModule
    {
        public override void Load()
        {
            // ~/Validation/*Validator.cs
            AssemblyScanner assembly = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly());

            foreach (AssemblyScanner.AssemblyScanResult result in assembly)
            {
                Bind(result.InterfaceType).To(result.ValidatorType);
            }
        }
    }
}
