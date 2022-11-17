using Andal.Models;

namespace Andal.DataAccess.Repositories.IRepositories
{
    public interface IJobPositionRepository : IRepository<JobPosition>
    {
        void Update(JobPosition m);
    }
}
