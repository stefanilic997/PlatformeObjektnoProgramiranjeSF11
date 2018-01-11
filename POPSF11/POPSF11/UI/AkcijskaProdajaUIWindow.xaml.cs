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
    /// Interaction logic for AkcijskaProdajaUIWindow.xaml
    /// </summary>
    public partial class AkcijskaProdajaUIWindow : Window
    {
        private AkcijskaProdaja akcijskaProdaja;
        private Operacija operacija;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        public AkcijskaProdajaUIWindow(AkcijskaProdaja akcijskaProdaja, Operacija operacija)
        {
            InitializeComponent();

            this.akcijskaProdaja = akcijskaProdaja;
            this.operacija = operacija;
            tbPopust.DataContext = akcijskaProdaja;
            datumPocetka.SelectedDate = akcijskaProdaja.DatumPocetka;
            datumZavrsetka.SelectedDate = akcijskaProdaja.DatumZavresetka;

        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajAkcijskuProdaju(object sender, RoutedEventArgs e)
        {
            DateTime datumP = datumPocetka.SelectedDate.Value.Date;
            DateTime datumZ = datumZavrsetka.SelectedDate.Value.Date;
            var postojeceAkcije = Projekat.Instance.AkcijskeProdaje;
            int compare = DateTime.Compare(datumP, datumZ);
            if (akcijskaProdaja.Popust > 0 || akcijskaProdaja.Popust < 96)
            {


                switch (operacija)
                {

                    case Operacija.DODAVANJE:

                        if (compare <= 0)
                        {


                            akcijskaProdaja.Id = postojeceAkcije.Count + 1;
                            akcijskaProdaja.Popust = int.Parse(tbPopust.Text);
                            akcijskaProdaja.DatumPocetka = (DateTime)datumPocetka.SelectedDate;
                            akcijskaProdaja.DatumZavresetka = (DateTime)datumZavrsetka.SelectedDate;
                            postojeceAkcije.Add(akcijskaProdaja);
                            AkcijskaProdaja.Create(akcijskaProdaja);
                        }
                        else
                        {
                            MessageBox.Show("Datum zavrsetka mora da bude kasnije od datuma pocetka akcije", "Pogresan datum", MessageBoxButton.OK);
                        }
                        break;
                    case Operacija.IZMENA:
                        if (compare <= 0)
                        {


                            foreach (var n in postojeceAkcije)
                            {
                                if (n.Id == akcijskaProdaja.Id)
                                {

                                    n.Popust = int.Parse(tbPopust.Text);
                                    n.DatumPocetka = (DateTime)datumPocetka.SelectedDate;
                                    n.DatumZavresetka = (DateTime)datumZavrsetka.SelectedDate;

                                    AkcijskaProdaja.Update(n);

                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Datum zavrsetka mora da bude kasnije od datuma pocetka akcije", "Pogresan datum", MessageBoxButton.OK);

                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Akcija ne moze biti manja od 0 ili veca od 95%","Neispravan Popust", MessageBoxButton.OK);

            }
            // GenericSerializer.Serialize("Akcije.xml", postojeceAkcije);
            this.Close();
        }
    }
}
