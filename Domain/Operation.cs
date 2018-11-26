using System;
using System.Collections.Generic;

namespace SkalvaBank.Domain
{
    public partial class Operation : BaseEntity
    {
        public DateTime? Dateoperation { get; set; }
        public string Libelle { get; set; }
        public string Reference { get; set; }
        public double? Montant { get; set; }
        public bool? Sens { get; set; }
        public int? IdCategorie { get; set; }
        public string Numcompte { get; set; }

        public Categorie IdCategorieNavigation { get; set; }
    }
}
