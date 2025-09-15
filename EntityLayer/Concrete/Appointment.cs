using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string PetName { get; set; }

        public int CustomerId { get; set; }
        public int VeterinarianId { get; set; }

      
    }

}
