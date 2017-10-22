using POPSF11.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSF11
{
    class Test1
    {
        static void Main(string[] args)
        {
            Person p1 = new Person("Butt", "Man");
            p1.Name = "New name John";

            List<string> strings = new List<string>();
            List<string> imena = new List<string>();
            List<string> prezimena = new List<string>();


            imena.Add("Pera");
            imena.Add("zika");
            imena.Add("mika");

            prezimena.Add("peric");
            prezimena.Add("zikic");
            prezimena.Add("mikic");

            for (int i = 0; i < imena.Count; i++)
            {
                Console.WriteLine(imena[i] + " " + prezimena[i]);
            }
            int[] a = { 1, 2, 3, 4, 5 };
            for (var i = 0; i < a.Count(); i++)
            {
                Console.Write(a[i] + " ");
                strings.Add($"broj {a[i]}");

            }


            strings.Add("rucno dodat broj 4");

            foreach (var broj in strings)
            {
                Console.WriteLine(broj);
            }

            var salon = new Salon()
            {
                Id = 1,
                Adresa = " Sime Matavulja 8",
                Email = "kontakt@salon.com"
            };
            Console.WriteLine(String.Format("\n Hello my name is {0}, and my last name is {1}", p1.Name, p1.Surname));
            Console.WriteLine($"Hello my name is :{p1.Name}, and my last name is {p1.Surname}");
            Console.ReadLine();
        }
    }
}

