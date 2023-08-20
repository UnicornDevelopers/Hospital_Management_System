
using Hospital_System.Models;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;

namespace Hospital_System.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalReportsController : ControllerBase
    {
        private readonly IMedicalReport _medicalReport;

        public MedicalReportsController(IMedicalReport medicalReport)
        {
            _medicalReport = medicalReport;
        }


        // GET: api/MedicalReports
        [HttpGet]
        //[Authorize(Roles = "Doctor")]
        public async Task<ActionResult<IEnumerable<OutMedicalReportDTO>>> GetMedicalReports()
        {
            var medicalReport = await _medicalReport.GetMedicalReports();
            return Ok(medicalReport);
        }

        // GET: api/MedicalReport/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Doctor, Patient")]
        public async Task<ActionResult<MedicalReportDTO>> GetMedicalReport(int id)
        {
            MedicalReportDTO TheMedicalReport = await _medicalReport.GetMedicalReport(id);

            if (TheMedicalReport == null)
            {
                return NotFound();
            }

            return TheMedicalReport;
        }

        // PUT: api/MedicalReport/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> PutMedicalReport(int id, InMedicalReportDTO medicalReport)
        {
            if (id != medicalReport.Id)
            {
                return BadRequest();
            }
            var updateMedicalReport = await _medicalReport.UpdateMedicalReport(id, medicalReport);
            return Ok(updateMedicalReport);
        }

        // POST: api/MedicalReport
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "Doctor")]
        public async Task<ActionResult<OutMedicalReportDTO>> PostMedicalReport(InMedicalReportDTO medicalReport)
        {
            if (medicalReport == null)
            {
                return Problem("Entity set 'MedicalReport'  is null.");
            }
            if (medicalReport.Id == null)
            {
                return NotFound();
            }
            var newMedicalReport = await _medicalReport.CreateMedicalReport(medicalReport);

            return Ok(newMedicalReport);
        }

        // DELETE: api/MedicalReport/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteMedicalReport(int id)
        {
            await _medicalReport.DeleteMedicalReport(id);
            return NoContent();
        }
    }
}
