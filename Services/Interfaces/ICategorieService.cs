using Microsoft.AspNetCore.Mvc.Rendering;
using SkalvaBank.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkalvaBank.Services
{
    public interface ICategorieService : IService<Categorie>
    {
        Task<IReadOnlyList<Categorie>> ListAllWithGraphAsync();
        Task<Categorie> GetByIdWithGraphAsync(int id);
    }
}