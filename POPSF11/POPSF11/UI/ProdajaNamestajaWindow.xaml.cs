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
                    viewDodatneUsluge = CollectionViewSource.GetDefaultView(korpaDodatneUsluge);
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
                    viewDodatneUsluge = CollectionViewSource.GetDefaultView(korpaDodatneUsluge);

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
            foreach (var spNamestaj in korpaNamestaj)
            {
                if(spNamestaj.RacunId == racun.Id)
                {
                    StavkaProdajeNamestaj.Delete(spNamestaj);
                }
            }
            foreach (var spDodatnaUsluga in korpaDodatneUsluge)
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
            var postojeciRacun = Racun.GetAll();
            switch (operacija)
            {

                case Operacija.DODAVANJE:
                    racun.Id = postojeciRacun.Count + 1;
                    racun.Kupac = tbKupac.Text;
                    racun.DatumProdaje = DateTime.Now;
                    racun.BrojRacuna = tbBrojRacuna.Text;

                    IzracunajCenu();
                    racun.UkupnaCena = double.Parse(tbUkupnaCena.Text);
                    postojeciRacun.Add(racun);
                    Racun.Create(racun);
                    

                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciRacun)
                    {
                        if (n.Id == racun.Id)
                        {
                            n.Kupac = tbKupac.Text;
                            n.BrojRacuna = tbBrojRacuna.Text;
                            n.DatumProdaje = DateTime.Now;

                            IzracunajCenu();
                            n.UkupnaCena = double.Parse(tbUkupnaCena.Text);
                            Racun.Update(n);
                        }
                    }
                    
                    break;
                    
            }
           // GenericSerializer.Serialize("Racuni.xml", postojeciRacun);
            this.Close();
        }

        private double IzracunajCenu()
        {
            racun.UkupnaCena = 0;
            foreach (var stavkaNamestaj in korpaNamestaj)
            {
                Namestaj namestajRef = Namestaj.GetById(stavkaNamestaj.NamestajId);
                double cenaNamestaja = namestajRef.Cena * stavkaNamestaj.Kolicina;
                if (namestajRef.AkcijskaProdaja == null)
                {
                    continue;
                }
                else
                {
                    racun.UkupnaCena += namestajRef.Cena * (1 + namestajRef.AkcijskaProdaja.Popust / 100);
                }

                racun.UkupnaCena += cenaNamestaja;
            }
            foreach (var stavkaDodatnaUsluga in korpaDodatneUsluge)
            {
                DodatnaUsluga DUref = DodatnaUsluga.GetById(stavkaDodatnaUsluga.DodatnaUslugaId);
                racun.UkupnaCena += DUref.Cena;
            }
            racun.UkupnaCena = racun.UkupnaCena * (1 + racun.PDV);
            return racun.UkupnaCena;
        }

        private void dgNamestajSalon_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "TipNamestajaId" || (string)e.Column.Header == "AkcijskaProdaja" || (string)e.Column.Header == "AkcijaId")
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
            var izabraniNamestaj = (Namestaj)dgNamestajSalon.SelectedItem;
            var stavkaNamestaj = new StavkaProdajeNamestaj()
            {
                Id = Projekat.Instance.SPNamestaj.Count() + 1,
                Naziv = izabraniNamestaj.Naziv,
                RacunId = racun.Id,
                NamestajId = izabraniNamestaj.Id,
                Kolicina = int.Parse(tbKolicina.Text)
            };
            StavkaProdajeNamestaj.Create(stavkaNamestaj);
            korpaNamestaj.Add(stavkaNamestaj);
            dgNamestajKorpaRefresh();
            
        }

        private void btnIzbaciNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var SPNamestaj = (StavkaProdajeNamestaj)dgNamestajKorpa.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbacite izabrani namestaj iz korpe: {SPNamestaj.Naziv}?", "Poruka o brisanju ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var n in korpaNamestaj)
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
            var izabranaUsluga = (DodatnaUsluga)dgUslugeSalon.SelectedItem;
            var stavkaDodatnaUsluga = new StavkaProdajeDodatnaUsluga
            {
                Id = Projekat.Instance.SPDodatneUsluge.Count() + 1,
                Naziv = izabranaUsluga.Naziv,
                DodatnaUslugaId = izabranaUsluga.Id,
                RacunId = racun.Id
            };
            StavkaProdajeDodatnaUsluga.Create(stavkaDodatnaUsluga);
            korpaDodatneUsluge.Add(stavkaDodatnaUsluga);
            dgIzabraneUslugeRefresh();
            
        }

        private void btnIzbaciUslugu_Click(object sender, RoutedEventArgs e)
        {
            var SPDUsluga = (StavkaProdajeDodatnaUsluga)dgIzabraneUsluge.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbacite ovu uslugu: {DodatnaUsluga.GetById(SPDUsluga.DodatnaUslugaId).Naziv}?", "Poruka o brisanju ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var du in korpaDodatneUsluge)
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
            IzracunajCenu();
        }
        private void dgNamestajKorpaRefresh()
        {
            viewNamestaj = CollectionViewSource.GetDefaultView(StavkaProdajeNamestaj.GetAllbyRacunId(racun.Id));
            dgNamestajKorpa.IsSynchronizedWithCurrentItem = true;
            dgNamestajKorpa.ItemsSource = StavkaProdajeNamestaj.GetAllbyRacunId(racun.Id);
        }
        private void dgIzabraneUslugeRefresh()
        {
            viewDodatneUsluge = CollectionViewSource.GetDefaultView(StavkaProdajeDodatnaUsluga.GetAllbyRacunId(racun.Id));
            dgIzabraneUsluge.ItemsSource = StavkaProdajeDodatnaUsluga.GetAllbyRacunId(racun.Id);
            dgIzabraneUsluge.IsSynchronizedWithCurrentItem = true;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            dgNamestajKorpaRefresh();
            dgIzabraneUslugeRefresh();
        }
    }
}