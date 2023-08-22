namespace Hospital_System.Models.DTOs.Appointment
{
    public class DoctorAppointmentsDTO
    {

        public int Id { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public int PatientId { get; set; }

    }
}
