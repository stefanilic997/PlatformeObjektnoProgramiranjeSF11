using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSF11.Model
{
    [Serializable]
    public class DodatnaUsluga
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int Cena { get; set; }
    }
}
