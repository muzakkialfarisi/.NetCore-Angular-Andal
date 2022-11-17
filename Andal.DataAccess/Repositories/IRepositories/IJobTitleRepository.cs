using Andal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andal.DataAccess.Repositories.IRepositories
{
    public interface IJobTitleRepository : IRepository<JobTitle>
    {
        void Update(JobTitle m);
    }
}
