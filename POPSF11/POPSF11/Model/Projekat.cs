
using POP_SF_11_GUI.Model;
using POP_SF_11_GUI.Model.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_GUI.Model
{
    public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();
        public ObservableCollection<TipNamestaja> TipoviNamestaja;
        public ObservableCollection<Namestaj> sviNamestaji;
        public ObservableCollection<Korisnik> Korisnici;
        public ObservableCollection<AkcijskaProdaja> AkcijskeProdaje;
        public ObservableCollection<DodatnaUsluga> DodatneUsluge;
        public ObservableCollection<Racun> Racuni;
        public ObservableCollection<Salon> Saloni;
        public ObservableCollection<StavkaProdajeDodatnaUsluga> SPDodatneUsluge;
        public ObservableCollection<StavkaProdajeNamestaj> SPNamestaj;

        private Projekat()
        {
            TipoviNamestaja = TipNamestaja.GetAll();
            sviNamestaji = Namestaj.GetAll();
            Korisnici = Korisnik.GetAll();
            AkcijskeProdaje = AkcijskaProdaja.GetAll();
            DodatneUsluge = DodatnaUsluga.GetAll();
            Racuni = Racun.GetAll();
            SPDodatneUsluge = StavkaProdajeDodatnaUsluga.GetAll();
            SPNamestaj = StavkaProdajeNamestaj.GetAll();
            Saloni = Salon.GetAll();
        }
    }
}
