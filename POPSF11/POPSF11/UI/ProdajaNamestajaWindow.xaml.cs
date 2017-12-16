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
    /// Interaction logic for ProdajaNamestajaWindow.xaml
    /// </summary>
    public partial class ProdajaNamestajaWindow : Window
    {
        private Racun racun;
        private Operacija operacija;
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
            cbNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            cbDodatneUsluge.ItemsSource = Projekat.Instance.DodatneUsluge;

            tbKolicina.DataContext = racun;
            tbKupac.DataContext = racun;
            cbNamestaj.DataContext = racun;
            tbBrojRacuna.DataContext = racun;
            cbDodatneUsluge.DataContext = racun;
            tbUkupnaCena.DataContext = racun;

        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajRacun(object sender, RoutedEventArgs e)
        {
            var postojeciRacun = Projekat.Instance.Racuni;
            switch (operacija)
            {

                case Operacija.DODAVANJE:
                    racun.Id = postojeciRacun.Count + 1;
                    racun.Kolicina = int.Parse(tbKolicina.Text);
                    racun.Kupac = tbKupac.Text;
                    racun.DatumProdaje = DateTime.Now;
                    racun.BrojRacuna = tbBrojRacuna.Text;
                    racun.Namestaj = ((Namestaj)cbNamestaj.SelectedItem);
                    racun.DodatneUsluge = ((DodatnaUsluga)cbDodatneUsluge.SelectedItem);
                    racun.UkupnaCena = double.Parse(tbUkupnaCena.Text);

                    postojeciRacun.Add(racun);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciRacun)
                    {
                        if (n.Id == racun.Id)
                        {

                            n.Kolicina = int.Parse(tbKolicina.Text);
                            n.Kupac = tbKupac.Text;
                            n.Namestaj = ((Namestaj)cbNamestaj.SelectedItem);
                            n.BrojRacuna = tbBrojRacuna.Text;
                            n.DodatneUsluge = ((DodatnaUsluga)cbDodatneUsluge.SelectedItem);
                            racun.DatumProdaje = n.DatumProdaje;
                            racun.UkupnaCena = double.Parse(tbUkupnaCena.Text);



                        }
                    }
                    break;
                    
            }
            GenericSerializer.Serialize("Racuni.xml", postojeciRacun);
            this.Close();
        }

        private void Izracunaj(object sender, RoutedEventArgs e)
        {
            tbUkupnaCena.Text = ((int.Parse(tbKolicina.Text) * ((Namestaj)cbNamestaj.SelectedItem).Cena)+((DodatnaUsluga)cbDodatneUsluge.SelectedItem).Cena
                + (int.Parse(tbKolicina.Text) * ((Namestaj)cbNamestaj.SelectedItem).Cena) + ((DodatnaUsluga)cbDodatneUsluge.SelectedItem).Cena  * Racun.PDV).ToString();
        }
    }
}

