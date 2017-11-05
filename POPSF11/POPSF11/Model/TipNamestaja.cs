using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSF11.Model
{
    [Serializable]
    public enum TipNamestaja1
    {
        sofaTipNamestaja
    }
    public class TipNamestaja
    {
        public int Id { get; set; }
        public bool Obrisan { get; set; }
        public string Naziv { get; set; }
    }
}
