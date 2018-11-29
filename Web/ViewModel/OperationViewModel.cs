using Microsoft.AspNetCore.Mvc.Rendering;
using SkalvaBank.Domain;
using System.Collections.Generic;

namespace SkalvaBank.Web
{
    public class OperationViewModel
    {
        public IEnumerable<Operation> ListOperations { get; set; }

    }
}