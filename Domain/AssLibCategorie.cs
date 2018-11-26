using System;
using System.Collections.Generic;

namespace SkalvaBank.Domain
{
    public partial class AssLibCategorie : BaseEntity
    {
        public string Libelle { get; set; }
        public int? IdCategorie { get; set; }

        public Categorie IdCategorieNavigation { get; set; }
    }
}
