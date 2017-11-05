using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSF11.Model
{
    [Serializable]
    public class Namestaj
    {
        public int Id { get; set;}
        public string Naziv { get; set; }
        public int Cena { get; set; }
        public int Kolicina { get; set; }
        public bool Obrisan { get; set; }
        public TipNamestaja TipNamestaja { get; set; }
        public AkcijskaProdaja Akcija { get; set; }

    }
}
