using POP_SF_11_GUI.Model;
using POP_SF_11_GUI.Model.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {
        private Namestaj namestaj;
        private Operacija operacija;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public NamestajWindow(Namestaj namestaj, Operacija operacija)
        {
            InitializeComponent();

            this.namestaj = namestaj;
            this.operacija = operacija;
            cbTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja;

            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            cbTipNamestaja.DataContext = namestaj;

        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajNamestaj(object sender, RoutedEventArgs e)
        {
            var postojeciNamestaj = Projekat.Instance.Namestaj;
            switch (operacija)
            {
                
                case Operacija.DODAVANJE:
                    namestaj.Id = postojeciNamestaj.Count + 1;
                    namestaj.Naziv = tbNaziv.Text;
                    namestaj.Cena = int.Parse(tbCena.Text);
                    namestaj.TipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).Id;

                    postojeciNamestaj.Add(namestaj);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciNamestaj)
                    {
                        if (n.Id == namestaj.Id)
                        {

                            n.Naziv = tbNaziv.Text;
                            n.Cena = int.Parse(tbCena.Text);
                            n.TipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).Id;

                        }
                    }
                    break;
              // ISTO KAO I FOR EACH IZNAD ^^^^
              //  Projekat.Instance.Namestaj - postojeciNamestaja.singleordefault( x=> x.Id == namestaj.Id)
                    
            }
            GenericSerializer.Serialize("namestaj.xml", postojeciNamestaj);
            this.Close();
        }
    }
}
