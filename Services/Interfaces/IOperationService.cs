using Microsoft.AspNetCore.Mvc.Rendering;
using SkalvaBank.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkalvaBank.Services
{
    public interface IOperationService : IService<Operation>
    {
        Task<IReadOnlyList<Operation>> ListAllWithGraphAsync();
        Task<IReadOnlyList<Operation>> ListFilterWithGraphAsync(int? idCategorie,DateTime? dateoperation);
        Task<Operation> GetByIdWithGraphAsync(int id);
        double? getTotalDepensesCourant(IEnumerable<Operation> listOperation);
        double? getTotalRecettesCourant(IEnumerable<Operation> listOperation);
        void UploadOperation(List<string> listOperationString);
    }

}