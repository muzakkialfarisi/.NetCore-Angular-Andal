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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            JobTitle = new JobTitleRepository(_db);
            JobPosition = new JobPositionRepository(_db);
        }

        public IJobTitleRepository JobTitle { get; private set; }
        public IJobPositionRepository JobPosition { get; private set; }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
