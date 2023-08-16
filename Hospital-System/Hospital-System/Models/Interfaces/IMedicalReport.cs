using Hospital_System.Models.DTOs.MedicalReport;

namespace Hospital_System.Models.Interfaces
{
    public interface IMedicalReport
    {

        Task<MedicalReportDTO> CreateMedicalReport(MedicalReportDTO MedicalReportDto);

        // GET All
        Task<List<MedicalReportDTO>> GetMedicalReports();

        // GET Hotel By Id

        Task<MedicalReportDTO> GetMedicalReport(int MedicalReportID);

        // Update
        Task<MedicalReportDTO> UpdateMedicalReport(int id, MedicalReportDTO MedicalReportID);

        // Delete 

        Task DeleteMedicalReport(int id);

    }
}
