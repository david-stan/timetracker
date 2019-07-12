﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackerApp.Models.Validation
{
    public class ProjectInputModelValidator
        : AbstractValidator<ProjectInputModel>
    {
        public ProjectInputModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 100);
            RuleFor(x => x.ClientId)
                .NotEmpty();
        }
    }
}
