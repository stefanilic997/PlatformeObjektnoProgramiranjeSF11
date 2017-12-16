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
    /// Interaction logic for SalonWindow.xaml
    /// </summary>
    public partial class SalonWindow : Window
    {
        private Salon salon;
        private Operacija operacija;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public SalonWindow(Salon salon, Operacija operacija)
        {
            InitializeComponent();

            this.salon = salon;
            this.operacija = operacija;
            

            tbNaziv.DataContext = salon;
            tbAdresa.DataContext = salon;
            tbTelefon.DataContext = salon;
            tbEmail.DataContext = salon;
            tbAdresaSajta.DataContext = salon;
            tbPib.DataContext = salon;
            tbMaticniBroj.DataContext = salon;
            tbZiroRacun.DataContext = salon;


        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajSalon(object sender, RoutedEventArgs e)
        {
            var postojeciSaloni = Projekat.Instance.Saloni;
            switch (operacija)
            {

                case Operacija.DODAVANJE:
                    salon.Id = postojeciSaloni.Count + 1;
                    salon.Naziv = tbNaziv.Text;
                    salon.Adresa = tbAdresa.Text;
                    salon.Telefon = tbTelefon.Text;
                    salon.Email = tbEmail.Text;
                    salon.AdresaSajta = tbAdresaSajta.Text;
                    salon.PIB = int.Parse(tbPib.Text);
                    salon.MaticniBroj = int.Parse(tbMaticniBroj.Text);
                    salon.BrojZiroRacuna = tbZiroRacun.Text;

                    postojeciSaloni.Add(salon);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciSaloni)
                    {
                        if (n.Id == salon.Id)
                        {

                            n.Naziv = tbNaziv.Text;
                            n.Adresa = tbAdresa.Text;
                            n.Telefon = tbTelefon.Text;
                            n.Email = tbEmail.Text;
                            n.AdresaSajta = tbAdresaSajta.Text;
                            n.PIB = int.Parse(tbPib.Text);
                            n.MaticniBroj = int.Parse(tbMaticniBroj.Text);
                            n.BrojZiroRacuna = tbZiroRacun.Text;

                        }
                    }
                    break;
                    
            }
            GenericSerializer.Serialize("Saloni.xml", postojeciSaloni);
            this.Close();
        }
    }
}

