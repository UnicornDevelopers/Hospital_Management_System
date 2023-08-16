using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int HospitalID { get; set; }

        //Nav
        public HospitalDTO? hospital { get; set; }
        public List<RoomDTO>? Rooms { get; set; }
        public List<DoctorDTO>? Doctors { get; set; }
        public List<NurseDTO>? Nurses { get; set; }



    }
}
