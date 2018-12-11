using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using SkalvaBank.Dal;
using SkalvaBank.Domain;

namespace SkalvaBank.Services
{
    public class OperationService : BaseService<Operation>, IOperationService
    {
        private readonly IAssLibCategorieService _assLibCategorieService;

        public OperationService(IRepository<Operation> repository, IAsyncRepository<Operation> repositoryAsync, IAssLibCategorieService assLibCategorieService) : base(repository, repositoryAsync)
        {
            _assLibCategorieService = assLibCategorieService;
        }

        public async Task<Operation> GetByIdWithGraphAsync(int id)
        {
            BaseSpecification<Operation> spec = new BaseSpecification<Operation>(O => O.Id == id);            
            spec.AddInclude(o => o.IdCategorieNavigation);
            return await _repositoryAsync.GetSingleBySpecAsync(spec);
        }

        public async Task<IReadOnlyList<Operation>> ListAllWithGraphAsync()
        {
            BaseSpecification<Operation> spec = new BaseSpecification<Operation>();
            spec.AddInclude(o => o.IdCategorieNavigation);
            spec.ApplyOrderBy(O => O.Dateoperation);
            return await this.ListAsync(spec);            
        }

        public async Task<IReadOnlyList<Operation>> ListFilterWithGraphAsync(int? idCategorie,DateTime? dateoperation)
        {
            OperationFilter spec = new OperationFilter(idCategorie, dateoperation);
            spec.AddInclude(o => o.IdCategorieNavigation);
            spec.ApplyOrderBy(O => O.Dateoperation);
            return await this.ListAsync(spec);            
        }

        public double? getTotalDepensesCourant(IEnumerable<Operation> listOperation)
        {
            return listOperation
                        .Where(O => O.Sens && O.IdCategorieNavigation != null && !O.IdCategorieNavigation.HorsStats && O.Numcompte == Constant.REF_COMPTE_COURANT)
                        .Sum(O => O.Montant);
        }

        public double? getTotalRecettesCourant(IEnumerable<Operation> listOperation)
        {
            return listOperation
                        .Where(O => O.Sens && O.IdCategorieNavigation != null && !O.IdCategorieNavigation.HorsStats && O.Numcompte == Constant.REF_COMPTE_COURANT)
                        .Sum(O => O.Montant);
        }

        public void UploadOperation(List<string> listOperationString)
        {
            if(listOperationString != null && listOperationString.Count > 0)
            {
                IEnumerable<Operation> listOperation  = this.ListAll();
                IEnumerable<AssLibCategorie> listAssLibCategorie =  _assLibCategorieService.ListAll();
                foreach (string ligneOperation in listOperationString)
                {
                    if(ligneOperation !=  Constant.ENTETE_FICHIER_BANQUE)
                    {
                        Operation operation = convertStringToOperation(ligneOperation);
                        if(operation != null && !OperationExists(operation , listOperation))
                        {
                            if(listAssLibCategorie != null)
                            {
                                addIdCategorieToOperation(operation, listAssLibCategorie);                                
                            }  
                            this.Add(operation);
                        }                        
                    }
                }                
            }
        }

        private void addIdCategorieToOperation(Operation operation, IEnumerable<AssLibCategorie> listAssLibCategorie)
        {
            if(listAssLibCategorie != null)
            {
                foreach (AssLibCategorie assLibCategorie in listAssLibCategorie)
                {
                    if(operation.Libelle.Contains(assLibCategorie.Libelle))
                    {
                        operation.IdCategorie = assLibCategorie.IdCategorie;
                        break;
                    }
                }
            }
        }

        private Operation convertStringToOperation(string ligneOperation)
        {            
            if(ligneOperation == null)
                return null;

            Operation operation = new Operation();
            string[] tabString = ligneOperation.Split(';');
            for (int i = 0; i < tabString.Count(); i++)
            {
                switch (i)
                {
                    case 0:
                        operation.Numcompte = tabString[0];
                        break;
                    case 2:
                    try
                    {
                        operation.Dateoperation = Convert.ToDateTime(tabString[2]);
                        break;
                    }
                    catch (System.Exception)
                    {                        
                        break;
                    }
                    case 3:
                        operation.Libelle = tabString[3];
                        break;
                    case 4:
                        operation.Reference = tabString[4];
                        break;                    
                    case 6:
                        double montant;
                        if(double.TryParse(tabString[6], out montant))
                        {
                            operation.Montant = Math.Abs(montant);
                            operation.Sens = (montant > 0);
                        }                        
                        break;                  
                    default:
                        break;
                }   
            }
            return operation;
        }

        private bool OperationExists(Operation operationToVerif, IEnumerable<Operation> listOperationExistante)
        {
            if(listOperationExistante.Any
                    (
                        O => O.Dateoperation.HasValue
                        &&  O.Dateoperation.Value == operationToVerif.Dateoperation.Value
                        && O.Libelle == operationToVerif.Libelle
                        && O.Reference == operationToVerif.Reference
                        && O.Montant == operationToVerif.Montant
                        && O.Numcompte == operationToVerif.Numcompte
                    )            
                )
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
