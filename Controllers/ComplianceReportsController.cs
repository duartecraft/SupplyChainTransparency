using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplyChainTransparency.Data;
using SupplyChainTransparency.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SupplyChainTransparency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Adiciona autorização a todos os endpoints deste controller
    public class ComplianceReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComplianceReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ComplianceReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplianceReport>>> GetComplianceReports(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var complianceReports = await _context.ComplianceReports
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(complianceReports);
        }

        // GET: api/ComplianceReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplianceReport>> GetComplianceReport(int id)
        {
            var complianceReport = await _context.ComplianceReports.FindAsync(id);

            if (complianceReport == null)
            {
                return NotFound();
            }

            return complianceReport;
        }

        // POST: api/ComplianceReports
        [HttpPost]
        public async Task<ActionResult<ComplianceReport>> PostComplianceReport(ComplianceReport complianceReport)
        {
            _context.ComplianceReports.Add(complianceReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComplianceReport", new { id = complianceReport.Id }, complianceReport);
        }

        // PUT: api/ComplianceReports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplianceReport(int id, ComplianceReport complianceReport)
        {
            if (id != complianceReport.Id)
            {
                return BadRequest();
            }

            _context.Entry(complianceReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplianceReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ComplianceReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplianceReport(int id)
        {
            var complianceReport = await _context.ComplianceReports.FindAsync(id);
            if (complianceReport == null)
            {
                return NotFound();
            }

            _context.ComplianceReports.Remove(complianceReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComplianceReportExists(int id)
        {
            return _context.ComplianceReports.Any(e => e.Id == id);
        }

        // GET: api/ComplianceReports/compliant
        [HttpGet("compliant")]
        public async Task<ActionResult<IEnumerable<ComplianceReport>>> GetCompliantReports()
        {
            var compliantReports = await _context.ComplianceReports
                .Where(cr => cr.IsCompliant)
                .ToListAsync();
            return Ok(compliantReports);
        }

        // GET: api/ComplianceReports/byCompany/{companyName}
        [HttpGet("byCompany/{companyName}")]
        public async Task<ActionResult<IEnumerable<ComplianceReport>>> GetComplianceReportsByCompany(string companyName)
        {
            var reports = await _context.ComplianceReports
                .Where(cr => cr.CompanyName.Contains(companyName))
                .ToListAsync();

            if (reports == null || !reports.Any())
            {
                return NotFound();
            }

            return Ok(reports);
        }
    }
}


