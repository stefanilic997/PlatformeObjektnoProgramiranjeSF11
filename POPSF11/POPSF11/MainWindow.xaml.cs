
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

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
            savNamestaj,
            TipNamestaja,
            AkcijskeProdaje,
            Korisnici,
            DodatneUsluge,
            RacunProdaje,
            Saloni
        }
        public MainWindow()
        {

            InitializeComponent();

            cbSortiranje.Items.Clear();
            cbOrderHack.Items.Add("ASC");
            cbOrderHack.Items.Add("DESC");

            podaci = Podaci.savNamestaj;
            dgNamestaj.ItemsSource = Model.Namestaj.GetAll();
            dgNamestaj.IsSynchronizedWithCurrentItem = true;

        }


        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Dodaj(object sender, RoutedEventArgs e)
        {

            switch (podaci)
            {
                case Podaci.savNamestaj:
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
                case Podaci.Saloni:
                    DodajSalon();
                    break;
            }
        }



        private void Izmeni(object sender, RoutedEventArgs e)
        {

            switch (podaci)
            {
                case Podaci.savNamestaj:
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
                case Podaci.Saloni:
                    IzmeniSalon();
                    break;
            }
        }



        private void Obrisi(object sender, RoutedEventArgs e)
        {

            switch (podaci)
            {
                case Podaci.savNamestaj:
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
                case Podaci.Saloni:
                    ObrisiSalon();
                    break;
            }
        }

        private void Pretrazi(object sender, RoutedEventArgs e)
        {
            switch (podaci)
            {
                case Podaci.savNamestaj:
                    PretraziNamestaj();
                    break;
                case Podaci.TipNamestaja:
                    PretraziTipNamestaja();
                    break;
                case Podaci.AkcijskeProdaje:
                    PretraziAkciju();
                    break;
                case Podaci.Korisnici:
                    PretraziKorisnika();
                    break;
                case Podaci.DodatneUsluge:
                    PretraziDodatnuUslugu();
                    break;
                case Podaci.RacunProdaje:
                    PretraziRacunProdaje();
                    break;
                case Podaci.Saloni:
                    PretraziSalon();
                    break;
            }
        }

        private void Sortiraj(object sender, RoutedEventArgs e)
        {

            switch (podaci)
            {
                case Podaci.savNamestaj:
                    SortirajNamestaj();
                    break;
                case Podaci.TipNamestaja:
                    SortirajTipNamestaja();
                    break;
                case Podaci.AkcijskeProdaje:
                    SortirajAkciju();
                    break;
                case Podaci.Korisnici:
                    SortirajKorisnika();
                    break;
                case Podaci.DodatneUsluge:
                    SortirajDodatnuUslugu();
                    break;
                case Podaci.RacunProdaje:
                    SortirajRacunProdaje();
                    break;
                case Podaci.Saloni:
                    SortirajSalon();
                    break;
            }

        }

        private void SortirajSalon()
        {
            string orderHack = cbOrderHack.SelectedValue.ToString();
            view = CollectionViewSource.GetDefaultView(Salon.Sort(cbSortiranje.SelectedItem.ToString(), orderHack));
            dgNamestaj.ItemsSource = Salon.Sort((cbSortiranje.SelectedItem.ToString()), orderHack);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;


        }

        private void SortirajRacunProdaje()
        {
            string orderHack = cbOrderHack.SelectedValue.ToString();
            view = CollectionViewSource.GetDefaultView(Racun.Sort(cbSortiranje.SelectedItem.ToString(), orderHack));
            dgNamestaj.ItemsSource = Racun.Sort((cbSortiranje.SelectedItem.ToString()), orderHack);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void SortirajDodatnuUslugu()
        {
            string orderHack = cbOrderHack.SelectedValue.ToString();
            view = CollectionViewSource.GetDefaultView(DodatnaUsluga.Sort(cbSortiranje.SelectedItem.ToString(), orderHack));
            dgNamestaj.ItemsSource = DodatnaUsluga.Sort((cbSortiranje.SelectedItem.ToString()), orderHack);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void SortirajKorisnika()
        {
            string orderHack = cbOrderHack.SelectedValue.ToString();
            view = CollectionViewSource.GetDefaultView(Korisnik.Sort(cbSortiranje.SelectedItem.ToString(), orderHack));
            dgNamestaj.ItemsSource = Korisnik.Sort((cbSortiranje.SelectedItem.ToString()), orderHack);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void SortirajAkciju()
        {

            string orderHack = cbOrderHack.SelectedValue.ToString();
            view = CollectionViewSource.GetDefaultView(AkcijskaProdaja.Sort(cbSortiranje.SelectedItem.ToString(), orderHack));
            dgNamestaj.ItemsSource = AkcijskaProdaja.Sort((cbSortiranje.SelectedItem.ToString()), orderHack);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void SortirajTipNamestaja()
        {

            string orderHack = cbOrderHack.SelectedValue.ToString();
            view = CollectionViewSource.GetDefaultView(TipNamestaja.Sort(cbSortiranje.SelectedItem.ToString(), orderHack));
            dgNamestaj.ItemsSource = TipNamestaja.Sort((cbSortiranje.SelectedItem.ToString()), orderHack);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;

        }

        private void SortirajNamestaj()
        {
            string orderHack = cbOrderHack.SelectedValue.ToString();
            view = CollectionViewSource.GetDefaultView(Model.Namestaj.Sort(cbSortiranje.SelectedItem.ToString(), orderHack));
            dgNamestaj.ItemsSource = Model.Namestaj.Sort((cbSortiranje.SelectedItem.ToString()), orderHack);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void PretraziSalon()
        {
            string searchQuery = tbPretrazi.Text;
            view = CollectionViewSource.GetDefaultView(Salon.Pretrazi(cbSortiranje.SelectedItem.ToString(), searchQuery));
            dgNamestaj.ItemsSource = Salon.Pretrazi((cbSortiranje.SelectedItem.ToString()), searchQuery);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void PretraziRacunProdaje()
        {
            string searchQuery = tbPretrazi.Text;
            view = CollectionViewSource.GetDefaultView(Racun.Pretrazi(cbSortiranje.SelectedItem.ToString(), searchQuery));
            dgNamestaj.ItemsSource = Racun.Pretrazi((cbSortiranje.SelectedItem.ToString()), searchQuery);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void PretraziDodatnuUslugu()
        {
            string searchQuery = tbPretrazi.Text;
            view = CollectionViewSource.GetDefaultView(DodatnaUsluga.Pretrazi(cbSortiranje.SelectedItem.ToString(), searchQuery));
            dgNamestaj.ItemsSource = DodatnaUsluga.Pretrazi((cbSortiranje.SelectedItem.ToString()), searchQuery);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void PretraziKorisnika()
        {
            string searchQuery = tbPretrazi.Text;
            view = CollectionViewSource.GetDefaultView(Korisnik.Pretrazi(cbSortiranje.SelectedItem.ToString(), searchQuery));
            dgNamestaj.ItemsSource = Korisnik.Pretrazi((cbSortiranje.SelectedItem.ToString()), searchQuery);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void PretraziAkciju()
        {
            string searchQuery = tbPretrazi.Text;
            view = CollectionViewSource.GetDefaultView(AkcijskaProdaja.Pretrazi(cbSortiranje.SelectedItem.ToString(), searchQuery));
            dgNamestaj.ItemsSource = AkcijskaProdaja.Pretrazi((cbSortiranje.SelectedItem.ToString()), searchQuery);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void PretraziTipNamestaja()
        {
            string searchQuery = tbPretrazi.Text;
            view = CollectionViewSource.GetDefaultView(TipNamestaja.Pretrazi(cbSortiranje.SelectedItem.ToString(), searchQuery));
            dgNamestaj.ItemsSource = TipNamestaja.Pretrazi((cbSortiranje.SelectedItem.ToString()), searchQuery);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
        }

        private void PretraziNamestaj()
        {
            string searchQuery = tbPretrazi.Text;
            view = CollectionViewSource.GetDefaultView(Model.Namestaj.Pretrazi(cbSortiranje.SelectedItem.ToString(), searchQuery));
            dgNamestaj.ItemsSource = Model.Namestaj.Pretrazi((cbSortiranje.SelectedItem.ToString()), searchQuery);
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
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
                        Salon.Delete(n);
                        view.Refresh();
                        break;
                    }
                }
            }
            //GenericSerializer.Serialize("DodatneUsluge.xml", Projekat.Instance.DodatneUsluge);
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
                        DodatnaUsluga.Delete(n);
                        view.Refresh();
                        break;
                    }
                }
            }
            //  GenericSerializer.Serialize("DodatneUsluge.xml", Projekat.Instance.DodatneUsluge);
        }

        private void DodajRacunProdaje()
        {
            var noviRacun = new Racun()
            {
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
            // GenericSerializer.Serialize("Racuni.xml", Projekat.Instance.Racuni);
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
                        Korisnik.Delete(n);
                        view.Refresh();
                        break;
                    }
                }
            }
            //GenericSerializer.Serialize("Korisnici.xml", Projekat.Instance.Korisnici);
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
                        Model.Namestaj.Delete(n);
                        view.Refresh();
                        break;
                    }
                }
            }
            // GenericSerializer.Serialize("namestaj.xml", Projekat.Instance.sviNamestaji);

        }



        private void ObrisiTipNamestaja()
        {
            var selektovaniTipNamestaja = (TipNamestaja)dgNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {selektovaniTipNamestaja.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                TipNamestaja.Delete(selektovaniTipNamestaja);
                Projekat.Instance.TipoviNamestaja.Remove(selektovaniTipNamestaja);
                view.Refresh();
            }

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
                        AkcijskaProdaja.Delete(n);
                        view.Refresh();
                        break;
                    }
                }
            }
            //  GenericSerializer.Serialize("Akcije.xml", Projekat.Instance.AkcijskeProdaje);

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

            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id"
                || (string)e.Column.Header == "AkcijaId" || (string)e.Column.Header == "TipNamestajaId")
            {
                e.Cancel = true;

            }
            else
            {
                foreach (var i in dgNamestaj.Columns.Count.ToString())
                {

                    cbSortiranje.Items.Add(e.Column.Header);
                }
            }
            cbSortiranje.SelectedIndex = 0;
        }

        private void TipoviNamestajaPrelaz(object sender, RoutedEventArgs e)
        {
            cbSortiranje.Items.Clear();
            view = CollectionViewSource.GetDefaultView(TipNamestaja.GetAll());

            podaci = Podaci.TipNamestaja;
            dgNamestaj.ItemsSource = TipNamestaja.GetAll();
            dgNamestaj.IsSynchronizedWithCurrentItem = true;



        }


        private void AkcijskeProdajePrelaz(object sender, RoutedEventArgs e)
        {
            cbSortiranje.Items.Clear();
            view = CollectionViewSource.GetDefaultView(AkcijskaProdaja.GetAll());

            podaci = Podaci.AkcijskeProdaje;

            dgNamestaj.ItemsSource = AkcijskaProdaja.GetAll();
            dgNamestaj.IsSynchronizedWithCurrentItem = true;




        }

        private void NamestajPrelaz(object sender, RoutedEventArgs e)
        {
            cbSortiranje.Items.Clear();

            podaci = Podaci.savNamestaj;
            dgNamestaj.ItemsSource = Model.Namestaj.GetAll();
            dgNamestaj.IsSynchronizedWithCurrentItem = true;


        }

        private void DodatneUslugePrelaz(object sender, RoutedEventArgs e)
        {
            cbSortiranje.Items.Clear();

            podaci = Podaci.DodatneUsluge;
            dgNamestaj.ItemsSource = DodatnaUsluga.GetAll();
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            view = CollectionViewSource.GetDefaultView(DodatnaUsluga.GetAll());


        }
        private void KorisniciPrelaz(object sender, RoutedEventArgs e)
        {
            cbSortiranje.Items.Clear();

            podaci = Podaci.Korisnici;
            dgNamestaj.ItemsSource = Korisnik.GetAll();
            dgNamestaj.IsSynchronizedWithCurrentItem = true;

            view = CollectionViewSource.GetDefaultView(Korisnik.GetAll());


        }
        private void RacuniPrelaz(object sender, RoutedEventArgs e)
        {
            cbSortiranje.Items.Clear();
            podaci = Podaci.RacunProdaje;
            //ZAVRSI RACUN SQL
            dgNamestaj.ItemsSource = Projekat.Instance.Racuni;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Racuni);


        }

        private void SalonPrelaz(object sender, RoutedEventArgs e)
        {
            cbSortiranje.Items.Clear();

            podaci = Podaci.Saloni;
            dgNamestaj.ItemsSource = Salon.GetAll();
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            view = CollectionViewSource.GetDefaultView(Salon.GetAll());


        }

        /*
        private void dgNamestaj_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            // iteratively traverse the visual tree
            while ((dep != null) &&
                    !(dep is DataGridCell) &&
                    !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            if (dep is DataGridColumnHeader)
            {
                DataGridColumnHeader columnHeader = dep as DataGridColumnHeader;
                // do something
               MessageBox.Show(columnHeader.Column.Header.ToString());
                MessageBox.Show(columnHeader.Column.DisplayIndex.ToString());
                //NE MOZE OVAKO NESTO RADI ALI JE VEROVATNO DEFAULTNO SORTIRANJE
                //view = CollectionViewSource.GetDefaultView(TipNamestaja.Sort(columnHeader.Column.Header.ToString()));
                //view.Refresh();

            }

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;
                // do something
            }
        }
        */

    }

}