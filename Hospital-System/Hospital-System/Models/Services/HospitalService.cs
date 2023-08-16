using Hospital_System.Data;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Models.Services
{
    public class HospitalService : IHospital
    {
        private readonly HospitalDbContext _context;

        public HospitalService(HospitalDbContext context)
        {
            _context = context;
        }


        public async Task<Hospital> Create(Hospital hospital)
        {



            _context.Hospitals.Add(hospital);
            await _context.SaveChangesAsync();

            return hospital;


        }

        public async Task Delete(int id)
        {
            Hospital hospital = await _context.Hospitals.FindAsync(id);
            _context.Entry(hospital).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<HospitalDTO> GetHospital(int HospitalID)
        {

           var hospital =  await _context.Hospitals.Select(
                  hospital => new HospitalDTO
                  {
                      Id = hospital.Id,
                      HospitalName = hospital.HospitalName,
                      Address = hospital.Address,
                      ContactNumber = hospital.ContactNumber,
                      departments = hospital.Departments.Select(x => new DepartmentDTO
                      {

                          Id = x.Id,
                          DepartmentName = x.DepartmentName,


                      }).ToList(),


                  }



                  ).FirstOrDefaultAsync(x =>x.Id == HospitalID);

            return hospital;
        }

        public async Task<List<HospitalDTO>> GetHospitals()
        {
            return await _context.Hospitals.Select(
                hospital => new HospitalDTO
                { 
                Id = hospital.Id,
                HospitalName = hospital.HospitalName,
                Address = hospital.Address,
                ContactNumber = hospital.ContactNumber,
                departments = hospital.Departments.Select(x => new DepartmentDTO
                {

                    Id = x.Id,
                    DepartmentName = x.DepartmentName,


                }).ToList(),
                
                
                }
                
                
                
                ).ToListAsync();



        }

        public async Task<Hospital> UpdateHospital(int id, Hospital hospitalDTO)
        {
            Hospital hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
                return null;
                hospital.Id = hospitalDTO.Id;
                hospital.HospitalName = hospitalDTO.HospitalName;
                 hospital.ContactNumber = hospitalDTO.ContactNumber;
            hospital.Address = hospitalDTO.Address;


            _context.Entry(hospital).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hospitalDTO;
        }
    }
}
