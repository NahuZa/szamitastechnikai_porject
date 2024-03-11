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
            Console.SetWindowSize(150, 43);
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
            Console.SetWindowSize(150, 43);
            Console.CursorSize = 20;

            for (int i = 0; i < adatoks.Count(); i++)
            {
                Console.CursorLeft = 0;
                Console.Write($"{i+1}) {adatoks[i].nev}");
                Console.CursorLeft = 90;
                Console.Write($"{adatoks[i].ar}FT\n");
            }
            Console.WriteLine();
        }

    
        static void Uj(List<Adatok> adatoks)

        {

       

            List<string> parameterek = new List<string>();

            List<string> jellemzok = new List<string>();

            Adatok temp = new Adatok();

            Console.Clear();

            Console.WriteLine("Az eszköz neve? ");

            string nev = Console.ReadLine();

            temp.nev = nev;

            Console.WriteLine("\nAz eszköz ára? ");

            string ar = Console.ReadLine();

            temp.ar = int.Parse(ar);

            Console.WriteLine("\nAz eszközből hány darab található készleten? ");

            string db = Console.ReadLine();

            temp.db = int.Parse(db);

            string sv;

            do

            {

                Console.WriteLine("\nAz eszköz egy paramétere? (0-KÖVETKEZŐ_ADAT) ");

                sv = Console.ReadLine();

                if (sv != "0")

                {

                    temp.parameterek.Add(sv);

                }

            } while (sv != "0");

            do

            {

                Console.WriteLine("\nAz eszköz egy jellemzője, kérem a következő példa alapján:'Videó processzor: nVidia GeForce RTX 3060' (0-KÖVETKEZŐ_ADAT) ");

                sv = Console.ReadLine();

                if (sv != "0")

                {

                    temp.jellemzok.Add(sv);

                }

            } while (sv != "0");

            adatoks.Add(temp);

        }

        static void Torles(List<Adatok> adatoks, int index)
        {
          adatoks.RemoveAt(index-1);
            Mentes(adatoks);
        }

        static void Modositas(List<Adatok> adatoks)
        {
            Console.Clear();
            Console.WriteLine($"Mit szeretnél módosítani?");
            string[] lehetosegek = { "Név", "Ár", "Mennyiség", "Paraméterek", "Jellemzők"};
            int kivalasztott = 0;
            ConsoleKeyInfo lenyomott;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Válasszon az alábbi lehetőségek közül:\n");

                #region Menü kiírása
                for (int i = 0; i < lehetosegek.Length; i++)
                {
                    if (i == kivalasztott)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("\t" + (i + 1) + ") " + lehetosegek[i]);
                }
                #endregion

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < lehetosegek.Length - 1) kivalasztott++; break;
                }
                #endregion

            } while (true);
        }

        static void Mentes(List<Adatok> adatoks)
        {
            StreamWriter sw = new StreamWriter("informaciok.txt");

            for (int i = 0; i < adatoks.Count(); i++)
            {
                sw.Write($"{adatoks[i].nev};{adatoks[i].ar};{adatoks[i].db};");

                for (int j = 0; j <= adatoks[i].parameterek.Count() - 2; j++)
                {
                    sw.Write($"{adatoks[i].parameterek[j]}*");
                }

                sw.Write($"{adatoks[i].parameterek[adatoks[i].parameterek.Count() - 1]};");

                for (int j = 0; j <= adatoks[i].jellemzok.Count() - 2; j++)
                {
                    sw.Write($"{adatoks[i].jellemzok[j]}*");
                }

                sw.Write($"{adatoks[i].jellemzok[adatoks[i].jellemzok.Count() - 1]}\n");

            }

            sw.Close();

        }

        static void KeresoNev(List<Adatok> adatoks)
        {
            Console.Clear();
            Console.Title = "Termék kereső";
            Console.SetWindowSize(150, 43);
            Console.CursorSize = 20;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Adja meg a termék nevét: ");
            Console.ForegroundColor = ConsoleColor.White;

            string keres = Console.ReadLine();
            bool van = false;
            Console.Clear();
            for (int i = 0; i < adatoks.Count(); i++)
            {
                if (adatoks[i].nev.Contains(keres))
                {
                    van = true;
                    KilistazAr(adatoks, i);
                }
            }

            if (!van)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Nem található ilyen termék a rendszerben!");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }

        static void KeresoAr(List<Adatok> adatoks)
        {
            Console.Clear();
            Console.Title = "Termék kereső";
            Console.SetWindowSize(150, 43);
            Console.CursorSize = 20;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorLeft = 0;
            Console.Write("Adjon meg egy minimum árat: ");
            Console.ForegroundColor = ConsoleColor.White;
            string minArS = Console.ReadLine();
            int minAr;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorLeft = 0;
            Console.Write("Adjon meg egy maximum árat: ");
            Console.ForegroundColor = ConsoleColor.White;
            string maxArS = Console.ReadLine();
            int maxAr = 0;

            if (minArS == "")
            {
                minAr = 0;
            }
            else { minAr = int.Parse(minArS); }
            if (maxArS == "")
            {
                for (int i = 0; i < adatoks.Count(); i++)
                {
                    if (maxAr < adatoks[i].ar)
                    {
                        maxAr = adatoks[i].ar;
                    }
                }
            }
            else { maxAr = int.Parse(maxArS); }

            Console.Clear();
            Console.Title = "Eszközök " + minAr + "FT >-----< " + maxAr + "FT között";


            for (int i = 0; i < adatoks.Count(); i++)
            {
                if (minAr <= adatoks[i].ar && adatoks[i].ar <= maxAr)
                {
                    KilistazAr(adatoks, i);
                }
            }

        }

        static void KilistazAr(List<Adatok> adatoks, int index)
        {
            Console.SetWindowSize(150, 43);
            Console.CursorSize = 20;

            Console.CursorLeft = 0;
            Console.Write($"{adatoks[index].nev}");
            Console.CursorLeft = 90;
            Console.Write($"{adatoks[index].ar}FT\n");
            
        }
        static void Main(string[] args)
        {
            List<Adatok> adatoks = Beolvasas();
            bool orok = true;
            int kivalasztott = 0;
            string[] opciok = { "Kilistázás", "Új adat", "Módosítás", "Törlés", "Keresés", "Mentés" };
            #region Menü
            ConsoleKeyInfo lenyomott;

            do
            {
            MenuCommand:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Válasszon az alábbi opciók közül:\n");

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
                if (lenyomott.Key == ConsoleKey.Enter)
                {
                    if (kivalasztott == 0)
                    {
                        int bentkivalasztott = 0;
                        ConsoleKeyInfo bentlenyomott;

                        do
                        {
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
                                Console.SetWindowSize(150, 43);
                                Console.CursorSize = 20;
                                Console.CursorLeft = 0;
                                Console.Write($"{i + 1}) {adatoks[i].nev}");
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
                    else if (kivalasztott == 1)
                    {
                        Uj(adatoks);
                    }
                    else if (kivalasztott == 2)
                    {
                        Modositas(adatoks);
                    }
                    else if (kivalasztott == 3)
                    {
                    TorlesCommand:
                        Console.ForegroundColor = ConsoleColor.White;
                        KilistazTermek(adatoks);
                        Console.Write($"Adja meg hanyadik elemet kívánja kitörölni, vagy lépjen vissza a 'vissza' parancsal: ");
                        string torleshez = Console.ReadLine().ToUpper();
                        if (torleshez == "VISSZA")
                        {
                            goto MenuCommand;
                        }
                        else
                        {
                            int hanyadik = int.Parse(torleshez);
                            Torles(adatoks, hanyadik);
                            Mentes(adatoks);
                            goto TorlesCommand;
                        }

                    }
                    else if (kivalasztott == 4)
                    {
                        string[] keresesek = { "Keresés ár alapján", "Keresés név alapján", "Vissza" };
                        int valasztott = 0;
                        #region Menü
                        ConsoleKeyInfo bentlenyomott;

                        do
                        {

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Válasszon az alábbi keresésel közül:\n");

                            #region Menü kiírása
                            for (int i = 0; i < keresesek.Length; i++)
                            {
                                if (i == valasztott)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\t" + (i + 1) + ") " + keresesek[i]);
                            }
                            #endregion

                            #region Gomblenyomás

                            bentlenyomott = Console.ReadKey();

                            switch (bentlenyomott.Key)
                            {
                                case ConsoleKey.UpArrow: if (valasztott > 0) valasztott--; break;
                                case ConsoleKey.DownArrow: if (valasztott < keresesek.Length - 1) valasztott++; break;
                            }
                            #endregion

                            if (bentlenyomott.Key==ConsoleKey.Enter)
                            {
                                if(valasztott == 0)
                                {
                                    KeresoAr(adatoks);
                                    Console.Write($"Írja be hogy 'vissza', ha vissza szeretne lépni: ");
                                    string back = Console.ReadLine();
                                    if (back.ToUpper() == "VISSZA")
                                    {
                                        goto MenuCommand;
                                    }
                                }

                                else if (valasztott == 1)
                                {
                                    KeresoNev(adatoks);
                                    Console.Write($"Írja be hogy 'vissza', ha vissza szeretne lépni: ");
                                    string back = Console.ReadLine();
                                    if (back.ToUpper() == "VISSZA")
                                    {
                                        goto MenuCommand;
                                    }
                                }

                                else
                                {
                                    goto MenuCommand;
                                }
                            }
                           
                        } while (orok == true);
                        #endregion
                    }

                    else if (kivalasztott == 5)
                    {
                        Mentes(adatoks);
                        Console.Clear();
                        Console.WriteLine("Adatok elmentve");
                        Console.Write("Írd be azt hogy 'vissza' ha vissza szeretnél térni a főmenűbe: ");
                        string vissza = Console.ReadLine().ToUpper();
                        if (vissza == "VISSZA")
                        {
                            goto MenuCommand;
                        }
                    }
                }
            } while (orok==true);
            #endregion



        }
    }
}
