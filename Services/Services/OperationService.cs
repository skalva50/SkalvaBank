using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using SkalvaBank.Dal;
using SkalvaBank.Domain;

namespace SkalvaBank.Services
{
    public class OperationService : BaseService<Operation>, IOperationService
    {
        public OperationService(IRepository<Operation> repository, IAsyncRepository<Operation> repositoryAsync) : base(repository, repositoryAsync)
        {
        }

        public async Task<Operation> GetByIdWithGraphAsync(int id)
        {
            BaseSpecification<Operation> spec = new BaseSpecification<Operation>(O => O.Id == id);            
            spec.AddInclude(o => o.IdCategorieNavigation);
            return await _repositoryAsync.GetSingleBySpecAsync(spec);
        }

        public async Task<IReadOnlyList<Operation>> ListAllWithGraphAsync()
        {
            BaseSpecification<Operation> spec = new BaseSpecification<Operation>(O => O.Dateoperation.Value.Month == 9);
            spec.AddInclude(o => o.IdCategorieNavigation);
            spec.ApplyOrderBy(O => O.Dateoperation);
            spec.ApplyPaging(0,20);
            return await this.ListAsync(spec);            
        }
    }
}