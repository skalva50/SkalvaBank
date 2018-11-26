using Microsoft.AspNetCore.Mvc.Rendering;
using SkalvaBank.Domain;
using System.Collections.Generic;

namespace SkalvaBank.Services
{
    public class OperationViewModel
    {
        public IEnumerable<Operation> ListeOperations { get; set; }

    }
}