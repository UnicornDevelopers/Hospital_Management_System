using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital_System.Models;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using Xunit;
namespace TestProject1.MedRep
{
    public class MedRepTest : Mock
    {
        [Fact]
        public async Task CreateMedicalReport_ReturnMedicalReportDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var MedicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicalReportService = new MedicalReportService(_db);
            var newMedicalReport = new InMedicalReportDTO
            {
                ReportDate = MedicalReport.ReportDate,
                Description = MedicalReport.Description,
                DoctorId = doctor.Id,
                PatientId = patient.Id,
            };
            var createdMedicalReport = await medicalReportService.CreateMedicalReport(newMedicalReport);
            Assert.NotNull(createdMedicalReport);
            Assert.Equal("Test Description", createdMedicalReport.Description);
            Assert.Equal(doctor.Id, createdMedicalReport.DoctorId);
            Assert.Equal(patient.Id, createdMedicalReport.PatientId);
        }
        [Fact]
        public async Task GetMedicalReport_ReturnListOfMedicalReportDTOs()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var MedicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var department2 = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor2 = await CreateAndSaveTestDoctor(department2.Id);
            var room2 = await CreateAndSaveTestRoom(department2.Id);
            var patient2 = await CreateAndSaveTestPatient(room2.Id);
            var MedicalReport2 = await CreateAndSaveTestMedicalReport(doctor2.Id, patient2.Id);
            var medicalReportService = new MedicalReportService(_db);
            var medicalReportDto = await medicalReportService.GetMedicalReports();
            Assert.NotNull(medicalReportDto);
            Assert.Equal(2, medicalReportDto.Count);
            Assert.Contains(medicalReportDto, dto => dto.Id == MedicalReport.Id);
            Assert.Contains(medicalReportDto, dto => dto.Id == MedicalReport2.Id);
        }
        [Fact]
        public async Task GetMedicalReport_ReturnMedicalReportDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var MedicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicalReportService = new MedicalReportService(_db);
            var retrievedMedicalReport = await medicalReportService.GetMedicalReport(MedicalReport.Id);
            Assert.NotNull(retrievedMedicalReport);
            Assert.Equal("Test Description", retrievedMedicalReport.Description);
            Assert.Equal(doctor.Id, retrievedMedicalReport.DoctorId);
            Assert.Equal(patient.Id, retrievedMedicalReport.PatientId);
        }
        [Fact]
        public async Task UpdateMedicalReport_ReturnUpdatedMedicalReportDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var MedicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicalReportService = new MedicalReportService(_db);
            var updatedMedicalReport = new InMedicalReportDTO
            {
                ReportDate = new DateTime(2000, 4, 11),
                Description = "New Description",
                DoctorId = doctor.Id,
                PatientId = patient.Id,
            };
            var updatedMedicalReportDto = await medicalReportService.UpdateMedicalReport(MedicalReport.Id, updatedMedicalReport);
            Assert.NotNull(updatedMedicalReportDto);
            Assert.Equal("New Description", updatedMedicalReportDto.Description);
            Assert.Equal(new DateTime(2000, 4, 11), updatedMedicalReportDto.ReportDate);
            Assert.Equal(doctor.Id, updatedMedicalReportDto.DoctorId);
            Assert.Equal(patient.Id, updatedMedicalReportDto.PatientId);
        }
        [Fact]
        public async Task DeleteMedicalReport_ReturnDeletedMedicalReport()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicalReportService = new MedicalReportService(_db);
            await medicalReportService.DeleteMedicalReport(medicalReport.Id);
            var deletedMedicalReport = await _db.MedicalReports.FindAsync(medicalReport.Id);
            Assert.Null(deletedMedicalReport);
        }
    }
}