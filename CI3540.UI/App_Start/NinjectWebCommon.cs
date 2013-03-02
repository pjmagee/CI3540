using System.Web.Mvc;
using CI3540.Infrastructure.EntityFramework;
using CI3540.UI.Services;
using CI3540.UI.Services.Impl;
using CI3540.UI.Validation.Module;
using CI3540.UI.Validation.Validator;
using FluentValidation.Mvc;
using Ninject.Web.Mvc.FluentValidation;


[assembly: WebActivator.PreApplicationStartMethod(typeof(CI3540.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(CI3540.UI.App_Start.NinjectWebCommon), "Stop")]

namespace CI3540.UI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Remote Validator taken from 
            // http://nripendra-newa.blogspot.co.uk/2011/05/fluent-validation-remote-validation.html
            // NinjectValidator is a Ninject Exetension listed on the Ninject Website used for FluentValidation with MVC 4

            FluentValidationModelValidationFactory validationFactory = (metadata, context, rule, validator) => new RemoteFluentValidationPropertyValidator(metadata, context, rule, validator);
            var ninjectValidatorFactory = new NinjectValidatorFactory(kernel);
            var validationProvider = new FluentValidationModelValidatorProvider(ninjectValidatorFactory);
            validationProvider.Add(typeof(RemoteValidator), validationFactory);
            ModelValidatorProviders.Providers.Add(validationProvider);
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            kernel.Load<FluentValidatorModule>();

            // Ninject with EF DbContext
            // http://stackoverflow.com/questions/11921883/how-to-handle-dbcontext-when-using-ninject
            // Must ensure the same db context is injected into each service per web request

            kernel.Bind<IProductService>().To<ProductService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            kernel.Bind<ICartService>().To<CartService>().InRequestScope();
            kernel.Bind<IOrderService>().To<OrderService>().InRequestScope();
            kernel.Bind<StoreContext>().ToSelf().InRequestScope();


        }        
    }
}
