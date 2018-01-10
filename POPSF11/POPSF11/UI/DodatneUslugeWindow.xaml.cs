
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
    /// Interaction logic for DodatneUslugeWindow.xaml
    /// </summary>
    public partial class DodatneUslugeWindow : Window
    {
        private DodatnaUsluga dodatnaUsluga;
        private Operacija operacija;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        public DodatneUslugeWindow(DodatnaUsluga dodatnaUsluga, Operacija operacija)
        {
            InitializeComponent();

            this.dodatnaUsluga = dodatnaUsluga;
            this.operacija = operacija;
            tbNaziv.DataContext = dodatnaUsluga;
            tbCena.DataContext = dodatnaUsluga;
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajDodatnuUslugu(object sender, RoutedEventArgs e)
        {
            var postojeceDodatneUsluge = Projekat.Instance.DodatneUsluge;
            switch (operacija)
            {

                case Operacija.DODAVANJE:
                    dodatnaUsluga.Id = postojeceDodatneUsluge.Count + 1;
                    dodatnaUsluga.Naziv = tbNaziv.Text;
                    dodatnaUsluga.Cena = double.Parse(tbCena.Text);
                    postojeceDodatneUsluge.Add(dodatnaUsluga);
                    DodatnaUsluga.Create(dodatnaUsluga);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeceDodatneUsluge)
                    {
                        if (n.Id == dodatnaUsluga.Id)
                        {
                            n.Naziv = tbNaziv.Text;
                            n.Cena = double.Parse(tbCena.Text);
                            DodatnaUsluga.Update(n);
                            break;
                        }
                    }
                    break;


            }
            //GenericSerializer.Serialize("DodatneUsluge.xml", postojeceDodatneUsluge);
            this.Close();
        }
    }
}
