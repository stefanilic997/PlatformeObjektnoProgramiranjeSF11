using POP_SF_11_GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_GUI.Model
{
    [Serializable]
    public class Namestaj
    {
        public int Id { get; set;}
        public string Naziv { get; set; }
        public int Cena { get; set; }
        public int Kolicina { get; set; }
        public bool Obrisan { get; set; }
        public int TipNamestajaId { get; set; }
        /*Treba int TipNamestajId*/
        public int AkcijaId { get; set; }

        
        public static Namestaj GetById(int id)
        {
            foreach (var Namestaj in Projekat.Instance.Namestaj)
            {
                if (Namestaj.Id == id)
                {
                    return Namestaj;
                }

            }
            return null;

        }
        public override string ToString()
        {
            return ($"Naziv: {Naziv},{Cena},{TipNamestajaId}");
        }

    }
}
