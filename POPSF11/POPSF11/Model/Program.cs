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
        public static List<Namestaj> Namestaj { get; set; } = new List<Namestaj>();
        public static List<TipNamestaja> TipNamestaja { get; set; } = new List<TipNamestaja>();
        static void Main(string[] args)
        {
            List<Namestaj> namestaj = Projekat.Instance.Namestaj;
            namestaj.Add(new Namestaj { Id = 12, Naziv = "novi" });
            foreach (var komad in namestaj)
            {
                Console.WriteLine($"{ komad.Naziv}");
            }
            TipNamestaja.Add(new TipNamestaja()
            {
                Id = 4,
                Naziv= "proba"
            });
            var n1 = new Namestaj()
            {
                Id = 1,
                Naziv = "super svetlo",
                Cena = 28,
                Kolicina = 3,
                TipNamestaja = TipNamestaja[0]
            };
            var n2 = new Namestaj()
            {
                Id = 3,
                Naziv = "super lampa",
                Cena = 35,
                Kolicina = 13,
                TipNamestaja = TipNamestaja[0]
            };
            var ln1 = new List<Namestaj>();
            ln1.Add(n1);
            ln1.Add(n2);


            Console.WriteLine("Serialization...");
            GenericSerializer.Serialize<Namestaj>("namestaj.xml", ln1);
            
            List<Namestaj> ln2 = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
            Console.WriteLine("Finished serialization");
            Console.ReadLine();

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
            Namestaj namestaj1 = new Namestaj();
            Namestaj.Add(new Namestaj()
            {
                Id = 1,
                Naziv = "namestaj proba",
                Cena = 20,
                Kolicina = 13,
                Akcija = null,
                
                

            });
            Console.WriteLine($"Dobrodosli u salon namestaja{s1.Naziv}.");
            IspisiGlavniMeni();
            

        }
        private static void IspisiGlavniMeni()
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
                Console.WriteLine("0. Povratak na glavni meni");
                



            } while (izbor < 0 || izbor > 4);

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
                case 0:
                    IspisiGlavniMeni();
                    break;
                default:
                    break;
            }
            
        }

        private static void ObrisiNamestaj()
        {
            throw new NotImplementedException();
        }

        private static void IzmeniNamestaj()
        {
            throw new NotImplementedException();
        }

        private static void IzlistajNamestaj()
        {
            int index = 0;
            Console.WriteLine("===== Listing namestaja=====");
            foreach (var namestaj in Namestaj)
            {
                Console.WriteLine($"{ ++index }.{ namestaj.Naziv},cena: { namestaj.Cena}");
            }
            IspisiMeniNamestaja();
        }

        private static void DodajNoviNamestaj()
        {
            throw new NotImplementedException();
        }
    }
}
