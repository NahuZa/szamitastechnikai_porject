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

        static bool AkciosE(List<Adatok> adatoks, int index)
        {
            int db = adatoks[index].db;
            bool akcios = false;
            if (db < 10)
            {
                akcios = true;
            }
            return akcios;
        }

        static void KilistazInformacio(List<Adatok> adatoks, int index)
        {
            Console.Clear();
            Console.Title = adatoks[index].nev + " információi:";
            Console.SetWindowSize(150, 63);
            Console.CursorSize = 20;

            if (AkciosE(adatoks, index))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Akciós!");
                Console.CursorLeft = 0;
                Console.Write($"{adatoks[index].nev}");
                Console.CursorLeft = 50;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{adatoks[index].ar}FT->");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{adatoks[index].ar - (adatoks[index].ar * 0.20)}FT\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Készleten: {adatoks[index].db} db");
                Console.WriteLine();
            }
            else
            {
                Console.CursorLeft = 0;
                Console.Write($"{adatoks[index].nev}");
                Console.CursorLeft = 50;
                Console.Write($"{adatoks[index].ar}FT\n");
                Console.WriteLine($"Készleten: {adatoks[index].db} db");
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorLeft = 0;
            Console.WriteLine("Műszaki paraméterek:\n ");


            for (int i = 0; i < adatoks[index].parameterek.Count(); i++)
            {
                Console.CursorLeft = 10;
                Console.WriteLine(adatoks[index].parameterek[i]);
            }

            Console.CursorLeft = 0;
            Console.WriteLine("\nEszköz jellemzői:\n ");

            for (int i = 0; i < adatoks[index].jellemzok.Count(); i++)
            {
                Console.CursorLeft = 10;
                Console.WriteLine(adatoks[index].jellemzok[i]);
            }
            Console.WriteLine();

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
                    int bentkivalasztott = 0;
                    ConsoleKeyInfo bentlenyomott;
                    
                    do { 
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    for (int i = 0; i < adatoks.Count; i++)
                    {
                        if (i == bentkivalasztott)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                            
                            Console.Title = "Eszközök";
                            Console.SetWindowSize(150, 63);
                            Console.CursorSize = 20;
                            Console.CursorLeft = 0;
                            Console.Write($"{i+1}) {adatoks[i].nev}");
                            Console.CursorLeft = 90;
                            Console.Write($"{adatoks[i].ar}FT\n");

                        }

                    #region Gomblenyomás

                    bentlenyomott = Console.ReadKey();

                    switch (bentlenyomott.Key)
                    {
                        case ConsoleKey.UpArrow: if (bentkivalasztott > 0) bentkivalasztott--; break;
                        case ConsoleKey.DownArrow: if (bentkivalasztott < adatoks.Count - 1) bentkivalasztott++; break;
                    }
                   

                    } while (bentlenyomott.Key != ConsoleKey.Enter);
                    #endregion
                    Console.Clear();
                    KilistazInformacio(adatoks, bentkivalasztott);
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