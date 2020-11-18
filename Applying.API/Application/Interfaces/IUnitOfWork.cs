using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IApplyRepository Applies { get; }

        void Commit();

        Task CommitAsync();
    }
}
