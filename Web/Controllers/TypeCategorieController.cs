using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkalvaBank.Domain;
using SkalvaBank.Services;

namespace SkalvaBank.Web
{
    [Route("[controller]/[action]")]
    public class TypeCategorieController : Controller
    {
        private readonly ITypeCategorieService _typeCategorieService;

        public TypeCategorieController(ITypeCategorieService typeCategorieService){
            _typeCategorieService = typeCategorieService;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index()
        {  
            TypeCategorieViewModel typeCategorieVM = new TypeCategorieViewModel();          
            typeCategorieVM.ListTypeCategories = await _typeCategorieService.ListAllAsync();
            return View(typeCategorieVM);
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeCategorie = await _typeCategorieService.GetByIdAsync(id.Value);
            if (typeCategorie == null)
            {
                return NotFound();
            }              
            return View(typeCategorie);
        }
                        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Libelle")] Typecategorie typeCategorie)
        {
            if (id != typeCategorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _typeCategorieService.Update(typeCategorie);
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
            return View(typeCategorie);
        }


        // POST: Typecategorie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Libelle")] Typecategorie typecategorie)
        {
            if (ModelState.IsValid)
            {
                await _typeCategorieService.AddAsync(typecategorie);                
                return RedirectToAction(nameof(Index));
            }
            return View(typecategorie);
        }

                // GET: Typecategorie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typecategorie = await _typeCategorieService.GetByIdAsync(id.Value);
            if (typecategorie == null)
            {
                return NotFound();
            }

            return View(typecategorie);
        }

        // POST: Typecategorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typecategorie = await _typeCategorieService.GetByIdAsync(id);                
            try
            {
                await _typeCategorieService.DeleteAsync(typecategorie);
            }
            catch (System.Exception ex)
            {
                return new GestionError().DisplayError(ex.InnerException.Message);                
            }        
            return RedirectToAction(nameof(Index));
        }
    }
}