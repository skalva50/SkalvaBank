using System;
using System.Collections.Generic;

namespace SkalvaBank.Domain
{
    public partial class Categorie : BaseEntity
    {
        public Categorie()
        {
            AssLibCategorie = new HashSet<AssLibCategorie>();
            Operation = new HashSet<Operation>();
        }

        public string Libelle { get; set; }
        public bool? HorsStats { get; set; }
        public int? IdTypecategorie { get; set; }

        public Typecategorie IdTypecategorieNavigation { get; set; }
        public ICollection<AssLibCategorie> AssLibCategorie { get; set; }
        public ICollection<Operation> Operation { get; set; }
    }
}
