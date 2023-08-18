using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading.Tasks;
using static Hospital_System.Models.Nurse;

namespace Hospital_System.Tests.Mocks
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly HospitalDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseSqlite(_connection)
                .Options;

            _db = new HospitalDbContext(options);
            _db.Database.EnsureCreated();
        }




        //Room
        protected async Task<Room> CreateAndSaveTestRoom(int departmentId)
        {
            var room = new Room
            {
                RoomNumber = "101",
                RoomAvailability = true,
                NumberOfBeds = 5,
                DepartmentId = departmentId // Make sure you set the correct department Id
            };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            return room;
        }





        //Hospital
        protected async Task<Hospital> CreateAndSaveTestHospital()
        {
            var hospital = new Hospital { HospitalName = "Test Hospital", Address ="Amman", ContactNumber = "123456789" };

            _db.Hospitals.Add(hospital);
            await _db.SaveChangesAsync();
            return hospital;
        }


        //Department
        protected async Task<Department> CreateAndSaveTestDepartment(int hospitalId)
        {
            var department = new Department { DepartmentName = "Test Department", HospitalID = hospitalId };

            _db.Departments.Add(department);
            await _db.SaveChangesAsync();
            return department;
        }


        //Nurse
        protected async Task<Nurse> CreateAndSaveTestNurse(int departmentId)
        {
            var nurse = new Nurse
            {
                FirstName = "Test",
                LastName = "Nurse",
                Gender = "Female",
                ContactNumber = "123456789",
                shift = Shift.Morning,
                DepartmentId = departmentId
            };

            _db.Nurses.Add(nurse);
            await _db.SaveChangesAsync();
            return nurse;
        }

        //Doctor
        protected async Task<Doctor> CreateAndSaveTestDoctor(int departmentId)
        {
            var doctor = new Doctor
            {
                FirstName = "Doctor",
                LastName = "Doctor",
                Gender = "Male",
                ContactNumber = "123456789",
                Speciality = "test",
                DepartmentId = departmentId
            };

            _db.Doctors.Add(doctor);
            await _db.SaveChangesAsync();
            return doctor;
        }






        // Other methods for creating and saving test data

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}
