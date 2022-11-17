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
    public class JobTitleRepository : Repository<JobTitle>, IJobTitleRepository
    {
        private readonly AppDbContext db;

        public JobTitleRepository(AppDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(JobTitle m)
        {
            db.Update(m);
        }
    }
}
