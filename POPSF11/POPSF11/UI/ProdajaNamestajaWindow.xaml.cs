using POP_SF_11_GUI.Model;
using POP_SF_11_GUI.Model.util;
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
using System.Windows.Shapes;

namespace POP_SF_11_GUI.UI
{
    /// <summary>
    /// Interaction logic for ProdajaNamestajaWindow.xaml
    /// </summary>
    public partial class ProdajaNamestajaWindow : Window
    {
        private ICollectionView viewNamestaj;
        private ICollectionView viewDodatneUsluge;
        private Racun racun;
        private Operacija operacija;
        private List<StavkaProdajeNamestaj> korpaNamestaj = new List<StavkaProdajeNamestaj>();
        private List<StavkaProdajeDodatnaUsluga> korpaDodatneUsluge = new List<StavkaProdajeDodatnaUsluga>();

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public ProdajaNamestajaWindow(Racun racun, Operacija operacija)
        {
            InitializeComponent();

            this.racun = racun;
            this.operacija = operacija;
            foreach (var spNamestaj in Projekat.Instance.SPNamestaj)
            {
                if (racun.Id == spNamestaj.RacunId)
                    korpaNamestaj.Add(spNamestaj);
            }
            foreach (var spDodatnaUsluga in Projekat.Instance.SPDodatneUsluge)
            {
                if (racun.Id == spDodatnaUsluga.RacunId)
                    korpaDodatneUsluge.Add(spDodatnaUsluga);
            }

            tbKupac.DataContext = racun;
            tbBrojRacuna.DataContext = racun;
            tbUkupnaCena.DataContext = racun;

            switch (operacija)
            {
                case Operacija.DODAVANJE:

                    viewNamestaj = CollectionViewSource.GetDefaultView(korpaNamestaj);
                    // viewNamestaj.Filter = namestajFilter;
                    dgNamestajKorpa.ItemsSource = viewNamestaj;
                    dgNamestajKorpa.IsSynchronizedWithCurrentItem = true;
                    dgNamestajKorpa.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

                    dgNamestajSalon.ItemsSource = Model.Namestaj.GetAll();
                    dgNamestajSalon.IsSynchronizedWithCurrentItem = true;
                    dgNamestajSalon.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


                    dgUslugeSalon.ItemsSource = DodatnaUsluga.GetAll();
                    dgUslugeSalon.IsSynchronizedWithCurrentItem = true;
                    dgUslugeSalon.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

                    dgIzabraneUsluge.ItemsSource = viewDodatneUsluge;
                    dgIzabraneUsluge.IsSynchronizedWithCurrentItem = true;
                    dgIzabraneUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case Operacija.IZMENA:

                    viewNamestaj = CollectionViewSource.GetDefaultView(korpaNamestaj);
                    // viewNamestaj.Filter = namestajFilter;
                    dgNamestajKorpa.ItemsSource = viewNamestaj;
                    dgNamestajKorpa.IsSynchronizedWithCurrentItem = true;
                    dgNamestajKorpa.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

                    dgNamestajSalon.ItemsSource = Model.Namestaj.GetAll();
                    dgNamestajSalon.IsSynchronizedWithCurrentItem = true;
                    dgNamestajSalon.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


                    dgUslugeSalon.ItemsSource = DodatnaUsluga.GetAll();
                    dgUslugeSalon.IsSynchronizedWithCurrentItem = true;
                    dgUslugeSalon.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

                    dgIzabraneUsluge.ItemsSource = viewDodatneUsluge;
                    dgIzabraneUsluge.IsSynchronizedWithCurrentItem = true;
                    dgIzabraneUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
            }

        }


        private void Izlaz(object sender, RoutedEventArgs e)
        {
            foreach (var spNamestaj in Projekat.Instance.SPNamestaj)
            {
                if(spNamestaj.RacunId == racun.Id)
                {
                    StavkaProdajeNamestaj.Delete(spNamestaj);
                }
            }
            foreach (var spDodatnaUsluga in Projekat.Instance.SPDodatneUsluge)
            {
                if (spDodatnaUsluga.RacunId == racun.Id)
                {
                    StavkaProdajeDodatnaUsluga.Delete(spDodatnaUsluga);
                }
            }
            this.Close();
        }
        private void SacuvajRacun(object sender, RoutedEventArgs e)
        {
            var postojeciRacun = Projekat.Instance.Racuni;
            switch (operacija)
            {

                case Operacija.DODAVANJE:
                    racun.Id = postojeciRacun.Count + 1;
                    racun.Kupac = tbKupac.Text;
                    racun.DatumProdaje = DateTime.Now;
                    racun.BrojRacuna = tbBrojRacuna.Text;

                    IzracunajCenu();
                    racun.UkupnaCena = IzracunajCenu() * (1+racun.PDV);
                    Racun.Create(racun);
                    postojeciRacun.Add(racun);

                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciRacun)
                    {
                        if (n.Id == racun.Id)
                        {
                            n.Kupac = tbKupac.Text;
                            n.BrojRacuna = tbBrojRacuna.Text;
                            

                            IzracunajCenu();
                            n.UkupnaCena = IzracunajCenu() * (1 + racun.PDV); 

                        }
                    }
                    break;
                    
            }
           // GenericSerializer.Serialize("Racuni.xml", postojeciRacun);
            this.Close();
        }

        private double IzracunajCenu()
        {
            foreach (var stavkaNamestaj in Projekat.Instance.SPNamestaj)
            {
                if (racun.Id == stavkaNamestaj.RacunId)
                {
                    Namestaj noviNamestaj = Namestaj.GetById(stavkaNamestaj.NamestajId);
                    for (int i = 1; i < stavkaNamestaj.Kolicina; i++)
                    {
                        if (noviNamestaj.AkcijskaProdaja == null)
                        {
                            racun.UkupnaCena += noviNamestaj.Cena;
                        }
                        else
                        {
                            racun.UkupnaCena += noviNamestaj.Cena - (noviNamestaj.Cena * (1 + noviNamestaj.AkcijskaProdaja.Popust));

                        }
                    }
                }
            }
            foreach (var stavkaDodatnaUsluga in Projekat.Instance.SPDodatneUsluge)
            {
                if (racun.Id == stavkaDodatnaUsluga.RacunId)
                {
                    DodatnaUsluga novaDU = DodatnaUsluga.GetById(stavkaDodatnaUsluga.DodatnaUslugaId);
                    racun.UkupnaCena += novaDU.Cena;
                }
            }
            return racun.UkupnaCena;
        }

        private void dgNamestajSalon_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id")
            {
                e.Cancel = true;

            }
        }

        private void dgNamestajKorpa_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "RacunId" || (string)e.Column.Header == "NamestajId")
            {
                e.Cancel = true;
            }
        }

        private void dgUslugeSalon_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id")
            {
                e.Cancel = true;

            }
        }

        private void dgIzabraneUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "RacunId" || (string)e.Column.Header == "NamestajId")
            {
                e.Cancel = true;
            }
        }

        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var namestaj = new Namestaj();
           // NamestajStavkaProdajeWindow nspw = new NamestajStavkaProdajeWindow(NamestajStavkaProdajeWindow.Operacija.DODAVANJE,namestaj);
        }

        private void btnIzbaciNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var SPNamestaj = (StavkaProdajeNamestaj)dgNamestajKorpa.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbacite izabrani namestaj iz korpe: {Namestaj.GetById(SPNamestaj.NamestajId).Naziv}?", "Poruka o brisanju ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var n in Projekat.Instance.SPNamestaj)
                {
                    if (n.Id == SPNamestaj.Id)
                    {
                        StavkaProdajeNamestaj.Delete(n);
                    }
                }
            }
        }

        private void btnDodajUslugu_Click(object sender, RoutedEventArgs e)
        {
            var dodatnaUsluga = new DodatnaUsluga();
            //DodatnaUslugaStavkaProdajeWindow duspw = new DodatnaUslugaStavkaProdajeWindow(DodatnaUslugaStavkaProdajeWindow.Operacija.Dodavanje, dodatnaUsluga);
        }

        private void btnIzbaciUslugu_Click(object sender, RoutedEventArgs e)
        {
            var SPDUsluga = (StavkaProdajeDodatnaUsluga)dgIzabraneUsluge.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbacite ovu uslugu: {DodatnaUsluga.GetById(SPDUsluga.DodatnaUslugaId).Naziv}?", "Poruka o brisanju ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var du in Projekat.Instance.SPDodatneUsluge)
                {
                    if (du.Id == SPDUsluga.Id)
                    {
                        StavkaProdajeDodatnaUsluga.Delete(du);
                    }
                }
            }
        }

        private void btnIzracunaj_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

