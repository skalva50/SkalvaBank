using Microsoft.AspNetCore.Mvc;

namespace SkalvaBank.Web
{
    public class GestionError : Controller
    {
        public RedirectToActionResult DisplayError(string message)
        {
            return RedirectToAction("Error","Error",new {@error=message});
        }
    }
}