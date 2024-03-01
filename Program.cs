using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.Design;

namespace szamitasTechinkaiEszkozok
{
    internal class Program
    {
        class Adatok
        {
            public string nev;
            public int ar, db;
            public List<string> parameterek = new List<string>();
            public List<string> jellemzok = new List<string>();
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
                    temp.jellemzok.Add(svJellem[i]);
                }

                adatoks.Add(temp);
            }
            sr.Close();
            return adatoks;
        }

        static void Kilistaz(List<Adatok> adatoks)
        {
            for (int i = 0; i < adatoks.Count(); i++)
            {
                Console.Write($"{adatoks[i].nev} - {adatoks[i].ar} FT - {adatoks[i].db} db -");
                for (int j = 0; j < adatoks[i].parameterek.Count(); j++)
                {
                    Console.Write($" {adatoks[i].parameterek[j]} +");
                }

                for (int j = 0; j < adatoks[i].jellemzok.Count(); j++)
                {
                    Console.Write($" - {adatoks[i].jellemzok[j]}");
                }
                Console.WriteLine();
            }
        }

        static void KilistazTermek(List<Adatok> adatoks)
        {
            Console.Clear();
            Console.Title = "Eszközök";
            Console.SetWindowSize(150, 63);
            Console.CursorSize = 20;

            for (int i = 0; i < adatoks.Count(); i++)
            {
                Console.CursorLeft = 0;
                Console.Write($"{adatoks[i].nev}");
                Console.CursorLeft = 90;
                Console.Write($"{adatoks[i].ar}FT\n");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            List<Adatok> adatoks = Beolvasas();


            int kivalasztott = 0;
            string[] opciok = { "Kilistázás", "Új adat", "Módosítás", "Törlés" };
            #region Menü
            ConsoleKeyInfo lenyomott;

            do
            {
            MenuCommand:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Válasszon az alábbi lehetőségek közül:\n");

                #region Menü kiírása
                for (int i = 0; i < opciok.Length; i++)
                {
                    if (i == kivalasztott)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("\t" + (i + 1) + ") " + opciok[i]);
                }
                #endregion

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < opciok.Length - 1) kivalasztott++; break;
                }
                #endregion
                if (kivalasztott == 0)
                {
                    KilistazTermek(adatoks);
                    Console.Write("Írd be azt hogy 'vissza' ha vissza szeretnél térni a főmenűbe: ");
                    string vissza = Console.ReadLine().ToUpper();
                    if (vissza == "VISSZA")
                    {
                        goto MenuCommand;
                    }


                }
            } while (lenyomott.Key != ConsoleKey.Enter);
            #endregion



        }
    }
}