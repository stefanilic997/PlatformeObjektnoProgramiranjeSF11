using POP_SF_11_GUI.Model;
using POP_SF_11_GUI.Model.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_GUI.Model
{
    public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();
        private List<TipNamestaja> tipNamestaja = new List<Model.TipNamestaja>();
        private List<Namestaj> namestaj = new List<Model.Namestaj>();
        private List<Korisnik> korisnici = new List<Model.Korisnik>();
        private List<AkcijskaProdaja> akcije = new List<Model.AkcijskaProdaja>();

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
        public List<TipNamestaja> TipoviNamestaja
        {
            get
            {
                this.tipNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipoviNamestaja.xml");
                return this.tipNamestaja;
            }
            set
            {
                this.tipNamestaja = value;
                GenericSerializer.Serialize<TipNamestaja>("tipoviNamestaja.xml", this.tipNamestaja);
            }
        }
        public List<Korisnik> Korisnici
        {
            get
            {
                this.korisnici = GenericSerializer.Deserialize<Korisnik>("Korisnici.xml");
                return this.korisnici;
            }
            set
            {
                this.korisnici = value;
                GenericSerializer.Serialize<Korisnik>("Korisnici.xml", this.korisnici);
            }
        }
        public List<AkcijskaProdaja> AkcijskeProdaje
        {
            get
            {
                this.akcije = GenericSerializer.Deserialize<AkcijskaProdaja>("Akcije.xml");
                return this.akcije;
            }
            set
            {
                this.akcije = value;
                GenericSerializer.Serialize<AkcijskaProdaja>("Akcije.xml", this.akcije);
            }
        }
    }
}
