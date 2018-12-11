using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SkalvaBank.Web
{
    [Route("[controller]/[action]")]
    public class ErrorController : Controller
    {
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Error(string error) 
        {  
            ErrorViewModel errorVM = new ErrorViewModel();
            errorVM.RequestId = error;   
            return View(errorVM);
        }
    }
}