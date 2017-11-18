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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {
        private Namestaj namestaj;
        private Operacija operacija;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA,
            BRISANJE
        };
        public NamestajWindow(Namestaj namestaj, Operacija operacija)
        {
            InitializeComponent();

            InicijalizujVrednosti(namestaj,operacija);
            
        }

        private void InicijalizujVrednosti(Namestaj namestaj,Operacija operacija)
        {
            this.namestaj = namestaj;
            this.operacija = operacija;

            tbNaziv.Text = namestaj.Naziv;
            foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            {
                cbTipNamestaja.Items.Add(tipNamestaja);
            }
            foreach (TipNamestaja TipNamestaja in cbTipNamestaja.Items)
            {
                if (TipNamestaja.Id == namestaj.TipNamestajaId)
                {
                    cbTipNamestaja.SelectedItem = TipNamestaja;
                }
            }

        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajNamestaj(object sender, RoutedEventArgs e)
        {
            List<Namestaj> postojeciNamestaj = Projekat.Instance.Namestaj;
            Namestaj namestajZaIzmenu = null;
            int TipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).Id;
            switch (operacija)
            {
                
                case Operacija.DODAVANJE:
                    namestaj = new Namestaj()
                    {
                        Id = postojeciNamestaj.Count + 1,
                        Naziv = tbNaziv.Text,
                        Cena = int.Parse(tbCena.Text),
                        TipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).Id

            };
                    postojeciNamestaj.Add(namestaj);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciNamestaj)
                    {
                        if (n.Id == namestaj.Id)
                        {

                            namestajZaIzmenu = n;
                            

                            
                        }
                    }
                    namestajZaIzmenu.Naziv = tbNaziv.Text;
                    namestajZaIzmenu.Cena = int.Parse(tbCena.Text);
                    namestajZaIzmenu.TipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).Id;
                    break;
              // ISTO KAO I FOR EACH IZNAD ^^^^
              //  Projekat.Instance.Namestaj - postojeciNamestaja.singleordefault( x=> x.Id == namestaj.Id)
                    
            }
            Projekat.Instance.Namestaj = postojeciNamestaj;
            this.Close();
        }
    }
}
