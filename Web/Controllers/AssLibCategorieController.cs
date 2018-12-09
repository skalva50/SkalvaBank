using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkalvaBank.Services;

namespace SkalvaBank.Web
{
    [Route("[controller]/[action]")]
    public class AssLibCategorieController : Controller
    {
        private readonly IAssLibCategorieService _assLibCategorieService;
       
        public AssLibCategorieController(IAssLibCategorieService assLibCategorieService)
        {
            this._assLibCategorieService = assLibCategorieService;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index()
        {  
            AssLibCategorieViewModel assLibCategorieVM = new AssLibCategorieViewModel();          
            assLibCategorieVM.ListAssLibCategories = await _assLibCategorieService.ListAllAsync();
            return View(assLibCategorieVM);
        }
    }
}