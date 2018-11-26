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
    public class OperationService : IOperationService
    {        
        private readonly IAsyncRepository<Operation> _itemRepository;

        public OperationService(IAsyncRepository<Operation> itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<OperationViewModel> ListeOperations()
        {
            BaseSpecification<Operation> spec = new BaseSpecification<Operation>(O => O.Dateoperation.Value.Month == 9);
            spec.AddInclude(o => o.IdCategorieNavigation);
            spec.ApplyOrderBy(O => O.Dateoperation);
            spec.ApplyPaging(0,20);

            IEnumerable<Operation> items = await _itemRepository.ListAsync(spec);
            var vm = new OperationViewModel();
            vm.ListeOperations = items;
            return vm;            
        }
    }


}