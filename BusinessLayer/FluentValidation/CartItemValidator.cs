using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.FluentValidation
{
    public class CartItemValidator : AbstractValidator<CartItem>
    {
        public CartItemValidator()
        {
       
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Miktar boş geçilemez");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Müşteri ıd numarası boş geçilemez");
        }
    }
}
