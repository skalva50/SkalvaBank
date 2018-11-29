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
    public class CategorieService : BaseService<Categorie>, ICategorieService
    {
        public CategorieService(IRepository<Categorie> repository, IAsyncRepository<Categorie> repositoryAsync) : base(repository, repositoryAsync)
        {
        }
        
        public async Task<IReadOnlyList<Categorie>> ListAllWithGraphAsync()
        {
            BaseSpecification<Categorie> spec = new BaseSpecification<Categorie>();
            spec.AddInclude(C => C.IdTypecategorieNavigation);
            spec.ApplyOrderBy(C => C.Libelle);

            return await _repositoryAsync.ListAsync(spec);            
        }
    }


}