using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Andal.Models;
using Andal.DataAccess.Repositories.IRepositories;

namespace Andal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitlesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobTitlesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTitle>>> GetJobTitles()
        {
            var model = await _unitOfWork.JobTitle.ToListAsync();
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

            var checker = await _unitOfWork.JobTitle.SingleOrDefaultAsync(
                filter:
                    m => (m.Code == jobTitle.Code ||
                    m.Name.ToUpper() == jobTitle.Name.ToUpper()) &&
                    m.TitleId != jobTitle.TitleId);

            if (checker != null)
            {
                return BadRequest("Job Tittle already exists");
            }

            await _unitOfWork.JobTitle.AddAsync(jobTitle);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutJobTitle(JobTitle jobTitle)
        {
            if (jobTitle == null)
            {
                return BadRequest("Invalid modelstate!");
            }

            var model = await _unitOfWork.JobTitle.SingleOrDefaultAsync(
                disableTracking:
                    false,
                filter:
                    m => m.TitleId == jobTitle.TitleId);

            if (model == null)
            {
                return NotFound("Job Title notfound!");
            }

            model.Code = jobTitle.Code.Trim().ToUpper();
            model.Name = jobTitle.Name;

            var checker = await _unitOfWork.JobTitle.SingleOrDefaultAsync(
                filter:
                    m => (m.Code == model.Code ||
                    m.Name.ToUpper() == model.Name.ToUpper()) &&
                    m.TitleId != model.TitleId);

            if (checker != null)
            {
                return BadRequest("Job Tittle already exists");
            }

            _unitOfWork.JobTitle.Update(model);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{TitileId}")]
        public async Task<IActionResult> DeleteJobTitle(int TitileId)
        {
            var jobTitle = await _unitOfWork.JobTitle.SingleOrDefaultAsync(
                disableTracking:
                    false,
                filter:
                    m => m.TitleId == TitileId);

            if (jobTitle == null)
            {
                return NotFound("Job Title notfound!");
            }

            _unitOfWork.JobTitle.Remove(jobTitle);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
