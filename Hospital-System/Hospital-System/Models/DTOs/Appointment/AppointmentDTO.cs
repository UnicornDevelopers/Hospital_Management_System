using Hospital_System.Models.DTOs.Patient;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models.DTOs.AppointmentDTO
{
    public class AppointmentDTO
    {

        public int Id { get; set; }
        public DateTime DateOfAppointment { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }


        // Nav
        public OutDocDTO? doctor { get; set; }
        public OutPatientDTO? patient { get; set; }


    }
}
