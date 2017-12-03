using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_GUI.Model
{
    [Serializable]
    public class DodatnaUsluga : INotifyPropertyChanged
    {
        private int id;
        private string naziv;
        private double cena;
        private bool obrisan;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get {
                return id;
                }
            set
            {

                OnPropertyChanged("Id");
                id = value;
            }
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

        public bool Obrisan
        {
            get { return obrisan; }
            set {
                OnPropertyChanged("Obrisan");
                obrisan = value; }
        }

        public static DodatnaUsluga GetById(int id)
        {
            foreach (var DodatnaUsluga in Projekat.Instance.DodatneUsluge)
            {
                if (DodatnaUsluga.Id == id)
                {
                    return DodatnaUsluga;
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
