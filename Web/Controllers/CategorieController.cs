using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkalvaBank.Domain;
using SkalvaBank.Services;
using System.Threading.Tasks;

namespace SkalvaBank.Web
{
    [Route("[controller]/[action]")]
    public class CategorieController : Controller
    {
        private readonly ICategorieService _categorieService;
        private readonly ITypeCategorieService _typeCategorieService;

        public CategorieController(ICategorieService categorieService, ITypeCategorieService typeCategorieService)
        {
            _categorieService = categorieService;
            _typeCategorieService = typeCategorieService;
        }
        
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index()
        {  
            CategorieViewModel categorieVM = new CategorieViewModel();          
            categorieVM.ListCategories = await _categorieService.ListAllWithGraphAsync();
            return View(categorieVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categorie = await _categorieService.GetByIdWithGraphAsync(id.Value);

            if (categorie == null)
            {
                return NotFound();
            }            
            return View(categorie);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _categorieService.GetByIdWithGraphAsync(id.Value);
            if (categorie == null)
            {
                return NotFound();
            }  
            ViewData["IdTypecategorie"] = new SelectList(_typeCategorieService.ListAll(), "Id", "Libelle", categorie.IdTypecategorie);              
            return View(categorie);
        }

                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Libelle,HorsStats,IdTypecategorie")] Categorie categorie)
        {
            if (id != categorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categorieService.Update(categorie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    // if (!OperationExists(operation.Id))
                    // {
                    //     return NotFound();
                    // }
                    // else
                    // {
                    //     throw;
                    // }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTypecategorie"] = new SelectList(_typeCategorieService.ListAll(), "Id", "Libelle", categorie.IdTypecategorie);
            return View(categorie);
        }
    }
}