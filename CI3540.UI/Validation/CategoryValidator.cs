using System;
using System.Linq;
using System.Web.Mvc;
using CI3540.Core.Entities;
using CI3540.Infrastructure.EntityFramework;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Validation.Validator;
using FluentValidation;

namespace CI3540.UI.Validation
{
    // http://fluentvalidation.codeplex.com/wikipage?title=Validators&referringTitle=Documentation
    
    public class NewCategoryValidator : AbstractValidator<NewCategoryViewModel>
    {
        public NewCategoryValidator()
        {
            RuleFor(m => m.Name).NotEmpty().Length(3, 30).SetValidator(new RemoteValidator("Category name is in use.", "ValidateCategoryName", "Categories"));
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(m => m.ParentId);
        }
    }
}