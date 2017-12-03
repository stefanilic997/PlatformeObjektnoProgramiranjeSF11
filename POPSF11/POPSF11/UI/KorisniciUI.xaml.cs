using POP_SF_11_GUI.Model;
using POP_SF_11_GUI.Model.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP_SF_11_GUI.UI
{
    /// <summary>
    /// Interaction logic for KorisniciUI.xaml
    /// </summary>
    public partial class KorisniciUI : Window
    {
        private Korisnik korisnik;
        private Operacija operacija;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        public KorisniciUI(Korisnik korisnik, Operacija operacija)
        {
            InitializeComponent();

            this.korisnik = korisnik;
            this.operacija = operacija;

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            tbLozinka.DataContext = korisnik;
            foreach (var item in Enum.GetValues(typeof(TipKorisnika)))
            {
                cbTipKorisnika.Items.Add(item);
            }
            cbTipKorisnika.DataContext = korisnik;
        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sacuvajKorisnika(object sender, RoutedEventArgs e)
        {
            var postojeciKorisnici = Projekat.Instance.Korisnici;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    korisnik.Id = postojeciKorisnici.Count() + 1;
                    korisnik.Ime = tbIme.Text;
                    korisnik.Prezime = tbPrezime.Text;
                    korisnik.KorisnickoIme = tbKorisnickoIme.Text;
                    korisnik.Lozinka = tbLozinka.Text;
                    korisnik.TipKorisnika = ((TipKorisnika)cbTipKorisnika.SelectedItem);

                    postojeciKorisnici.Add(korisnik);
                    break;
                case Operacija.IZMENA:
                    foreach (var k in postojeciKorisnici)
                    {
                        if (k.Id == korisnik.Id)
                        {
                            k.Ime = tbIme.Text;
                            k.Prezime = tbPrezime.Text;
                            k.KorisnickoIme = tbKorisnickoIme.Text;
                            k.Lozinka = tbLozinka.Text;
                            k.TipKorisnika = ((TipKorisnika)cbTipKorisnika.SelectedItem);
                        }
                    }
                    break;
            }
            GenericSerializer.Serialize("Korisnici.xml", postojeciKorisnici);
            this.Close();

        }
    }
}
