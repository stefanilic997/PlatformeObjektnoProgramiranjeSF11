using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_GUI.Model
{
    [Serializable]
    public class AkcijskaProdaja
    {
        public int Id { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavresetka { get; set; }
        public decimal Popust { get; set; }
        public bool Obrisan { get; set; }

        public static AkcijskaProdaja GetByIdAkcija(int id)
        {
            foreach (var AkcijskaProdaja in Projekat.Instance.AkcijskeProdaje)
            {
                if (AkcijskaProdaja.Id == id)
                {
                    return AkcijskaProdaja;
                }

            }
            return null;

        }
    }
}
