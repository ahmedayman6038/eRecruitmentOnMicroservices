using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IJobRepository Jobs { get; }

        void Commit();

        Task CommitAsync();
    }
}
