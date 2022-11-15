using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Andal.Data;
using Andal.Models;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace Andal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobPositionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPosition>>> GetJobPositions()
        {
            var model = await _context.JobPositions.AsNoTracking()
                .Include(m => m.JobTitle).ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> PostJobPosition(JobPosition jobPosition)
        {
            if (jobPosition == null)
            {
                return BadRequest("Invalid modelstate!");
            }

            jobPosition.Code = jobPosition.Code.Trim().ToUpper();

            var checker = await _context.JobPositions.AsNoTracking().SingleOrDefaultAsync(
                m => (m.Code == jobPosition.Code ||
                m.Name.ToUpper() == jobPosition.Name.ToUpper()) &&
                m.PositionId != jobPosition.PositionId);

            if (checker != null)
            {
                return BadRequest("Job Position already exists");
            }

            _context.JobPositions.Add(jobPosition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutJobPosition(JobPosition jobPosition)
        {
            if (jobPosition == null)
            {
                return BadRequest("Invalid modelstate!");
            }

            var model = await _context.JobPositions.SingleOrDefaultAsync(m => m.PositionId == jobPosition.PositionId);

            if (model == null)
            {
                return NotFound("Job Position notfound!");
            }

            model.Code = jobPosition.Code.Trim().ToUpper();
            model.Name = jobPosition.Name;
            model.JobTitleId = jobPosition.JobTitleId;

            var checker = await _context.JobPositions.AsNoTracking().SingleOrDefaultAsync(
                m => (m.Code == model.Code ||
                m.Name.ToUpper() == model.Name.ToUpper()) &&
                m.PositionId != model.PositionId);

            if (checker != null)
            {
                return BadRequest("Job Position already exists");
            }

            _context.JobPositions.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{PositionId}")]
        public async Task<IActionResult> DeleteJobPosition(int PositionId)
        {
            var jobPosition = await _context.JobPositions.FindAsync(PositionId);

            if (jobPosition == null)
            {
                return NotFound("Job Position notfound!");
            }

            _context.JobPositions.Remove(jobPosition);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
