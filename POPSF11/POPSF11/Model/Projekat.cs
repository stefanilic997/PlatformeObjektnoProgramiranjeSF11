﻿using POP_SF_11_GUI.Model;
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
        public ObservableCollection<Namestaj> Namestaj;
        public ObservableCollection<Korisnik> Korisnici;
        public ObservableCollection<AkcijskaProdaja> AkcijskeProdaje;
        public ObservableCollection<DodatnaUsluga> DodatneUsluge;
        public ObservableCollection<Racun> Racuni;
        public ObservableCollection<Salon> Saloni;

        private Projekat()
        {
            TipoviNamestaja = TipNamestaja.GetAll();
            Namestaj = new ObservableCollection<Namestaj>(GenericSerializer.Deserialize<Namestaj>("namestaj.xml"));
            Korisnici = new ObservableCollection<Korisnik>(GenericSerializer.Deserialize<Korisnik>("Korisnici.xml"));
            AkcijskeProdaje = new ObservableCollection<AkcijskaProdaja>(GenericSerializer.Deserialize<AkcijskaProdaja>("Akcije.xml"));
            DodatneUsluge = new ObservableCollection<DodatnaUsluga>(GenericSerializer.Deserialize<DodatnaUsluga>("DodatneUsluge.xml"));
            Racuni = new ObservableCollection<Racun>(GenericSerializer.Deserialize<Racun>("Racuni.xml"));
            Saloni = new ObservableCollection<Salon>(GenericSerializer.Deserialize<Salon>("Saloni.xml"));
        }
    }
}
