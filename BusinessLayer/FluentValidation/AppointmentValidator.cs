using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.FluentValidation
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.AppointmentDate).NotEmpty().WithMessage("Randevu tarihi boş geçilemez");
            
            RuleFor(x=>x.PetName).NotEmpty().WithMessage("Evcil hayvan  adı boş geçilemez");
        }
    }
}
