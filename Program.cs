using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace szamitasTechinkaiEszkozok
{
    internal class Program
    {
        class Adatok 
        {
            public string nev;
            public int ar, db;
            public List<string> parameterek, jellemzok;
        }
        static List<Adatok> Beolvasas()
        {
            StreamReader sr = new StreamReader("Informaciok.txt");
            List<Adatok> adatoks = new List<Adatok>();

            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                string[] sorok = sor.Split(';');
                
                Adatok temp = new Adatok();
                temp.nev = sorok[0];
                temp.ar = int.Parse(sorok[1]);
                temp.db = int.Parse(sorok[2]);

                string[] svPara = sorok[3].Split('*');
                for (int i = 0; i < svPara.Length; i++)
                {
                    temp.parameterek.Add(svPara[i]);
                }

                string[] svJellem = sorok[4].Split('*');
                for (int i = 0; i < svJellem.Length; i++)
                {
                    temp.parameterek.Add(svJellem[i]);
                }

                adatoks.Add(temp);
            }
            sr.Close();
            return adatoks;
        }
        static void Main(string[] args)
        {
            List<Adatok> adatoks = Beolvasas();

            Console.WriteLine($"{adatoks[0].nev},{adatoks[0].ar}-{adatoks[0].parameterek[0]}-{adatoks[0].jellemzok[0]}");
        }
    }
}
