using Projekat_2;

public class Test
{
    public static void Main(string[] args)
    {
        int n, k, wr, wc;

        Console.Write("Unesi duzinu kodne reci (n):");
        string unos = Console.ReadLine();
        n = int.Parse(unos);

        Console.Write("Unesi duzinu bitova parnosti (k):");
        unos = Console.ReadLine();
        k = int.Parse(unos);

        Console.Write("Unesi broj jedinica u vrsti (wr):");
        unos = Console.ReadLine();
        wr = int.Parse(unos);

        Console.Write("Unesi broj jedinica u koloni (wc):");
        unos = Console.ReadLine();
        wc = int.Parse(unos);

        try
        {
            Linearni_blok_kodovi linearniBlokKodovi = new Linearni_blok_kodovi(n, k, wr, wc);

            // #####################################################################
            // Matrica H
            // #####################################################################

            int[,] H = linearniBlokKodovi.GenerisiKontrolnuMatricuH();

            linearniBlokKodovi.StampajMatricu();

            linearniBlokKodovi.GenerisiTabeluSindroma();

            linearniBlokKodovi.StampajTabelu();

            Console.WriteLine();

            // #####################################################################
            // Kodno rastojanje
            // #####################################################################

            int kodnoRastojanje = linearniBlokKodovi.DajKodnoRastojanje();

            Console.WriteLine("Najmanje kodno rastojanje je: " + kodnoRastojanje + "\n");

            // #####################################################################
            // Gallager B algoritam
            // #####################################################################

            Console.WriteLine("\n <- Gallager B algoritam -> \n");

            double th0 = 0.5;
            double th1 = 0.5;

            Gallager_A_B_algoritam algoritam = new Gallager_A_B_algoritam(H);

            int[] y = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            int[] rezultat = algoritam.Dekodiraj(y, th0, th1, 20);

            Console.Write("Gallager B za pristiglu rec: ");
            for (int i = 0; i < y.Length; i++)
            {
                Console.Write(y[i]);
            }

            Console.Write(", je pronasao kodnu rec: ");

            for (int i = 0; i < n; i++)
            {
                Console.Write(rezultat[i]);

                if (i == n - 1)
                {
                    Console.WriteLine();
                }
            }

            // #####################################################################
            // Najmanji broj jedinica koje Gallager B ne uspeva da ispravi
            // #####################################################################

            int najmanja_tezina = algoritam.OdrediNajmanjiBrojJedinica(th0, th1, 20);

            Console.WriteLine($"Najmanji broj jedinica tako da Gallager B algoritam ne uspeva da ispravi sve greske je = " + najmanja_tezina + "\n");

            // #####################################################################
            // Detektovanje i ispravljanje kodova
            // #####################################################################

            int te = kodnoRastojanje - 1;
            Console.WriteLine("Gallager B uspeva da maksimalno detektuje " + te + " greske. \n");

            int t = (kodnoRastojanje - 1) / 2;
            Console.WriteLine("Gallager B uspeva maksimalno da ispravi " + t + " greske. \n");

            if (najmanja_tezina <= t)
            {
                Console.WriteLine("Gallager B algoritam nije optimalan. Losiji je od teorijske granice. \n");
            }
            else
            {
                Console.WriteLine("Gallager B algoritam dostize teorijsku granicu. \n");
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}