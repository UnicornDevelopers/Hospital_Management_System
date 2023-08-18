namespace Hospital_System.Models.DTOs.Appointment
{
    public class InAppoinmentDTO
    {
        public int Id { get; set; }
        public DateTime DateOfAppointment { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }
    }
}
