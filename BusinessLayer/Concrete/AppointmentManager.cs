using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class AppointmentManager : IAppointmentService
    {
        IAppointmentDal _appointmentDal;

        public AppointmentManager(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }

        public void Delete(Appointment t)
        {
            _appointmentDal.Delete(t);
        }

        public Appointment GetById(int id)
        {
            return _appointmentDal.GetById(id);
        }

        public List<Appointment> GetList()
        {
           return _appointmentDal.GetList();
        }

        public void Insert(Appointment t)
        {
            _appointmentDal.Insert(t);
        }

        public void Update(Appointment t)
        {
            _appointmentDal.Update(t);
        }
    }
}
