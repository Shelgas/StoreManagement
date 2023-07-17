using FluentValidation;
using SM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().Length(3, 255);
            RuleFor(x => x.Price).NotEmpty().NotNull();
            RuleFor(x => x.BrandName).NotEmpty().NotNull();

        }
    }
}
