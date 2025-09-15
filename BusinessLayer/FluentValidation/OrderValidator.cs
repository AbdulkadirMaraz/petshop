using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.FluentValidation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Sipariş tarihi boş geçilemez");
            RuleFor(x => x.TotalAmount).NotEmpty().WithMessage("Toplam tutar boş geçilemez");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Müşteri no boş geçilemez");

        }
    }
}
