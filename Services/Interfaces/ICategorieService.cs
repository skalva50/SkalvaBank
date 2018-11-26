using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkalvaBank.Services
{
    public interface ICategorieService
    {
        Task<CategorieViewModel> ListeCategories();
    }
}