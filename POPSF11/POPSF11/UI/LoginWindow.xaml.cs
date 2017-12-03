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
            ObservableCollection<Korisnik> postojeciKorisnici = Projekat.Instance.Korisnici;
            string korIme = tbKorisnickoIme.Text;
            string password = pbLozinka.Password;
            
            foreach (var korisnik in postojeciKorisnici)
            {
                
                if (korIme == korisnik.KorisnickoIme && password== korisnik.Lozinka)
                {
                    if (korisnik.TipKorisnika == TipKorisnika.Administrator)
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.ShowDialog();
                    }else if (korisnik.TipKorisnika == TipKorisnika.Prodavac)
                    {
                        var mainProdavacWindow = new mainProdavacWindow();
                        mainProdavacWindow.ShowDialog();
                    }
                    
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
