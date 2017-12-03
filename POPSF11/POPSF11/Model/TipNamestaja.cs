using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_GUI.Model
{
    [Serializable]
    public class TipNamestaja: INotifyPropertyChanged
    {
        private int id;
        private string naziv;
        private bool obrisan;


        public int Id
        {
            get { return id; }
            set {
                OnPropertyChanged("Id");
                id = value;
                }

        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                OnPropertyChanged("Naziv");
                naziv = value;
            }
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

        public static TipNamestaja GetById(int id)
        {
            foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            {
                if (tipNamestaja.Id == id)
                {
                    return tipNamestaja;
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
