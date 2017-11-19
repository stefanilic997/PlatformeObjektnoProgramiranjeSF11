using POP_SF_11_GUI.Model;
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
    /// Interaction logic for AkcijskaProdajaUIWindow.xaml
    /// </summary>
    public partial class AkcijskaProdajaUIWindow : Window
    {
        private AkcijskaProdaja akcijskaProdaja;
        private Operacija operacija;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA,
            BRISANJE
        };

        public AkcijskaProdajaUIWindow(AkcijskaProdaja akcijskaProdaja, Operacija operacija)
        {
            InitializeComponent();

            InicijalizujVrednost(akcijskaProdaja, operacija);
        }

        private void InicijalizujVrednost(AkcijskaProdaja akcijskaProdaja, Operacija operacija)
        {
            this.akcijskaProdaja = akcijskaProdaja;
            this.operacija = operacija;

            tbPopust.Text = akcijskaProdaja.Popust.ToString();
            datumPocetka.SelectedDate = akcijskaProdaja.DatumPocetka;
            datumZavrsetka.SelectedDate = akcijskaProdaja.DatumZavresetka;

        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajAkcijskuProdaju(object sender, RoutedEventArgs e)
        {
            List<AkcijskaProdaja> postojeceAkcije = Projekat.Instance.AkcijskeProdaje;
            AkcijskaProdaja akcijaZaIzmenu = null;
            switch (operacija)
            {

                case Operacija.DODAVANJE:
                    akcijskaProdaja = new AkcijskaProdaja()
                    {
                        Id = postojeceAkcije.Count + 1,
                        Popust = int.Parse(tbPopust.Text),
                        DatumPocetka = (DateTime)datumPocetka.SelectedDate,
                        DatumZavresetka = (DateTime)datumZavrsetka.SelectedDate,
                    };
                    postojeceAkcije.Add(akcijskaProdaja);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeceAkcije)
                    {
                        if (n.Id == akcijskaProdaja.Id)
                        {

                            akcijaZaIzmenu = n;



                        }
                    }
                    akcijaZaIzmenu.Popust = int.Parse(tbPopust.Text);
                    akcijaZaIzmenu.DatumPocetka = (DateTime)datumPocetka.SelectedDate;
                    akcijaZaIzmenu.DatumZavresetka = (DateTime)datumZavrsetka.SelectedDate;
                    break;
                    
            }
            Projekat.Instance.AkcijskeProdaje = postojeceAkcije;
            this.Close();
        }
    }
}
