using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Müşteri Adı Soyadı boş geçilemez");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş geçilmez");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon numarası boş geçilemez");
        }
    }
}
