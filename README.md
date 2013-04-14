CI3540: Web Based Enterprise Services
======

E-Commerce ASP.NET MVC4 Web Application

ASP.NET MVC 4
----
Microsoft ASP.NET MVC 4 Framework.

Bootstrap CSS
----
Bootstrap CSS Grid Framework great for rapid prototyping.

AutoMapper
----
AutoMapper use between Entities and ViewModels for UI and Data Layer Abstraction reducing high-coupling between layers.

EntityFramework
----
EntityFramework 5 used as the ORM for Persistence.

FluentValidation
----
FluentValidation was used to validate ViewModels on the UI Layer.

Ninject
----
IoC Container used to Inject Implementations of my Service Layer into my Controllers. Allowing for easily swapping out persistence implementations from EntityFramework, ADO.NET, NHibernate keeping the UI Layer ORM Agnostic when consuming Injected Services through Controller Constructors.

jQuery
----
jQuery used for UI validation with FluentValidation Factory Model Validator binder with ASP.NET MVC 4. Including jQuery UI Datepickers and various other plugins.
