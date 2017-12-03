using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_GUI.Model
{
    [Serializable]
    public enum TipKorisnika
    {
        Administrator,
        Prodavac
    }
    public class Korisnik : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private string ime;
        private string prezime;
        private string korisnickoIme;
        private string lozinka;
        private TipKorisnika tipKorisnika;
        private bool obrisan;



        public int Id
        {
            get { return id; }
            set {
                OnPropertyChanged("Id");
                id = value; }
        }

        public string Ime
        {
            get { return ime; }
            set {
                OnPropertyChanged("Ime");
                ime = value; }
        }

        public string Prezime
        {
            get { return prezime; }
            set {
                OnPropertyChanged("Prezime");
                prezime = value; }
        }
        

        public string KorisnickoIme
        {
            get { return korisnickoIme; }
            set {
                OnPropertyChanged("KorisnickoIme");
                korisnickoIme = value; }
        }

        public string Lozinka
        {
            get { return lozinka; }
            set {
                OnPropertyChanged("Lozinka");
                lozinka = value; }
        }

        public TipKorisnika TipKorisnika
        {
            get { return tipKorisnika; }
            set {
                OnPropertyChanged("TipKorisnika");
                tipKorisnika = value; }
        }


        public bool Obrisan
        {
            get { return obrisan; }
            set {
                OnPropertyChanged("Obrisan");

                obrisan = value; }
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
