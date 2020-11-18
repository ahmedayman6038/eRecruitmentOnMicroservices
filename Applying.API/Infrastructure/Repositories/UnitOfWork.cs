using Applying.API.Application.Exceptions;
using Applying.API.Application.Interfaces;
using Applying.API.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplyContext _dbContext;

        public IApplyRepository Applies { get; }

        public UnitOfWork(ApplyContext dbContext,
            IApplyRepository applyRepository)
        {
            this._dbContext = dbContext;
            this.Applies = applyRepository;
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
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
