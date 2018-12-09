using SkalvaBank.Dal;
using SkalvaBank.Domain;

namespace SkalvaBank.Services
{
    public class AssLibCategorieService : BaseService<AssLibCategorie>, IAssLibCategorieService
    {
        public AssLibCategorieService(IRepository<AssLibCategorie> repository, IAsyncRepository<AssLibCategorie> repositoryAsync) : base(repository, repositoryAsync)
        {
        }
    }
}