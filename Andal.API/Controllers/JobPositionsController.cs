using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Andal.Models;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Andal.Data;
using Andal.DataAccess.Repositories.IRepositories;

namespace Andal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobPositionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPosition>>> GetJobPositions()
        {
            var model = await _unitOfWork.JobPosition.ToListAsync(
                includeProperties:
                    m => m.Include(m => m.JobTitle));

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

            var checker = await _unitOfWork.JobPosition.SingleOrDefaultAsync(
                filter:
                    m => (m.Code == jobPosition.Code ||
                    m.Name.ToUpper() == jobPosition.Name.ToUpper()) &&
                    m.PositionId != jobPosition.PositionId);

            if (checker != null)
            {
                return BadRequest("Job Position already exists");
            }

            await _unitOfWork.JobPosition.AddAsync(jobPosition);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutJobPosition(JobPosition jobPosition)
        {
            if (jobPosition == null)
            {
                return BadRequest("Invalid modelstate!");
            }

            var model = await _unitOfWork.JobPosition.SingleOrDefaultAsync(
                filter:
                    m => m.PositionId == jobPosition.PositionId);

            if (model == null)
            {
                return NotFound("Job Position notfound!");
            }

            model.Code = jobPosition.Code.Trim().ToUpper();
            model.Name = jobPosition.Name;
            model.JobTitleId = jobPosition.JobTitleId;

            var checker = await _unitOfWork.JobPosition.SingleOrDefaultAsync(
                filter:
                    m => (m.Code == model.Code ||
                    m.Name.ToUpper() == model.Name.ToUpper()) &&
                    m.PositionId != model.PositionId);

            if (checker != null)
            {
                return BadRequest("Job Position already exists");
            }

            _unitOfWork.JobPosition.Update(model);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{PositionId}")]
        public async Task<IActionResult> DeleteJobPosition(int PositionId)
        {
            var jobPosition = await _unitOfWork.JobPosition.SingleOrDefaultAsync(
                disableTracking:
                    false,
                filter:
                    m => m.PositionId == PositionId);

            if (jobPosition == null)
            {
                return NotFound("Job Position notfound!");
            }

            _unitOfWork.JobPosition.Remove(jobPosition);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
