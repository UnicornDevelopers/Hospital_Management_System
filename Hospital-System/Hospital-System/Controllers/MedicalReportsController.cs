
using Hospital_System.Models;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hospital_System.Controllers
{
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
        public async Task<ActionResult<IEnumerable<MedicalReportDTO>>> GetMedicalReports()
        {
            var medicalReport = await _medicalReport.GetMedicalReports();
            return Ok(medicalReport);
        }

        // GET: api/MedicalReport/5
        [HttpGet("{id}")]
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
        public async Task<IActionResult> PutMedicalReport(int id, MedicalReportDTO medicalReport)
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
        public async Task<ActionResult<MedicalReportDTO>> PostMedicalReport(MedicalReportDTO medicalReport)
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
        public async Task<IActionResult> DeleteMedicalReport(int id)
        {
            await _medicalReport.DeleteMedicalReport(id);
            return NoContent();
        }
    }
}
