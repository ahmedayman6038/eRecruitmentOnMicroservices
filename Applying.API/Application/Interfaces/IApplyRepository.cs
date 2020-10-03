using Applying.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Application.Interfaces
{
    public interface IApplyRepository : IGenericRepository<Apply>
    {
        Task<bool> IsAppliedAsync(int jobId, string userId);
        Task<bool> IsAppliedAsync(int jobId, string userId, int applyId);
        Task<IReadOnlyList<Apply>> GetPagedReponseWithStatusAsync(int pageNumber, int pageSize, int statusId);
        Task<Apply> GetApplyByIdAsync(int applyId);
    }
}
