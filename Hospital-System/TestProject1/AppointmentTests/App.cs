using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.DTOs.AppointmentDTO;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.DepartmentTests;
namespace TestProject1.AppointmentTests
{
    public class App : Mock
    {
        private IAppointment BuildRepository()
        {
            return new AppointmentService(_db);
        }
        [Fact]
        public async Task CanSaveAndGetAppointment()
        {
            // arrange
            var appointment = new InAppoinmentDTO
            {
                Id = 1,
                DateOfAppointment = DateTime.Now,
                PatientId = 1,
                DoctorId = 1
            };
            var service = BuildRepository();
            // act
            var saved = await service.CreateAppointment(appointment);
            // assert
            Assert.NotNull(saved);
            Assert.NotEqual(0, appointment.Id);
            Assert.Equal(saved.Id, appointment.Id);
            Assert.Equal(saved.DateOfAppointment, appointment.DateOfAppointment);
            Assert.Equal(saved.PatientId, appointment.PatientId);
            Assert.Equal(saved.DoctorId, appointment.DoctorId);
        }
        [Fact]
        public async Task EmptyTest() // Can Check If There Are No Appointment
        {
            // arrange
            var service = BuildRepository();
            await service.DeleteAppointment(1);
            // act
            List<OutAppointmentDTO> result = await service.GetAppointments();
            // assert
            Assert.Empty(result);
        }
        [Fact]
        public async Task GetAppointment()
        {
            // arrange
            var appointment = new InAppoinmentDTO
            {
                Id = 1,
                DateOfAppointment = DateTime.Now,
                PatientId = 1,
                DoctorId = 1
            };
            var appointment2 = new InAppoinmentDTO
            {
                Id = 2,
                DateOfAppointment = DateTime.Now,
                PatientId = 2,
                DoctorId = 2
            };
            var service = BuildRepository();
            var saved = await service.CreateAppointment(appointment);
            var saved2 = await service.CreateAppointment(appointment2);
            // act
            var result4 = await service.GetAppointment(1);
            var result5 = await service.GetAppointment(2);
            // assert
            Assert.Equal(1, result4.PatientId);
            Assert.Equal(2, result5.PatientId);
        }
        [Fact]
        public async Task GetAllAppointment()
        {
            // arrange
            var appointment = new InAppoinmentDTO
            {
                Id = 1,
                DateOfAppointment = DateTime.Now,
                PatientId = 1,
                DoctorId = 1
            };
            var appointment2 = new InAppoinmentDTO
            {
                Id = 2,
                DateOfAppointment = DateTime.Now,
                PatientId = 2,
                DoctorId = 2
            };
            var service = BuildRepository();
            var saved = await service.CreateAppointment(appointment);
            var saved2 = await service.CreateAppointment(appointment2);
            // act
            List<OutAppointmentDTO> result = await service.GetAppointments();
            // assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task UpdateAppoinment()
        {
            var updateAppoinmentInDB = new InAppoinmentDTO
            {
                Id = 2,
                DateOfAppointment = DateTime.Now,
                PatientId = 3,
                DoctorId = 2
            };
            var service = BuildRepository();
            // act
            var result = await service.UpdateAppointment(2, updateAppoinmentInDB);
            // assert
            Assert.Equal(1, result.PatientId);
            Assert.NotEqual(2, result.DoctorId);
        }
        [Fact]
        public async Task DeleteAppointment()
        {
            var service = BuildRepository();
            // act & assert
            List<OutAppointmentDTO> result = await service.GetAppointments();
            Assert.Equal(2, result.Count);
            await service.DeleteAppointment(1);
            List<OutAppointmentDTO> result2 = await service.GetAppointments();
            Assert.Equal(1, result2.Count);
        }
    }
}