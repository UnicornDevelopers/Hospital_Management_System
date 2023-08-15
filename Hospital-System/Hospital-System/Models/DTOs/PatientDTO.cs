using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models.DTOs
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int? RoomId { get; set; }


        //Nav
        public RoomDTO? Rooms { get; set; }
        public List<AppointmentDTO>? Appointments { get; set; }
        public List<MedicalReportDTO>? MedicalReports { get; set; }


    }
}
