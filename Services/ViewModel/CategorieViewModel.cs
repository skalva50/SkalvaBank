using Microsoft.AspNetCore.Mvc.Rendering;
using SkalvaBank.Domain;
using System.Collections.Generic;

namespace SkalvaBank.Services
{
    public class CategorieViewModel
    {
        public IEnumerable<Categorie> ListCategories { get; set; }

    }
}