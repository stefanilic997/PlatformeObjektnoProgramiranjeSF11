using POPSF11.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSF11.Model
{
    public class Projekat
    {
        public Projekat Instance { get;  } = new Projekat();
        public List<TipNamestaja> TipoviNamestaja { get; set; }
        private List<Namestaj> namestaj = new List<Model.Namestaj>();

        public List<Namestaj> Namestaj
        {
            get {
                this.namestaj = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
                return this.namestaj;
            }
            set {
                this.namestaj = value;
                GenericSerializer.Serialize<Namestaj>("namestaj.xml",this.namestaj);
            }
        }
        public Projekat()
        {
            Instance = new Projekat();

             

        }
    }
}
