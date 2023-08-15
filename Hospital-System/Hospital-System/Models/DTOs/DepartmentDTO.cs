using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }


        //Nav
        public List<RoomDTO>? Rooms { get; set; }
        public List<DoctorDTO>? Doctors { get; set; }
        public List<NurseDTO>? Nurses { get; set; }



    }
}
