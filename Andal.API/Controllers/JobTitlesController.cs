using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Andal.Data;
using Andal.Models;
using Microsoft.AspNetCore.Cors;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Andal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitlesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobTitlesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTitle>>> GetJobTitles()
        {
            var model = await _context.JobTitles.AsNoTracking().ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> PostJobTitle(JobTitle jobTitle)
        {
            if (jobTitle == null)
            {
                return BadRequest("Invalid modelstate!");
            }

            jobTitle.Code = jobTitle.Code.Trim().ToUpper();

            var checker = await _context.JobTitles.AsNoTracking().SingleOrDefaultAsync(
                m => (m.Code == jobTitle.Code ||
                m.Name.ToUpper() == jobTitle.Name.ToUpper()) &&
                m.TitleId != jobTitle.TitleId);

            if (checker != null)
            {
                return BadRequest("Job Tittle already exists");
            }

            _context.JobTitles.Add(jobTitle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutJobTitle(JobTitle jobTitle)
        {
            if (jobTitle == null)
            {
                return BadRequest("Invalid modelstate!");
            }

            var model = await _context.JobTitles.SingleOrDefaultAsync(m => m.TitleId == jobTitle.TitleId);

            if (model == null)
            {
                return NotFound("Job Title notfound!");
            }

            model.Code = jobTitle.Code.Trim().ToUpper();
            model.Name = jobTitle.Name;

            var checker = await _context.JobTitles.AsNoTracking().SingleOrDefaultAsync(
                m => (m.Code == model.Code ||
                m.Name.ToUpper() == model.Name.ToUpper()) &&
                m.TitleId != model.TitleId);

            if (checker != null)
            {
                return BadRequest("Job Tittle already exists");
            }

            _context.JobTitles.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{TitileId}")]
        public async Task<IActionResult> DeleteJobTitle(int TitileId)
        {
            var jobTitle = await _context.JobTitles.FindAsync(TitileId);

            if (jobTitle == null)
            {
                return NotFound("Job Title notfound!");
            }

            _context.JobTitles.Remove(jobTitle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
