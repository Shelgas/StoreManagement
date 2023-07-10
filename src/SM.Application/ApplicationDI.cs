using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using SM.Application.Validation;
using SM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<Category>, CategoryValidator>();
            return services;
        }
    }
}
