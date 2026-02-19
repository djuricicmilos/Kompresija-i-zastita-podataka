using Projekat_1;

public class Program
{
    public static void Main(string[] args)
    {
        string putanjaDoPodataka = Path.Combine("fajlovi", "testFajl.txt");
        Manager manager = new Manager();

        byte[] podaciZaKodiranje = manager.UcitajPodatke(putanjaDoPodataka);

        // ##################################################################
        // ENTROPIJA
        // ##################################################################

        izracunajEntropiju(manager, podaciZaKodiranje);

        // ##################################################################
        // SHANNON-FANO ALGORITAM
        // ##################################################################

        ShannonFano_algoritam(manager, podaciZaKodiranje);

        // ##################################################################
        // Poredjenje fajlova
        // ##################################################################

        string putanjaShannonFanoKodirano = Path.Combine("fajlovi", "shannonFanoKodirano.bin");
        string putanjaShannonFanoDekodirano = Path.Combine("fajlovi", "shannonFanoDekodirano.bin");

        bool istiSu = manager.UporediFajlove(putanjaDoPodataka, putanjaShannonFanoDekodirano);

        if (istiSu)
        {
            Console.WriteLine("Jeej... Dekodiranje Shannon-Fano algoritmom je uspesno izvrseno.\n");
        }
        else
        {
            Console.WriteLine("Ups... Dekodiranje Shannon-Fano algoritmom nije uspesno izvrseno.\n");
        }

        // ##################################################################
        // stepenKompresije = velicinaOriginala / velicinaKompresovanogFajla
        // ##################################################################

        double stepenKompresije = manager.IzracunajStepenKompresije(putanjaDoPodataka, putanjaShannonFanoKodirano);
        Console.WriteLine("Stepen kompresije za Shannon-Fano algoritam je: " + stepenKompresije + "\n");

        // ##################################################################
        // HUFFMAN ALGORITAM
        // ##################################################################

        Huffman_algoritam(manager, podaciZaKodiranje);

        // ##################################################################
        // Poredjenje fajlova
        // ##################################################################

        string putanjaHuffmanKodirano = Path.Combine("fajlovi", "huffmanKodirano.bin");
        string putanjaHuffmanDekodirano = Path.Combine("fajlovi", "huffmanDekodirano.bin");

        istiSu = manager.UporediFajlove(putanjaDoPodataka, putanjaHuffmanDekodirano);

        if (istiSu)
        {
            Console.WriteLine("Jeej... Dekodiranje Huffman algoritmom je uspesno izvrseno.\n");
        }
        else
        {
            Console.WriteLine("Ups... Dekodiranje Huffman algoritmom nije uspesno izvrseno.\n");
        }

        // ##################################################################
        // stepenKompresije = velicinaOriginala / velicinaKompresovanogFajla
        // ##################################################################

        stepenKompresije = manager.IzracunajStepenKompresije(putanjaDoPodataka, putanjaHuffmanKodirano);
        Console.WriteLine("Stepen kompresije za Huffman algoritam je: " + stepenKompresije + "\n");

        // ##################################################################
        // LZ77 ALGORITAM
        // ##################################################################

        LZ77_algoritam(manager, podaciZaKodiranje);

        // ##################################################################
        // Poredjenje fajlova
        // ##################################################################

        string putanjaLZ77Kodirano = Path.Combine("fajlovi", "LZ77Kodirano.bin");
        string putanjaLZ77Dekodirano = Path.Combine("fajlovi", "LZ77Dekodirano.bin");

        istiSu = manager.UporediFajlove(putanjaDoPodataka, putanjaLZ77Dekodirano);

        if (istiSu)
        {
            Console.WriteLine("Jeej... Dekodiranje algoritmom LZ77 je uspesno izvrseno.\n");
        }
        else
        {
            Console.WriteLine("Ups... Dekodiranje algoritmom LZ77 nije uspesno izvrseno.\n");
        }

        // ##################################################################
        // stepenKompresije = velicinaOriginala / velicinaKompresovanogFajla
        // ##################################################################

        stepenKompresije = manager.IzracunajStepenKompresije(putanjaDoPodataka, putanjaLZ77Kodirano);
        Console.WriteLine("Stepen kompresije za LZ77 algoritam je: " + stepenKompresije + "\n");

        // ##################################################################
        // LZW ALGORITAM
        // ##################################################################

        LZW_algoritam(manager, podaciZaKodiranje);

        // ##################################################################
        // Poredjenje fajlova
        // ##################################################################

        string putanjaLZWKodirano = Path.Combine("fajlovi", "LZWKodirano.bin");
        string putanjaLZWDekodirano = Path.Combine("fajlovi", "LZWDekodirano.bin");

        istiSu = manager.UporediFajlove(putanjaDoPodataka, putanjaLZWDekodirano);

        if (istiSu)
        {
            Console.WriteLine("Jeej... Dekodiranje algoritmom LZW je uspesno izvrseno.\n");
        }
        else
        {
            Console.WriteLine("Ups... Dekodiranje algoritmom LZW nije uspesno izvrseno.\n");
        }

        // ##################################################################
        // stepenKompresije = velicinaOriginala / velicinaKompresovanogFajla
        // ##################################################################

        stepenKompresije = manager.IzracunajStepenKompresije(putanjaDoPodataka, putanjaLZWKodirano);
        Console.WriteLine("Stepen kompresije za LZW algoritam je: " + stepenKompresije + "\n");
    }

    public static void izracunajEntropiju(Manager manager, byte[] podaci)
    {
        List<Simbol> simboli = manager.UcitajSimbolе(podaci);

        Entropija entropija = new Entropija(simboli);
        double vrednostEntropije = entropija.Izracunaj();
        Console.WriteLine("Vrednost entropije je: " + vrednostEntropije + "\n");
    }

    public static void ShannonFano_algoritam(Manager manager, byte[] podaci)
    {
        List<Simbol> simboli = manager.UcitajSimbolе(podaci);

        ShannonFano shannonFano = new ShannonFano(simboli);

        string putanjaShannonFanoKodirano = Path.Combine("fajlovi", "shannonFanoKodirano.bin");
        string putanjaShannonFanoDekodirano = Path.Combine("fajlovi", "shannonFanoDekodirano.bin");

        Console.WriteLine("SHANNON-FANO ALGORITAM \n");

        // ##################################################################
        // Kodiranje Shannon-Fano algoritmom
        // ##################################################################

        shannonFano.OdrediShannonFanoKodoveZaSimbole();

        string kodiraniBinarniString = shannonFano.KodirajPodatke(podaci);

        List<byte> kodiraniBajtoviShannonFano = manager.KonvertujBinarniStringUBajtove(kodiraniBinarniString);

        byte[] simboliUBajtovima = manager.SacuvajSimboleKaoBajtove(simboli);

        manager.SacuvajKodiranePodatke(putanjaShannonFanoKodirano, kodiraniBajtoviShannonFano.ToArray(), simboliUBajtovima);

        // ##################################################################
        // Dekodiranje Shannon-Fano algoritmom
        // ##################################################################

        byte[] sviKodiraniBajtoviZaDekodiranje = manager.UcitajPodatke(putanjaShannonFanoKodirano);

        manager.RazdvojiPodatke(sviKodiraniBajtoviZaDekodiranje, out string metapodaci, out byte[] kodiraniBajtoviZaDekodiranje);

        string kodiraniBinarniStringZaDekodiranje = manager.KonvertujBajtoveUBinarniString(kodiraniBajtoviZaDekodiranje);

        List<Simbol> dekodiraniSimboli = manager.UcitajSimboleIzBajtova(metapodaci);

        List<byte> dekodiraniPodaciShannonFano = shannonFano.DekodirajPodatke(kodiraniBinarniStringZaDekodiranje, dekodiraniSimboli);

        manager.SacuvajDekodiranePodatke(putanjaShannonFanoDekodirano, dekodiraniPodaciShannonFano.ToArray());

        Console.WriteLine("Shannon-Fano simboli:");
        string simboliIspis = shannonFano.ToString();
        Console.WriteLine(simboliIspis);
    }

    public static void Huffman_algoritam(Manager manager, byte[] podaci)
    {
        List<Simbol> simboli = manager.UcitajSimbolе(podaci);

        Huffman huffman = new Huffman(simboli);

        string putanjaHuffmanKodirano = Path.Combine("fajlovi", "huffmanKodirano.bin");
        string putanjaHuffmanDekodirano = Path.Combine("fajlovi", "huffmanDekodirano.bin");

        Console.WriteLine("HUFFMAN ALGORITAM \n");

        // ##################################################################
        // Kodiranje Huffman algoritmom
        // ##################################################################

        Cvor koren = huffman.KreirajHuffmanStablo();

        huffman.OdrediHuffmanKodove(koren, "");

        string kodiraniBinarniString = huffman.KodirajFajl(podaci);

        List<byte> kodiraniBajtoviHuffman = manager.KonvertujBinarniStringUBajtove(kodiraniBinarniString);

        byte[] simboliUBajtovima = manager.SacuvajSimboleKaoBajtove(simboli);

        manager.SacuvajKodiranePodatke(putanjaHuffmanKodirano, kodiraniBajtoviHuffman.ToArray(), simboliUBajtovima);

        // ##################################################################
        // Dekodiranje Huffman algoritmom
        // ##################################################################

        byte[] sviKodiraniBajtoviZaDekodiranje = manager.UcitajPodatke(putanjaHuffmanKodirano);

        manager.RazdvojiPodatke(sviKodiraniBajtoviZaDekodiranje, out string metapodaci, out byte[] kodiraniBajtoviZaDekodiranje);

        string kodiraniBinarniStringZaDekodiranje = manager.KonvertujBajtoveUBinarniString(kodiraniBajtoviZaDekodiranje);

        List<Simbol> dekodiraniSimboli = manager.UcitajSimboleIzBajtova(metapodaci);

        List<byte> dekodiraniPodaciHuffman = huffman.DekodirajPodatke(kodiraniBinarniStringZaDekodiranje, dekodiraniSimboli);

        manager.SacuvajDekodiranePodatke(putanjaHuffmanDekodirano, dekodiraniPodaciHuffman.ToArray());

        Console.WriteLine("Huffman simboli:");
        string simboliIspis = huffman.ToString();
        Console.WriteLine(simboliIspis);
    }

    public static void LZ77_algoritam(Manager manager, byte[] podaci)
    {
        LZ77 LZ77 = new LZ77(4);

        string putanjaLZ77Kodirano = Path.Combine("fajlovi", "LZ77Kodirano.bin");
        string putanjaLZ77Dekodirano = Path.Combine("fajlovi", "LZ77Dekodirano.bin");

        Console.WriteLine("LZ77 ALGORITAM \n");

        // ##################################################################
        // Kodiranje LZ77 algoritmom
        // ##################################################################

        List<LZ77Token> izlaz = LZ77.KodirajFajl(podaci);

        //string ispis = "";

        //for (int i = 0; i < izlaz.Count; i++)
        //{
        //    if (izlaz[i].Fleg == 0)
        //    {
        //        ispis += "Fleg: " + izlaz[i].Fleg + ", vrednost: " + izlaz[i].Duzina_Vrednost + "\n";
        //    }
        //    else
        //    {
        //        ispis += "Fleg: " + izlaz[i].Fleg + ", pomeraj: " + izlaz[i].Pomeraj + ", duzina: " + izlaz[i].Duzina_Vrednost + "\n";
        //    }
        //}

        //Console.WriteLine(ispis);

        manager.UpisiUBinarniFajl_LZ77(izlaz, putanjaLZ77Kodirano);

        // ##################################################################
        // Dekodiranje LZ77 algoritmom
        // ##################################################################

        List<LZ77Token> ulaz = manager.ProcitajIzBinarnogFajla_LZ77(putanjaLZ77Kodirano);

        List<byte> dekodiraniPodaciLZ77 = LZ77.DekodirajPodatke(ulaz);

        manager.SacuvajDekodiranePodatke(putanjaLZ77Dekodirano, dekodiraniPodaciLZ77.ToArray());
    }

    public static void LZW_algoritam(Manager manager, byte[] podaci)
    {
        LZW LZW = new LZW();

        string putanjaLZWKodirano = Path.Combine("fajlovi", "LZWKodirano.bin");
        string putanjaLZWDekodirano = Path.Combine("fajlovi", "LZWDekodirano.bin");

        Console.WriteLine("LZW ALGORITAM \n");

        // ##################################################################
        // Kodiranje LZW algoritmom
        // ##################################################################

        List<int> enkodiraniPodaci = LZW.KodirajPodatke(podaci);

        manager.UpisiUBinarniFajl_LZW(enkodiraniPodaci, putanjaLZWKodirano);

        // ##################################################################
        // Dekodiranje LZW algoritmom
        // ##################################################################

        List<int> kodovi = manager.ProcitajIzBinarnogFajla_LZW(putanjaLZWKodirano);

        List<byte> dekodiraniPodaciLZW = LZW.DekodirajPodatke(kodovi);

        if (dekodiraniPodaciLZW != null)
        {
            manager.SacuvajDekodiranePodatke(putanjaLZWDekodirano, dekodiraniPodaciLZW.ToArray());
        }
    }
}
