using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkalvaBank.Domain
{
    public partial class Categorie : BaseEntity
    {
        public Categorie()
        {
            AssLibCategorie = new HashSet<AssLibCategorie>();
            Operation = new HashSet<Operation>();
        }

        [Display(Name = "Categorie Opération")]
        public string Libelle { get; set; }

        [Display(Name = "Hors Statistiques")]        
        public bool HorsStats { get; set; }
        
        public string HorsStatsString
        {
            get
            {
                if(HorsStats)
                {
                    return "Oui";
                }else
                {
                    return "Non";
                }
            }
        }
        public int? IdTypecategorie { get; set; }

        [Display(Name = "Type Catégorie")]
        public Typecategorie IdTypecategorieNavigation { get; set; }
        public ICollection<AssLibCategorie> AssLibCategorie { get; set; }
        public ICollection<Operation> Operation { get; set; }
    }
}
