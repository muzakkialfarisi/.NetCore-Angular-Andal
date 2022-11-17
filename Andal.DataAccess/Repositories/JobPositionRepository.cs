using Andal.Data;
using Andal.DataAccess.Repositories.IRepositories;
using Andal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andal.DataAccess.Repositories
{
    public class JobPositionRepository : Repository<JobPosition>, IJobPositionRepository
    {
        private readonly AppDbContext db;

        public JobPositionRepository(AppDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(JobPosition m)
        {
            db.Update(m);
        }
    }
}
