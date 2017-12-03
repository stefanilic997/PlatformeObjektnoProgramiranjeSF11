using POP_SF_11_GUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF_11_GUI.Model
{
    [Serializable]
    public class Namestaj : INotifyPropertyChanged
    {
        private int id;
        private bool obrisan;
        private string naziv;
        private double cena;
        private int kolicina;
        private int tipNamestajaId;
        public event PropertyChangedEventHandler PropertyChanged;
        private TipNamestaja tipNamestaja;

        [XmlIgnore]
        public TipNamestaja TipNamestaja
        {
            get {
                if (tipNamestaja == null)
                {
                    tipNamestaja = TipNamestaja.GetById(tipNamestajaId);
                }
                return tipNamestaja;
                }

            set {
                tipNamestaja = value;
                TipNamestajaId = tipNamestaja.Id;
                OnPropertyChanged("TipNamestaja");
                }
        }

        public int Id
        {
            get { return id; }
            set {
                OnPropertyChanged("Id");
                id = value; }
        }

        public string Naziv
        {
            get { return naziv; }
            set {
                OnPropertyChanged("Naziv");
                naziv = value; }
        }

        public double Cena
        {
            get { return cena; }
            set {
                OnPropertyChanged("Cena");
                cena = value; }
        }

        public int Kolicina
        {
            get { return kolicina; }
            set {
                OnPropertyChanged("Kolicina");
                kolicina = value; }
        }

        public int TipNamestajaId
        {
            get { return tipNamestajaId; }
            set
            {
                OnPropertyChanged("TipNamestajaId");

                tipNamestajaId = value; }
        }

        public bool Obrisan
        {
            get { return obrisan; }
            set {
                OnPropertyChanged("Obrisan");
                obrisan = value; }
        }
        
        public static Namestaj GetById(int id)
        {
            foreach (var Namestaj in Projekat.Instance.Namestaj)
            {
                if (Namestaj.Id == id)
                {
                    return Namestaj;
                }

            }
            return null;

        }
        public override string ToString()
        {
            return Naziv;
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
