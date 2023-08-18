using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using TestProject1.DepartmentTests;


using Hospital_System.Models;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.DepartmentTests;
using Hospital_System.Tests.Mocks;

namespace TestProject1.MedicalReportTests
{
    public class MedRep : Mock
    {
        private IMedicalReport BuildRepository()
        {
            return new MedicalReportService(_db);
        }
        [Fact]
        public async Task CanSaveAndGetMedicalReport()
        {
            // arrange
            var medicalReport = new InMedicalReportDTO
            {
                ReportDate = DateTime.Now,
                Description = "Abood",
                PatientId = 2,
                DoctorId = 2,
            };
            var service = BuildRepository();
            // act
            var saved = await service.CreateMedicalReport(medicalReport);
            // assert
            Assert.NotNull(saved);
            Assert.NotEqual(0, medicalReport.Id);
            Assert.Equal(saved.Id, medicalReport.Id);
            Assert.Equal(saved.Description, medicalReport.Description);
            Assert.Equal(saved.ReportDate, medicalReport.ReportDate);
        }
        [Fact]
        public async Task EmptyTest() // Can Check If There Are No MedicalReport
        {
            // arrange
            var service = BuildRepository();
            await service.DeleteMedicalReport(1);
            // act
            List<OutMedicalReportDTO> result = await service.GetMedicalReports();
            // assert
            Assert.Empty(result);
        }
        [Fact]
        public async Task GetMedicalReport()
        {
            // arrange
            var medicalReport = new InMedicalReportDTO
            {
                Id = 1,
                ReportDate = DateTime.Now,
                Description = "Abood",
                PatientId = 1,
                DoctorId = 1,
            };
            var medicalReport2 = new InMedicalReportDTO
            {
                Id = 1,
                ReportDate = DateTime.Now,
                Description = "Odai",
                PatientId = 1,
                DoctorId = 1,
            };
            var service = BuildRepository();
            var saved = await service.CreateMedicalReport(medicalReport);
            var saved2 = await service.CreateMedicalReport(medicalReport2);
            // act
            var result4 = await service.GetMedicalReport(1);
            var result5 = await service.GetMedicalReport(2);
            // assert
            Assert.Equal("Abood", result4.Description);
            Assert.Equal("Odai", result5.Description);
        }
        [Fact]
        public async Task GetAllMedicalReport()
        {
            // arrange
            var medicalReport = new InMedicalReportDTO
            {
                Id = 1,
                ReportDate = DateTime.Now,
                Description = "Abood",
                PatientId = 1,
                DoctorId = 1,
            };
            var medicalReport2 = new InMedicalReportDTO
            {
                Id = 1,
                ReportDate = DateTime.Now,
                Description = "Odai",
                PatientId = 1,
                DoctorId = 1,
            };
            var service = BuildRepository();
            var saved = await service.CreateMedicalReport(medicalReport);
            var saved2 = await service.CreateMedicalReport(medicalReport2);
            // act
            List<OutMedicalReportDTO> result = await service.GetMedicalReports();
            // assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task UpdateMedicalReport()
        {
            var updateMedicalReportInDB = new InMedicalReportDTO
            {
                Id = 1,
                ReportDate = DateTime.Now,
                Description = "Qusai",
                PatientId = 1,
                DoctorId = 1,
            };
            var service = BuildRepository();
            // act
            var result = await service.UpdateMedicalReport(2, updateMedicalReportInDB);
            // assert
            Assert.Equal("Qusai", result.DepartmentName);
        }
        [Fact]
        public async Task DeleteMedicalReport()
        {
            var service = BuildRepository();
            // act & assert
            List<OutMedicalReportDTO> result = await service.GetMedicalReports();
            Assert.Equal(2, result.Count);
            await service.DeleteMedicalReport(1);
            List<OutMedicalReportDTO> result2 = await service.GetMedicalReports();
            Assert.Equal(1, result2.Count);
        }
    }
}