using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Medicine;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.DepartmentTests;
namespace TestProject1.MedicineTests
{
    public class Medicine : Mock
    {
        private IMedicine BuildRepository()
        {
            return new MedicineService(_db);
        }
        [Fact]
        public async Task CanSaveAndGetMedicine()
        {
            // arrange
            var medicine = new OutMedicineDTO
            {
                Id = 1,
                MedicineName = "Odai",
                Portion = "odaix"
            };
            var service = BuildRepository();
            // act
            var saved = await service.CreateMedicine(medicine);
            // assert
            Assert.NotNull(saved);
            Assert.NotEqual(0, medicine.Id);
            Assert.Equal(saved.Id, medicine.Id);
            Assert.Equal(saved.MedicineName, medicine.MedicineName);
            Assert.Equal(saved.Portion, medicine.Portion);
        }
        [Fact]
        public async Task EmptyTest() // Can Check If There Are No MedicalReport
        {
            // arrange
            var service = BuildRepository();
            await service.DeleteMedicine(1);
            // act
            List<OutMedicineDTO> result = await service.GetMedicines();
            // assert
            Assert.Empty(result);
        }
        [Fact]
        public async Task GetMedicine()
        {
            // arrange
            var medicine = new OutMedicineDTO
            {
                Id = 1,
                MedicineName = "Odai",
                Portion = "odaix"
            };
            var medicine2 = new OutMedicineDTO
            {
                Id = 2,
                MedicineName = "Aboood",
                Portion = "Abooodx"
            };
            var service = BuildRepository();
            var saved = await service.CreateMedicine(medicine);
            var saved2 = await service.CreateMedicine(medicine2);
            // act
            var result4 = await service.GetMedicine(1);
            var result5 = await service.GetMedicine(2);
            // assert
            Assert.Equal("Odai", result4.MedicineName);
            Assert.Equal("Aboood", result5.MedicineName);
        }
        [Fact]
        public async Task GetAllMedicine()
        {
            // arrange
            var medicine = new OutMedicineDTO
            {
                Id = 1,
                MedicineName = "Odai",
                Portion = "odaix"
            };
            var medicine2 = new OutMedicineDTO
            {
                Id = 2,
                MedicineName = "Aboood",
                Portion = "Abooodx"
            };
            var service = BuildRepository();
            var saved = await service.CreateMedicine(medicine);
            var saved2 = await service.CreateMedicine(medicine2);
            // act
            List<OutMedicineDTO> result = await service.GetMedicines();
            // assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task UpdateMedicine()
        {
            var updateMedicineInDB = new OutMedicineDTO
            {
                Id = 2,
                MedicineName = "Abooodzzzzz",
                Portion = "Abooodx"
            };
            var service = BuildRepository();
            // act
            var result = await service.UpdateMedicine(2, updateMedicineInDB);
            // assert
            Assert.Equal("Abooodzzzzz", result.MedicineName);
        }
        [Fact]
        public async Task DeleteMedicine()
        {
            var service = BuildRepository();
            // act & assert
            List<OutMedicineDTO> result = await service.GetMedicines();
            Assert.Equal(2, result.Count);
            await service.DeleteMedicine(1);
            List<OutMedicineDTO> result2 = await service.GetMedicines();
            Assert.Equal(1, result2.Count);
        }
    }
}