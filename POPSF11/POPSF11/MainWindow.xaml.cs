using POP_SF_11_GUI.Model;
using POP_SF_11_GUI.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF_11_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OsveziPrikaz();
            listBoxNamestaj.SelectedIndex = 0;
        }
        private void OsveziPrikaz()
        {
            listBoxNamestaj.Items.Clear();
            foreach (var namestaj in Projekat.Instance.Namestaj)
            {
                if(namestaj.Obrisan == false)
                {
                    listBoxNamestaj.Items.Add(namestaj);
                }
                
            }
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void DodajNamestaj(object sender, RoutedEventArgs e)
        {
            var noviNamestaj = new Namestaj()
            {
                Naziv = ""
            };
            var namestajProzor = new NamestajWindow(noviNamestaj,NamestajWindow.Operacija.DODAVANJE);
            namestajProzor.ShowDialog();

            OsveziPrikaz();
        }

        private void IzmeniNamestaj(object sender, RoutedEventArgs e)
        {
            var selektovaniNamestaj = (Namestaj)listBoxNamestaj.SelectedItem;
            var namestajProzor = new NamestajWindow(selektovaniNamestaj,NamestajWindow.Operacija.IZMENA);
            namestajProzor.ShowDialog();

            OsveziPrikaz();
        }

        private void IzbrisiNamestaj(object sender, RoutedEventArgs e)
        {
            var selektovaniNamestaj = (Namestaj)listBoxNamestaj.SelectedItem;
            var staraLista = Projekat.Instance.Namestaj;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {selektovaniNamestaj.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Namestaj namestaj = null;
                foreach (var n in staraLista)
                {
                    if (n.Id == selektovaniNamestaj.Id)
                    {
                        namestaj = n;
                    }
                }
                namestaj.Obrisan = true;

                Projekat.Instance.Namestaj = staraLista;
                //listBoxNamestaj.Items.Remove(selektovaniNamestaj);
                OsveziPrikaz();
            }
            

        }

        private void TipoviNamestajaPrelaz(object sender, RoutedEventArgs e)
        {
            // Treba drugi prozor/frejm/tab za ispis svih tipova, kao ovaj mainwindow
           // var tipoviNamestaja = new TipNamestajaUIWindow();
        }
    }
}
