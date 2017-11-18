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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            List<Korisnik> postojeciKorisnici = Projekat.Instance.Korisnici;

            foreach (var korisnik in postojeciKorisnici)
            {
                if (tbKorisnickoIme.Text == korisnik.KorisnickoIme && pbLozinka.Password == korisnik.Lozinka)
                {
                    Console.WriteLine("Uspesno ste se ulogovali");
                    var mainWindow = new MainWindow();
                }
                else
                {
                    Console.WriteLine("Neispravni login podaci");
                    pbLozinka.Clear();
                    tbKorisnickoIme.Clear();
                }
            }
        }
    }
}
