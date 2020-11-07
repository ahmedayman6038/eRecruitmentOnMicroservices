using Applying.API.Application.Interfaces;
using Applying.API.Application.Entities;
using Applying.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Infrastructure.Repositories
{
    public class ApplyRepository : GenericRepository<Apply>, IApplyRepository
    {
        private readonly DbSet<Apply> _applies;

        public ApplyRepository(ApplyContext dbContext) : base(dbContext)
        {
            _applies = dbContext.Set<Apply>();
        }
        public async Task<IReadOnlyList<Apply>> GetPagedReponseWithStatusAsync(int pageNumber, int pageSize, int statusId)
        {
            var applies = _applies.AsQueryable();
            if(statusId != 0)
            {
                applies = applies.Where(j => j.Status == statusId);
            }
            return await applies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<bool> IsAppliedAsync(int jobId, string userId)
        {
            return _applies
                .AnyAsync(a => a.JobId == jobId && a.UserId == userId);
        }

        public Task<bool> IsAppliedAsync(int jobId, string userId, int applyId)
        {
            return _applies
                .AnyAsync(a => a.JobId == jobId && a.UserId == userId && a.Id != applyId);
        }

        public Task<Apply> GetApplyByIdAsync(int applyId)
        {
            return  _applies
                .Where(a => a.Id == applyId)
                .FirstOrDefaultAsync();
        }
    }
}
