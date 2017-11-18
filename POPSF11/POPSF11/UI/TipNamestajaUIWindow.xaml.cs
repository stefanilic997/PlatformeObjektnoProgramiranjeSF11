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
    /// Interaction logic for TipNamestajaUIWindow.xaml
    /// </summary>
    public partial class TipNamestajaUIWindow : Window
    {
        private TipNamestaja tipNamestaja;
        private Operacija operacija;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA,
            BRISANJE
        };

        public TipNamestajaUIWindow()
        {
            InitializeComponent();
            InicijalizujVrednosti(tipNamestaja, operacija);
        }

        private void InicijalizujVrednosti(TipNamestaja tipNamestaja, Operacija operacija)
        {
            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;

            tbNaziv.Text = tipNamestaja.Naziv;

        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajTipNamestaja(object sender, RoutedEventArgs e)
        {
            List<TipNamestaja> postojeciTipoviNamestaja = Projekat.Instance.TipoviNamestaja;
            switch (operacija)
            {

                case Operacija.DODAVANJE:
                    var noviTipNamestaja = new TipNamestaja()
                    {
                        Naziv = tbNaziv.Text


                    };
                    postojeciTipoviNamestaja.Add(noviTipNamestaja);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciTipoviNamestaja)
                    {
                        if (n.Id == tipNamestaja.Id)
                        {
                            n.Naziv = tbNaziv.Text;
                            break;
                        }
                    }
                    break;


            }
            Projekat.Instance.TipoviNamestaja = postojeciTipoviNamestaja;
            this.Close();
        }
    }
}
