using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andal.DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IJobTitleRepository JobTitle { get; }
        IJobPositionRepository JobPosition { get; }

        Task SaveChangesAsync();
    }
}
