using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models;
using Hospital_System.Tests.Mocks;

namespace TestProject1.DepartmentTests
{
    public class Dep : Mock
    {
        private IDepartment BuildRepository()
        {
            return new DepartmentService(_db);
        }
        [Fact]
        public async Task CreateDepartment_Should_Add_Department()
        {
            // Arrange
            var service = new DepartmentService(_db);
            var newDepartment = new InDepartmentDTO { DepartmentName = "NewDept", HospitalID = 1 };

            // Act
            var createdDepartment = await service.CreateDepartment(newDepartment);

            // Assert
            var retrievedDepartment = await service.GetDepartment(createdDepartment.Id);
            Assert.NotNull(retrievedDepartment);
            Assert.Equal(newDepartment.DepartmentName, retrievedDepartment.DepartmentName);
            Assert.Equal(newDepartment.HospitalID, retrievedDepartment.HospitalID);
        }
        [Fact]
        public async Task EmptyTest() // Can Check If There Are No Department
        {
            // arrange
            var service = BuildRepository();
            await service.DeleteDepartment(1);
            // act
            List<OutDepartmentDTO> result = await service.GetDepartments();
            // assert
            Assert.Empty(result);
        }
        [Fact]
        public async Task GetDepartment()
        {
            // arrange
            var department = new InDepartmentDTO
            {
                Id = 1,
                DepartmentName = "OdaiDepartment",
                HospitalID = 1,
            };
            var department2 = new InDepartmentDTO
            {
                Id = 2,
                DepartmentName = "OdaiDepartment222",
                HospitalID = 1,
            };
            var service = BuildRepository();
            var saved = await service.CreateDepartment(department);
            var saved2 = await service.CreateDepartment(department2);
            // act
            var result4 = await service.GetDepartment(1);
            var result5 = await service.GetDepartment(2);
            // assert
            Assert.Equal("OdaiDepartment", result4.DepartmentName);
            Assert.Equal("OdaiDepartment222", result5.DepartmentName);
        }
        [Fact]
        public async Task GetAllDepartment()
        {
            // arrange
            var department = new InDepartmentDTO
            {
                Id = 1,
                DepartmentName = "OdaiDepartment",
                HospitalID = 1,
            };
            var department2 = new InDepartmentDTO
            {
                Id = 2,
                DepartmentName = "OdaiDepartment222",
                HospitalID = 1,
            };
            var service = BuildRepository();
            var saved = await service.CreateDepartment(department);
            var saved2 = await service.CreateDepartment(department2);
            // act
            List<OutDepartmentDTO> result = await service.GetDepartments();
            // assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task UpdateDepartment()
        {
            var updateDepartmentInDB = new OutDepartmentDTO
            {
                Id = 1,
                DepartmentName = "OdaiDepartment202020",
            };
            var service = BuildRepository();
            // act
            var result = await service.UpdateDepartment(1, updateDepartmentInDB);
            // assert
            Assert.Equal("OdaiDepartment202020", result.DepartmentName);
        }
        [Fact]
        public async Task DeleteDepartment()
        {
            var service = BuildRepository();
            // act & assert
            List<OutDepartmentDTO> result = await service.GetDepartments();
            Assert.Equal(2, result.Count);
            await service.DeleteDepartment(1);
            List<OutDepartmentDTO> result2 = await service.GetDepartments();
            Assert.Equal(1, result2.Count);
        }
    }
}