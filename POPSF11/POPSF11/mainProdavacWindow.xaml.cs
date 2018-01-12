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
using System.Windows.Shapes;

namespace POP_SF_11_GUI
{
    /// <summary>
    /// Interaction logic for mainProdavacWindow.xaml
    /// </summary>
    public partial class mainProdavacWindow : Window
    {
        ICollectionView view;

        public mainProdavacWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Racun.GetAll());
            dgRacuni.ItemsSource = view;
            dgRacuni.IsSynchronizedWithCurrentItem = true;
            dgRacuni.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajRacunProdaje(object sender, RoutedEventArgs e)
        {
            var noviRacun = new Racun()
            {
                Id = Projekat.Instance.Racuni.Count + 1,
                Kupac = "",
                BrojRacuna = ""

            };
            var racuniProzor = new ProdajaNamestajaWindow(noviRacun, ProdajaNamestajaWindow.Operacija.DODAVANJE);
            racuniProzor.ShowDialog();
        }

        private void IzmeniRacunProdaje(object sender, RoutedEventArgs e)
        {
            var selektovaniRacun = (Racun)dgRacuni.SelectedItem;
            var racuniProzor = new ProdajaNamestajaWindow(selektovaniRacun, ProdajaNamestajaWindow.Operacija.IZMENA);
            racuniProzor.ShowDialog();
        }
        private void ObrisiRacunProdaje(object sender, RoutedEventArgs e)
        {
            var selektovaniRacun = (Racun)dgRacuni.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {selektovaniRacun.BrojRacuna}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var n in Projekat.Instance.Racuni)
                {
                    if (n.Id == selektovaniRacun.Id)
                    {

                        n.Obrisan = true;
                        Racun.Delete(n);
                        view.Refresh();
                        break;
                    }
                }
            }
          //  GenericSerializer.Serialize("Racuni.xml", Projekat.Instance.Racuni);
        }
        private void dgRacuni_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
        }
    }
}
