
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkalvaBank.Domain;
using SkalvaBank.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkalvaBank.Web
{
    [Route("[controller]/[action]")]
    public class OperationController : Controller
    {
        private readonly IOperationService _operationService;
        private readonly ICategorieService _categorieService;

        public OperationController(IOperationService operationService, ICategorieService categorieService)
        {
            _operationService = operationService;
            _categorieService = categorieService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            var operationViewModel = new OperationViewModel();
            operationViewModel.ListOperations = await _operationService.ListAllWithGraphAsync();
            return View(operationViewModel);
        }

        // GET: Operation/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var operation = await _operationService.GetByIdWithGraphAsync(id.Value);

            if (operation == null)
            {
                return NotFound();
            }
            return View(operation);
        }

        // GET: Operation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operation = await _operationService.GetByIdAsync(id.Value); 
            if (operation == null)
            {
                return NotFound();
            }
           // IEnumerable<Categorie> test = _categorieService.ListAll();
            ViewData["IdCategorie"] = new SelectList(_categorieService.ListAll(), "Id", "Libelle", operation.IdCategorie);
            return View(operation);
        }

        // POST: Operation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dateoperation,Libelle,Reference,Montant,Sens,IdCategorie,Numcompte")] Operation operation)
        {
            if (id != operation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _operationService.Update(operation);
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
            ViewData["IdCategorie"] = new SelectList(_categorieService.ListAll(), "Id", "Libelle", operation.IdCategorie);
            return View(operation);
        }

    }
}