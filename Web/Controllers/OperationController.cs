
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SkalvaBank.Domain;
using SkalvaBank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Index(int? idCategorie, DateTime? dateSelected)
        {  
            // Recuperation du filtre si celui a été activé
            CookieFilterOperation cookie = new CookieFilterOperation().GetCookieFilterOperation(Request);
            if(cookie != null && cookie.Activer)
            {
                idCategorie = cookie.IdCategorie;
                dateSelected = cookie.DateSelected;
            }
            cookie = new CookieFilterOperation(idCategorie, dateSelected, false, Response);
            
            OperationViewModel operationVM = new OperationViewModel();            
            operationVM.IdCategorie = idCategorie;    
            operationVM.DateSelected = dateSelected;
            var categories = _categorieService.ListAll().OrderBy(C => C.Libelle);

            operationVM.ListOperations = await _operationService.ListFilterWithGraphAsync(idCategorie,dateSelected);
            operationVM.TotalDepenses = _operationService.getTotalDepensesCourant(operationVM.ListOperations);
            operationVM.TotalRecettes = _operationService.getTotalRecettesCourant(operationVM.ListOperations);
            
            operationVM.Categories = new SelectList(categories, "Id", "Libelle");             
            return View(operationVM);
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
            new CookieFilterOperation().ActiverCookieFilterOperation(Request, Response);
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
            new CookieFilterOperation().ActiverCookieFilterOperation(Request, Response);
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
            new CookieFilterOperation().ActiverCookieFilterOperation(Request, Response);
            return View(operation);
        }

    }
}