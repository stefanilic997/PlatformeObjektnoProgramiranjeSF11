using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSF11.Model
{
    public class ProdajaNamestaja
    {
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public DateTime DatumProdaje { get; set; }
        public string brojRacuna { get; set; }
        public string Kupac { get; set; }
        public List<DodatnaUsluga> DodatneUsluge { get; set; }
        public double PDV { get; set; }
        public double UkupnaCena { get; set; }

    }

}
