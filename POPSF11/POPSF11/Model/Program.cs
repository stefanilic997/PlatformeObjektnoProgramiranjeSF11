using POPSF11.Model;
using POPSF11.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSF11
{
    class Program
    {
        public static List<Korisnik> Korisnici { get; set; } = new List<Korisnik>();
        public static List<Namestaj> Namestaj { get; set; } = new List<Namestaj>();
        public static List<TipNamestaja> TipNamestaja { get; set; } = new List<TipNamestaja>();
        public static List<AkcijskaProdaja> AkcijskaProdaja { get; set; } = new List<AkcijskaProdaja>();
        static void Main(string[] args)
        {

            

            TipNamestaja.Add(new TipNamestaja()
            {
                Id = 4,
                Naziv= "proba"
            });
            AkcijskaProdaja.Add(new AkcijskaProdaja()
            {
                Id = 1,
                Popust = 10
            });

            
            List<Namestaj> ln2 = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
            Console.WriteLine("Finished serialization");
            Namestaj = ln2;


            Salon s1 = new Salon()
            {
                Id = 1,
                Naziv = " Salon namestaja",
                Adresa = "bez broja",
                Telefon = "021556123",
                Email = "salon@gmail.com",
                AdresaSajta = "salonnamestaja.com",
                PIB = 3322125,
                MaticniBroj = 22113551,
                BrojZiroRacuna = "00443-21234542"


            };
            Korisnik k1 = new Korisnik()
            {
                Id = 1,
                Ime = "admin1",
                Prezime = "abc",
                KorisnickoIme = "a",
                Lozinka = "a",
                TipKorisnika = TipKorisnika.Administrator,
                Obrisan = false
            };
            Korisnik k2 = new Korisnik()
            {
                Id = 2,
                Ime = "prodavac1",
                Prezime = "abc",
                KorisnickoIme = "b",
                Lozinka = "b",
                TipKorisnika = TipKorisnika.Prodavac,
                Obrisan = false
            };
            var lk = new List<Korisnik>();
            lk.Add(k1);
            lk.Add(k2);

            Console.WriteLine("Login");
            Console.WriteLine("Unesite korisnicko ime");
            string korIme = Console.ReadLine();

            Console.WriteLine("Unesite lozinku");
            string lozinka = Console.ReadLine();

            foreach (var korisnik in lk)
            {
                if (korIme == korisnik.KorisnickoIme &&
                    lozinka == korisnik.Lozinka &&
                    korisnik.TipKorisnika == TipKorisnika.Administrator)
                {
                    IspisiGlavniMeni();
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            /*Console.WriteLine($"Dobrodosli u salon namestaja{s1.Naziv}.");
            IspisiGlavniMeni();*/
            

        }
        public static void IspisiGlavniMeni()
        {
            int izbor=0;

            do 
            {
                Console.WriteLine("GLAVNI MENI");
                Console.WriteLine("1. Rad sa namestajem");
                Console.WriteLine("2. Rad sa tipom");
                //TODO Dodaj ostale stvari 
                Console.WriteLine("0. Izlaz iz aplikacije");
                


            } while (izbor<0 || izbor >2);
            izbor = int.Parse(Console.ReadLine());

            switch (izbor)
            {
                case 1:
                    IspisiMeniNamestaja();
                    break;
                case 2:
                    IspisiMeniTipaNamestaja();
                    break;
                default:
                   // Environment.Exit(0);
                    break;
            }
            
        }

        private static void IspisiMeniTipaNamestaja()
        {
            throw new NotImplementedException();
        }

        private static void IspisiMeniNamestaja()
        {
            int izbor = 0;
            
            do
            {
                
                Console.WriteLine("1. Listing namestaja");
                Console.WriteLine("2. Dodaj novi namestaj");
                Console.WriteLine("3. Izmeni postojeci namestaj");
                Console.WriteLine("4. Obrisi postojeci namestaj");
                Console.WriteLine("5. Pretraga namestaja");
                Console.WriteLine("0. Povratak na glavni meni");
                



            } while (izbor < 0 || izbor > 5);

            izbor = int.Parse(Console.ReadLine());
            

            switch (izbor)
            {
                
                case 1:
                    IzlistajNamestaj();
                    break;
                case 2:
                    DodajNoviNamestaj();
                    break;
                case 3:
                    IzmeniNamestaj();
                    break;
                case 4:
                    ObrisiNamestaj();
                    break;
                case 5:
                    PretragaNamestaja();
                    break;
                case 0:
                    IspisiGlavniMeni();
                    break;
                default:
                    break;
            }
            
        }

        private static void PretragaNamestaja()
        {
            Console.WriteLine("Pretraga namestaja");
            int IdPretrage = int.Parse(Console.ReadLine());
            foreach (var namestaj in Namestaj)
            {
                if (namestaj.Id == IdPretrage)
                {
                    Console.WriteLine($"{namestaj.Id},{namestaj.Naziv},{namestaj.Kolicina}");
                    IspisiMeniNamestaja();
                }
                else
                {
                    Console.WriteLine("Namestaj ne postoji");
                    IspisiMeniNamestaja();
                }
            }
        }

        private static void ObrisiNamestaj()
        {
            Console.WriteLine("Brisanje namestaja");
            Console.WriteLine("Unesite id namestaja za brisanje");
            int IdBrisanje = int.Parse(Console.ReadLine());
            foreach (var namestaj in Namestaj)
            {
                if (namestaj.Id == IdBrisanje)
                {
                    Console.WriteLine("Namestaj postoji/n");
                    namestaj.Obrisan = true;
                    Namestaj.Remove(namestaj);
                    Console.WriteLine("Namestaj je obrisan");
                    IspisiMeniNamestaja();

                }
                else
                {
                    Console.WriteLine("Namestaj ne postoji");
                    IspisiMeniNamestaja();
                }
            }
            
        }

        private static void IzmeniNamestaj()
        {
            Console.WriteLine("Izmena namestaja");
            Console.WriteLine("Unesite id namestaja za izmenu");
            int IdIzmena = int.Parse(Console.ReadLine());
            foreach (var namestaj in Namestaj)
            {
                if (namestaj.Id == IdIzmena){
                    Console.WriteLine("Namestaj postoji/nUpisite nove podatke");
                    Namestaj.Remove(namestaj);
                    DodajNoviNamestaj();

                }
                else
                {
                    Console.WriteLine("Namestaj ne postoji");
                    IspisiMeniNamestaja();
                }
            }
            
        }

        private static void IzlistajNamestaj()
        {
            int index = 0;
            Console.WriteLine("===== Listing namestaja=====");
            foreach (var namestaj in Namestaj)
            {
                if (namestaj.Obrisan==false)
                {
                    Console.WriteLine($"{ ++index }.{ namestaj.Naziv},cena: { namestaj.Cena},kolicina: {namestaj.Kolicina}");
                }
            }
            IspisiMeniNamestaja();
        }

        private static void DodajNoviNamestaj()
        {
            
            Namestaj namestaj = new Namestaj();
            Console.WriteLine("Dodavanje namestaja");

            Console.WriteLine("Unesite ID:");
            int Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Unesite naziv:");
            string Naziv = Console.ReadLine();

            Console.WriteLine("Unesite cenu:" );
            int Cena = int.Parse(Console.ReadLine());

            Console.WriteLine("Unesite kolicinu:");
            int Kolicina = int.Parse(Console.ReadLine());

            namestaj.Id = Id;
            namestaj.Naziv = Naziv;
            namestaj.Cena = Cena;
            namestaj.Kolicina = Kolicina;
            namestaj.Obrisan = false;
            namestaj.TipNamestaja = TipNamestaja[0];
            namestaj.Akcija = AkcijskaProdaja[0];

            
            
            Namestaj.Add(namestaj);
            var ln1 = Namestaj;
            GenericSerializer.Serialize<Namestaj>("namestaj.xml", ln1);



            IspisiMeniNamestaja();
        }
    }
}
