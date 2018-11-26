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
    public class CategorieService : ICategorieService
    {        
        private readonly IAsyncRepository<Categorie> _itemRepository;

        public CategorieService(IAsyncRepository<Categorie> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<CategorieViewModel> ListeCategories()
        {
            BaseSpecification<Categorie> spec = new BaseSpecification<Categorie>();
            spec.AddInclude(C => C.IdTypecategorieNavigation);
            spec.ApplyOrderBy(C => C.Libelle);

            IEnumerable<Categorie> liste = await _itemRepository.ListAsync(spec);
            var vm = new CategorieViewModel();
            vm.ListCategories = liste;
            return vm;   
        }
    }


}