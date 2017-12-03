using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_GUI.Model
{
    [Serializable]
    public class AkcijskaProdaja: INotifyPropertyChanged

    {
        private int id;
        private DateTime datumPocetka;
        private DateTime datumZavresetka;
        private int popust;
        private bool obrisan;

        public int Id
        {
            get { return id; }
            set
            {
                OnPropertyChanged("Id");
                id = value;
            }
        }

        public int Popust
        {
            get { return popust; }
            set {
                OnPropertyChanged("Popust");
                popust = value; }
        }


        public DateTime DatumPocetka
        {
            get { return datumPocetka; }
            set
            {
                OnPropertyChanged("DatumPocetka");
                datumPocetka = value;
            }
        }

        public DateTime DatumZavresetka
        {
            get { return datumZavresetka; }
            set {
                OnPropertyChanged("DatumZavresetka");
                datumZavresetka = value; }
        }


        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                OnPropertyChanged("Obrisan");
                obrisan = value;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public static AkcijskaProdaja GetByIdAkcija(int id)
        {
            foreach (var AkcijskaProdaja in Projekat.Instance.AkcijskeProdaje)
            {
                if (AkcijskaProdaja.Id == id)
                {
                    return AkcijskaProdaja;
                }

            }
            return null;

        }
        public override string ToString()
        {
            return ($"Popust:{Popust}% Pocetak:{DatumPocetka.ToShortDateString()} Kraj:{DatumZavresetka.ToShortDateString()}");
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
