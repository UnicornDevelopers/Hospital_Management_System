using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
       

        //Nav
        public List<Room> Rooms { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Nurse> Nurses { get; set; }



    }
}
