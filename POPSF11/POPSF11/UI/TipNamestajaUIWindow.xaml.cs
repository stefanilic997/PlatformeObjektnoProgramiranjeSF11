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

        public TipNamestajaUIWindow(TipNamestaja tipNamestaja,Operacija operacija)
        {
            InitializeComponent();

            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;
            tbNaziv.DataContext = tipNamestaja;
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajTipNamestaja(object sender, RoutedEventArgs e)
        {
            var postojeciTipoviNamestaja = TipNamestaja.GetAll();
            switch (operacija)
            {

                case Operacija.DODAVANJE:
                    tipNamestaja.Id = postojeciTipoviNamestaja.Count + 1;
                    tipNamestaja.Naziv = tbNaziv.Text;

                    postojeciTipoviNamestaja.Add(tipNamestaja);
                    TipNamestaja.Create(tipNamestaja);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciTipoviNamestaja)
                    {
                        if (n.Id == tipNamestaja.Id)
                        {
                            n.Naziv = tbNaziv.Text;
                            TipNamestaja.Update(n);
                            
                        }
                    }
                    break;


            }
          //  GenericSerializer.Serialize("tipoviNamestaja.xml", postojeciTipoviNamestaja);
            this.Close();
        }
    }
}
