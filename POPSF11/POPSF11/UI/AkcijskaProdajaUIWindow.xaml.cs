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
            var postojeceAkcije = Projekat.Instance.AkcijskeProdaje;
            switch (operacija)
            {

                case Operacija.DODAVANJE:
                    akcijskaProdaja.Id = postojeceAkcije.Count + 1;
                    akcijskaProdaja.Popust = int.Parse(tbPopust.Text);
                    akcijskaProdaja.DatumPocetka = (DateTime)datumPocetka.SelectedDate;
                    akcijskaProdaja.DatumZavresetka = (DateTime)datumZavrsetka.SelectedDate;
                    postojeceAkcije.Add(akcijskaProdaja);
                    AkcijskaProdaja.Create(akcijskaProdaja);
                    break;
                case Operacija.IZMENA:
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
                    break;
            }
           // GenericSerializer.Serialize("Akcije.xml", postojeceAkcije);
            this.Close();
        }
    }
}
