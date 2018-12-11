using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkalvaBank.Services;

namespace SkalvaBank.Web
{
    [Route("[controller]/[action]")]
    public class UploadFilesController : Controller
    {
        private readonly IOperationService _operationService;

        public UploadFilesController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {  
            return View();
        }
        
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            foreach (var formFile in files)
            {
                List<string> listLigneFichier = new List<string>();
                using (var reader = new StreamReader(formFile.OpenReadStream()))
                {
                    while(reader.ReadLine() != null){
                        listLigneFichier.Add(reader.ReadLine());
                    }                                     
                }
                _operationService.UploadOperation(listLigneFichier);
            }
            return RedirectToAction(nameof(Index));
        }       
    }
}