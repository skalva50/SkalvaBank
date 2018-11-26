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
            var categorieModel = await _categorieService.ListeCategories();
            return View(categorieModel);
        }
    }
}