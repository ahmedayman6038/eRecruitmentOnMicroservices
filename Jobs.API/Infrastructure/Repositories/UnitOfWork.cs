using Jobs.API.Application.Exceptions;
using Jobs.API.Application.Interfaces;
using Jobs.API.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JobContext _dbContext;

        public IJobRepository Jobs { get; }

        public UnitOfWork(JobContext dbContext,
            IJobRepository jobRepository)
        {
            this._dbContext = dbContext;
            this.Jobs = jobRepository;
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
