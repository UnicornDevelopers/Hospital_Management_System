using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models.DTOs
{
    public class MedicalReportDTO
    {

        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string Description { get; set; }
        public int? DoctorId { get; set; }

        public int? PatientId { get; set; }

        //Nav


        public List<MedicineDTO>? Medicines { get; set; }
        public DoctorDTO? doctor { get; set; }
        public PatientDTO? patient { get; set; }



    }
}
