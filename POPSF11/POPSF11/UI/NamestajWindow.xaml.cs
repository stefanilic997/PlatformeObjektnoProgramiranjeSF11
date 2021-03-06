﻿using POP_SF_11_GUI.Model;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {
        private Namestaj namestaj;
        private Operacija operacija;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public NamestajWindow(Namestaj namestaj, Operacija operacija)
        {
            InitializeComponent();

            this.namestaj = namestaj;
            this.operacija = operacija;
            cbTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja;
            cbAkcija.ItemsSource = Projekat.Instance.AkcijskeProdaje;

            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;
            cbTipNamestaja.DataContext = namestaj;
            cbAkcija.DataContext = namestaj;

        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SacuvajNamestaj(object sender, RoutedEventArgs e)
        {
            var postojeciNamestaj = Projekat.Instance.sviNamestaji;
            switch (operacija)
            {
                
                case Operacija.DODAVANJE:
                    namestaj.Id = postojeciNamestaj.Count + 1;
                    namestaj.Naziv = tbNaziv.Text;
                    namestaj.Cena = int.Parse(tbCena.Text);
                    namestaj.Kolicina = int.Parse(tbKolicina.Text);
                    namestaj.TipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).Id;
                    namestaj.AkcijaId = ((AkcijskaProdaja)cbAkcija.SelectedItem).Id;

                    Namestaj.Create(namestaj);
                    postojeciNamestaj.Add(namestaj);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciNamestaj)
                    {
                        if (n.Id == namestaj.Id)
                        {

                            n.Naziv = tbNaziv.Text;
                            n.Cena = int.Parse(tbCena.Text);
                            n.Kolicina = int.Parse(tbKolicina.Text);
                            n.AkcijaId = ((AkcijskaProdaja)cbAkcija.SelectedItem).Id;
                            n.TipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).Id;

                            Namestaj.Update(n);
                        }
                    }
                    break;
              // ISTO KAO I FOR EACH IZNAD ^^^^
              //  Projekat.Instance.sviNamestaji - postojeciNamestaja.singleordefault( x=> x.Id == namestaj.Id)
                    
            }
           // GenericSerializer.Serialize("namestaj.xml", postojeciNamestaj);
            this.Close();
        }
    }
}
