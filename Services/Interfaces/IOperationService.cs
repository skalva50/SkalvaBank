using Microsoft.AspNetCore.Mvc.Rendering;
using SkalvaBank.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkalvaBank.Services
{
    public interface IOperationService : IService<Operation>
    {
        Task<IReadOnlyList<Operation>> ListAllWithGraphAsync();
        Task<Operation> GetByIdWithGraphAsync(int id);
    }

}