using POP_SF_11_GUI.Model;
using POP_SF_11_GUI.Model.util;
using POP_SF_11_GUI.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF_11_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ICollectionView view;
        private Podaci podaci;


        public enum Podaci
        {
            Namestaj,
            TipNamestaja,
            AkcijskeProdaje,
            Korisnici,
            DodatneUsluge,
            RacunProdaje,
            Salon
        }
        public MainWindow()
        {
            InitializeComponent();
            TipNamestaja noviTip = TipNamestaja.Create(new TipNamestaja(){
                Naziv = "cekam Id",
                Obrisan = false
            });
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.sviNamestaji);
            dgNamestaj.ItemsSource = view;
            view.Filter = NamestajFilter;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

        }

        

        

        private bool NamestajFilter(object obj)
        {
            return ((Namestaj)obj).Obrisan == false;
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Dodaj(object sender, RoutedEventArgs e)
        {

            switch (podaci)
            {
                case Podaci.Namestaj:
                    DodajNamestaj();
                    break;
                case Podaci.TipNamestaja:
                    DodajTipNamestaja();
                    break;
                case Podaci.AkcijskeProdaje:
                    DodajAkciju();
                    break;
                case Podaci.Korisnici:
                    DodajKorisnika();
                    break;
                case Podaci.DodatneUsluge:
                    DodajDodatnuUslugu();
                    break;
                case Podaci.RacunProdaje:
                    DodajRacunProdaje();
                    break;
                case Podaci.Salon:
                    DodajSalon();
                    break;
            }
        }

        

        private void Izmeni(object sender, RoutedEventArgs e)
        {

            switch (podaci)
            {
                case Podaci.Namestaj:
                    IzmeniNamestaj();
                    break;
                case Podaci.TipNamestaja:
                    IzmeniTipNamestaja();
                    break;
                case Podaci.AkcijskeProdaje:
                    IzmeniAkciju();
                    break;
                case Podaci.Korisnici:
                    IzmeniKorisnika();
                    break;
                case Podaci.DodatneUsluge:
                    IzmeniDodatnuUslugu();
                    break;
                case Podaci.RacunProdaje:
                    IzmeniRacunProdaje();
                    break;
                case Podaci.Salon:
                    IzmeniSalon();
                    break;
            }
        }

       

        private void Obrisi(object sender, RoutedEventArgs e)
        {

            switch (podaci)
            {
                case Podaci.Namestaj:
                    ObrisiNamestaj();
                    break;
                case Podaci.TipNamestaja:
                    ObrisiTipNamestaja();
                    break;
                case Podaci.AkcijskeProdaje:
                    ObrisiAkciju();
                    break;
                case Podaci.Korisnici:
                    ObrisiKorisnika();
                    break;
                case Podaci.DodatneUsluge:
                    ObrisiDodatnuUslugu();
                    break;
                case Podaci.RacunProdaje:
                    ObrisiRacunProdaje();
                    break;
                case Podaci.Salon:
                    ObrisiSalon();
                    break;
            }
        }
        private void DodajSalon()
        {

            var noviSalon = new Salon()
            {
                Naziv = "",
                Adresa = "",
                Telefon = "",
                Email = "",
                AdresaSajta = "",
                PIB = 0,
                MaticniBroj = 0,
                BrojZiroRacuna = ""

            };
            var saloniProzor = new SalonWindow(noviSalon, SalonWindow.Operacija.DODAVANJE);
            saloniProzor.ShowDialog();

        }
        private void IzmeniSalon()
        {
            var selektovaniSalon = (Salon)dgNamestaj.SelectedItem;
            var saloniProzor = new SalonWindow(selektovaniSalon, SalonWindow.Operacija.IZMENA);
            saloniProzor.ShowDialog();
        }

        private void ObrisiSalon()
        {
            var selektovaniSalon = (Salon)dgNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {selektovaniSalon.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var n in Projekat.Instance.Saloni)
                {
                    if (n.Id == selektovaniSalon.Id)
                    {

                        n.Obrisan = true;
                        view.Refresh();
                        break;
                    }
                }
            }
            GenericSerializer.Serialize("DodatneUsluge.xml", Projekat.Instance.DodatneUsluge);
        }

        private void DodajDodatnuUslugu()
        {
            var novaDodatnaUsluga = new DodatnaUsluga()
            {
                Naziv = "",
                Cena = 0
            };
            var dodatneUslugeProzor = new DodatneUslugeWindow(novaDodatnaUsluga, DodatneUslugeWindow.Operacija.DODAVANJE);
            dodatneUslugeProzor.ShowDialog();
        }
        private void IzmeniDodatnuUslugu()
        {
            var selektovanaUsluga = (DodatnaUsluga)dgNamestaj.SelectedItem;
            var dodatnaUslugaProzor = new DodatneUslugeWindow(selektovanaUsluga, DodatneUslugeWindow.Operacija.IZMENA);
            dodatnaUslugaProzor.ShowDialog();
        }

        private void ObrisiDodatnuUslugu()
        {
            var selektovanaUsluga = (DodatnaUsluga)dgNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {selektovanaUsluga.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var n in Projekat.Instance.DodatneUsluge)
                {
                    if (n.Id == selektovanaUsluga.Id)
                    {
                        
                        n.Obrisan = true;
                        view.Refresh();
                        break;
                    }
                }
            }
            GenericSerializer.Serialize("DodatneUsluge.xml", Projekat.Instance.DodatneUsluge);
        }

        private void DodajRacunProdaje()
        {
            var noviRacun = new Racun()
            {
                Kolicina = 0,
                Kupac = "",
                BrojRacuna = ""

            };
            var racuniProzor = new ProdajaNamestajaWindow(noviRacun, ProdajaNamestajaWindow.Operacija.DODAVANJE);
            racuniProzor.ShowDialog();
        }

        private void IzmeniRacunProdaje()
        {
            var selektovaniRacun = (Racun)dgNamestaj.SelectedItem;
            var racuniProzor = new ProdajaNamestajaWindow(selektovaniRacun, ProdajaNamestajaWindow.Operacija.IZMENA);
            racuniProzor.ShowDialog();
        }
        private void ObrisiRacunProdaje()
        {
            var selektovaniRacun = (Racun)dgNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {selektovaniRacun.BrojRacuna}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var n in Projekat.Instance.Korisnici)
                {
                    if (n.Id == selektovaniRacun.Id)
                    {
                        
                        n.Obrisan = true;
                        view.Refresh();
                        break;
                    }
                }
            }
            GenericSerializer.Serialize("Racuni.xml", Projekat.Instance.Racuni);
        }

        private void DodajKorisnika()
        {
            var noviKorisnik = new Korisnik()
            {
                Ime = "",
                Prezime = "",
                KorisnickoIme = "",
                Lozinka = "",
                TipKorisnika = TipKorisnika.Prodavac
            };
            var korisniciProzor = new KorisniciUI(noviKorisnik, KorisniciUI.Operacija.DODAVANJE);
            korisniciProzor.ShowDialog();
        }

        private void IzmeniKorisnika()
        {
            var selektovaniKorisnik = (Korisnik)dgNamestaj.SelectedItem;
            var korisniciProzor = new KorisniciUI(selektovaniKorisnik, KorisniciUI.Operacija.IZMENA);
            korisniciProzor.ShowDialog();
        }

        private void ObrisiKorisnika()
        {
            var selektovaniKorisnik = (Korisnik)dgNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {selektovaniKorisnik.KorisnickoIme}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var n in Projekat.Instance.Korisnici)
                {
                    if (n.Id == selektovaniKorisnik.Id)
                    {
                        
                        n.Obrisan = true;
                        view.Refresh();
                        break;
                    }
                }
            }
            GenericSerializer.Serialize("Korisnici.xml", Projekat.Instance.Korisnici);
        }

        

        

        private void DodajNamestaj()
        {
            var noviNamestaj = new Namestaj()
            {
                Naziv = ""
            };
            var namestajProzor = new NamestajWindow(noviNamestaj, NamestajWindow.Operacija.DODAVANJE);
            namestajProzor.ShowDialog();

        }

        private void IzmeniNamestaj()
        {
            var selektovaniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            var namestajProzor = new NamestajWindow(selektovaniNamestaj, NamestajWindow.Operacija.IZMENA);
            namestajProzor.ShowDialog();

        }

        private void ObrisiNamestaj()
        {
            var selektovaniNamestaj = (Namestaj)dgNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {selektovaniNamestaj.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var n in Projekat.Instance.sviNamestaji)
                {
                    if (n.Id == selektovaniNamestaj.Id)
                    {
                        
                        n.Obrisan = true;
                        view.Refresh();
                        break;
                    }
                }
            }
            GenericSerializer.Serialize("namestaj.xml", Projekat.Instance.sviNamestaji);

        }



        private void ObrisiTipNamestaja()
        {
            var selektovaniTipNamestaja = (TipNamestaja)dgNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {selektovaniTipNamestaja.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var n in Projekat.Instance.TipoviNamestaja)
                {
                    if (n.Id == selektovaniTipNamestaja.Id)
                    {
                        
                        n.Obrisan = true;
                        view.Refresh();
                        break;
                    }
                }
            }
            GenericSerializer.Serialize("tipoviNamestaja.xml", Projekat.Instance.TipoviNamestaja);

        }



        private void IzmeniTipNamestaja()
        {
            var selektovaniTipNamestaja = (TipNamestaja)dgNamestaj.SelectedItem;
            var tipNamestajaProzor = new TipNamestajaUIWindow(selektovaniTipNamestaja, TipNamestajaUIWindow.Operacija.IZMENA);
            tipNamestajaProzor.ShowDialog();

        }

        private void DodajTipNamestaja()
        {
            var noviTipNamestaja = new TipNamestaja()
            {
                Naziv = ""
            };
            var tipNamestajaProzor = new TipNamestajaUIWindow(noviTipNamestaja, TipNamestajaUIWindow.Operacija.DODAVANJE);
            tipNamestajaProzor.ShowDialog();
        }

        private void ObrisiAkciju()
        {
            var selektovanaAkcija = (AkcijskaProdaja)dgNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {selektovanaAkcija.Id}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var n in Projekat.Instance.AkcijskeProdaje)
                {
                    if (n.Id == selektovanaAkcija.Id)
                    {
                        
                        n.Obrisan = true;
                        view.Refresh();
                        break;
                    }
                }
            }
            GenericSerializer.Serialize("Akcije.xml", Projekat.Instance.AkcijskeProdaje);

        }

        private void IzmeniAkciju()
        {
            var selektovanaAkcija = (AkcijskaProdaja)dgNamestaj.SelectedItem;
            var akcijeProzor = new AkcijskaProdajaUIWindow(selektovanaAkcija, AkcijskaProdajaUIWindow.Operacija.IZMENA);
            akcijeProzor.ShowDialog();
        }

        private void DodajAkciju()
        {
            var novaAkcija = new AkcijskaProdaja()
            {
                Popust = 0,
                DatumPocetka = DateTime.Today,
                DatumZavresetka = DateTime.Today
            };
            var akcijeProzor = new AkcijskaProdajaUIWindow(novaAkcija, AkcijskaProdajaUIWindow.Operacija.DODAVANJE);
            akcijeProzor.ShowDialog();
        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
        }

        private void TipoviNamestajaPrelaz(object sender, RoutedEventArgs e)
        {
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.TipoviNamestaja);
            view.Filter = TipNamestajaFilter;
            podaci = Podaci.TipNamestaja;
            dgNamestaj.ItemsSource = Projekat.Instance.TipoviNamestaja;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;

            bool TipNamestajaFilter(object obj)
            {
                return ((TipNamestaja)obj).Obrisan == false;
            }

        }


        private void AkcijskeProdajePrelaz(object sender, RoutedEventArgs e)
        {
            
            podaci = Podaci.AkcijskeProdaje;
            dgNamestaj.ItemsSource = Projekat.Instance.AkcijskeProdaje;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.AkcijskeProdaje);
            view.Filter = AkcijeFilter;
            bool AkcijeFilter(object obj)
            {
                return ((AkcijskaProdaja)obj).Obrisan == false;
            }

        }

        private void NamestajPrelaz(object sender, RoutedEventArgs e)
        {
            podaci = Podaci.Namestaj;
            dgNamestaj.ItemsSource = Projekat.Instance.sviNamestaji;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;


        }

        private void DodatneUslugePrelaz(object sender, RoutedEventArgs e)
        {
            podaci = Podaci.DodatneUsluge;
            dgNamestaj.ItemsSource = Projekat.Instance.DodatneUsluge;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatneUsluge);
            view.Filter = uslugeFilter;
            bool uslugeFilter(object obj)
            {
                return ((DodatnaUsluga)obj).Obrisan == false;
            }
        }
        private void KorisniciPrelaz(object sender, RoutedEventArgs e)
        {
            podaci = Podaci.Korisnici;
            dgNamestaj.ItemsSource = Projekat.Instance.Korisnici;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Korisnici);
            view.Filter = KorisniciFilter;
            bool KorisniciFilter(object obj)
            {
                return ((Korisnik)obj).Obrisan == false;
            }
        }
        private void RacuniPrelaz(object sender, RoutedEventArgs e)
        {
            podaci = Podaci.RacunProdaje;
            dgNamestaj.ItemsSource = Projekat.Instance.Racuni;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Racuni);
            view.Filter = RacuniFilter;
            bool RacuniFilter(object obj)
            {
                return ((Racun)obj).Obrisan == false;
            }
        }

        private void SalonPrelaz(object sender, RoutedEventArgs e)
        {
            podaci = Podaci.Salon;
            dgNamestaj.ItemsSource = Projekat.Instance.Saloni;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Saloni);
            view.Filter = saloniFilter;
            bool saloniFilter(object obj)
            {
                return ((Salon)obj).Obrisan == false;
            }
        }
    }
}
