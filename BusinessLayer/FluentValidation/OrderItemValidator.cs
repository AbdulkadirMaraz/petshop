using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.FluentValidation
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Miktar boş geçilemez");
            RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("Birim fiyatı boş geçilemez");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Müşteri no boş geçilemez");
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("Sipariş no boş geçilemez");
           
        }
    }
}
