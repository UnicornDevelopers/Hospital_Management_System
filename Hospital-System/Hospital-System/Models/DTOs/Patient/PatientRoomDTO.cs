using Hospital_System.Models.DTOs.Department;

namespace Hospital_System.Models.DTOs.Patient
{

    public class PatientRoomDTO
    {
        public int IdRoom { get; set; }
        public string RoomNumber { get; set; }
        public int? DepartmentId { get; set; }
        public OutDepartmentDTO? department { get; set; }
    }

}