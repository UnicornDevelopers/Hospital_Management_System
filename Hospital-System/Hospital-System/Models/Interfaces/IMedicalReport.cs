using Hospital_System.Models.DTOs.MedicalReport;

namespace Hospital_System.Models.Interfaces
{
    public interface IMedicalReport
    {

        Task<OutMedicalReportDTO> CreateMedicalReport(InMedicalReportDTO MedicalReportDto);

        // GET All
        Task<List<OutMedicalReportDTO>> GetMedicalReports();

        // GET Hotel By Id

        Task<MedicalReportDTO> GetMedicalReport(int MedicalReportID);

        // Update
        Task<OutMedicalReportDTO> UpdateMedicalReport(int id, InMedicalReportDTO MedicalReportID);

        // Delete 

        Task DeleteMedicalReport(int id);

    }
}
