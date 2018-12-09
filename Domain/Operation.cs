using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkalvaBank.Domain
{
    public partial class Operation : BaseEntity
    {
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime? Dateoperation { get; set; }

        [Display(Name = "Nom Operation")]
        public string Libelle { get; set; }

        [Display(Name = "Référence")]
        public string Reference { get; set; }
        
        [DataType(DataType.Currency)]
        public double? Montant { get; set; }
        
        public bool? Sens { get; set; }

        [Display(Name = "Sens")]
        public string SensString
        {
            get
            {
                if(Sens.HasValue && Sens.Value)
                {
                    return "Recettes";
                }else
                {
                    return "Depenses";
                }
            }
        }
        public int? IdCategorie { get; set; }

        [Display(Name = "Numéro compte")]
        public string Numcompte { get; set; }

        [Display(Name = "Catégorie Operation")]
        public Categorie IdCategorieNavigation { get; set; }
    }
}
