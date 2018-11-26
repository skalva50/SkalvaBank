using System;
using System.Collections.Generic;

namespace SkalvaBank.Domain
{
    public partial class Typecategorie : BaseEntity
    {
        public Typecategorie()
        {
            Categorie = new HashSet<Categorie>();
        }
        public string Libelle { get; set; }

        public ICollection<Categorie> Categorie { get; set; }
    }
}
