

using System.Collections.Generic;
using System.Threading.Tasks;
using SkalvaBank.Dal;
using SkalvaBank.Domain;

namespace SkalvaBank.Services
{
    public class TypeCategorieService : BaseService<Typecategorie>, ITypeCategorieService
    {
        public TypeCategorieService(IRepository<Typecategorie> repository, IAsyncRepository<Typecategorie> repositoryAsync) : base(repository, repositoryAsync)
        {
            
        }
    }
}