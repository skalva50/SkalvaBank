
using Microsoft.AspNetCore.Mvc;
using SkalvaBank.Services;
using System.Threading.Tasks;

namespace SkalvaBank.Web
{
    [Route("")]
    public class OperationController : Controller
    {
        private readonly IOperationService _operationService;

        public OperationController(IOperationService operationService)
        {
            _operationService = operationService;
        }
        
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index()
        {            
            var catalogModel = await _operationService.ListeOperations();
            return View(catalogModel);
        }
    }
}