using ApiGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Interfaces
{
    public interface IApplyingService
    {
        Task<IEnumerable<ApplyingData>> GetAll();

        Task<ApplyingData> GetById(int id);
    }
}
