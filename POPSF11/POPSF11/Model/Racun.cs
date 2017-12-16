using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_GUI.Model
{
    [Serializable]
    public class Racun : INotifyPropertyChanged
    {
        
        public const double PDV = 0.02;
        private int id;
        private int kolicina;
        private Namestaj namestaj;
        private DateTime datumProdaje;
        private string brojRacuna;
        private string kupac;
        private DodatnaUsluga dodatneUsluge;
        private double ukupnaCena;
        private bool obrisan;


        public event PropertyChangedEventHandler PropertyChanged;


        public int Id
        {
            get { return id; }
            set {
                OnPropertyChanged("Id");
                id = value; }
        }

        public int Kolicina
        {
            get { return kolicina; }
            set {
                OnPropertyChanged("Kolicina");
                kolicina = value; }
        }
        public Namestaj Namestaj
        {
            get { return namestaj; }
            set
            {
                OnPropertyChanged("Namestaj");
                namestaj = value;
            }
        }

        public DateTime DatumProdaje
        {
            get { return datumProdaje; }
            set { datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
            }
        }


        public string BrojRacuna
        {
            get { return brojRacuna; }
            set { brojRacuna = value;
                OnPropertyChanged("BrojRacuna");
            }
        }

        public string Kupac
        {
            get { return kupac; }
            set { kupac = value;
                OnPropertyChanged("Kupac");
            }
        }

        public DodatnaUsluga DodatneUsluge
        {
            get { return dodatneUsluge; }
            set { dodatneUsluge = value;
                OnPropertyChanged("DodatneUsluge");
            }
        }


        public double UkupnaCena
        {
            get { return ukupnaCena; }
            set { ukupnaCena = (Namestaj.Cena * kolicina + DodatneUsluge.Cena)+ (Namestaj.Cena * kolicina + DodatneUsluge.Cena)*PDV;
                OnPropertyChanged("UkupnaCena");
            }
        }

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }
        public static Racun GetById(int id)
        {
            foreach (var Racun in Projekat.Instance.Racuni)
            {
                if (Racun.Id == id)
                {
                    return Racun;
                }

            }
            return null;

        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
