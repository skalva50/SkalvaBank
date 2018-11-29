using Microsoft.AspNetCore.Mvc;
using SkalvaBank.Services;
using System.Threading.Tasks;

namespace SkalvaBank.Web
{
    [Route("Categorie")]
    public class CategorieController : Controller
    {
        private readonly ICategorieService _categorieService;

        public CategorieController(ICategorieService categorieService)
        {
            _categorieService = categorieService;
        }
        
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index()
        {  
            CategorieViewModel categorieVM = new CategorieViewModel();          
            categorieVM.ListCategories = await _categorieService.ListAllWithGraphAsync();
            return View(categorieVM);
        }
    }
}