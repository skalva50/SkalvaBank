using System;
using SkalvaBank.Domain;

namespace SkalvaBank.Dal
{
    public class OperationFilter : BaseSpecification<Operation>
    {
        public OperationFilter(int? idCategorie,DateTime? dateoperation)
            : base(o => (!idCategorie.HasValue || o.IdCategorie == idCategorie) &&
                (!dateoperation.HasValue || (o.Dateoperation.Value.Month == dateoperation.Value.Month && o.Dateoperation.Value.Year == dateoperation.Value.Year)))
        {
        }
    }
}