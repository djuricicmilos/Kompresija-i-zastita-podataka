using System.Text;

namespace Projekat_1
{
    public class Manager
    {
        #region implementacija

        public byte[] UcitajPodatke(string nazivFajla)
        {
            return File.ReadAllBytes(nazivFajla);
        }

        public List<Simbol> UcitajSimbolе(byte[] podaci)
        {
            List<Simbol> simboli = new List<Simbol>();

            foreach (byte bajt in podaci)
            {
                Simbol? s = simboli.FirstOrDefault(x => x.Vrednost == bajt);

                if (s == null)
                {
                    Simbol simbol = new Simbol
                    {
                        Vrednost = bajt,
                        BrojPojavljivanja = 1
                    };

                    simboli.Add(simbol);
                }
                else
                {
                    s.BrojPojavljivanja++;
                }
            }

            int ukupnoBajtova = podaci.Length;

            foreach (var s in simboli)
            {
                s.Verovatnoca = (double)s.BrojPojavljivanja / ukupnoBajtova;
            }

            return simboli;
        }

        public byte[] SacuvajSimboleKaoBajtove(List<Simbol> simboli)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < simboli.Count; i++)
            {
                sb.Append(simboli[i].Vrednost + ":" + simboli[i].Kod + ";");
            }

            sb.Append("\n");

            byte[] simboliUBajtovima = Encoding.UTF8.GetBytes(sb.ToString());

            return simboliUBajtovima;
        }

        public void SacuvajKodiranePodatke(string fajl, byte[] bajtovi, byte[] metapodaci)
        {
            using (FileStream fs = new FileStream(fajl, FileMode.Create))
            {
                fs.Write(metapodaci, 0, metapodaci.Length);

                fs.Write(bajtovi, 0, bajtovi.Length);
            }
        }

        public void SacuvajDekodiranePodatke(string fajl, byte[] bajtovi)
        {
            using (FileStream fs = new FileStream(fajl, FileMode.Create))
            {
                fs.Write(bajtovi, 0, bajtovi.Length);
            }
        }

        public bool UporediFajlove(string fajl1, string fajl2)
        {
            byte[] f1 = File.ReadAllBytes(fajl1);
            byte[] f2 = File.ReadAllBytes(fajl2);

            if (f1.Length != f2.Length)
            {
                Console.WriteLine("Nisu iste duzine");
                Console.WriteLine("Fajl f1 ime duzinu " + f1.Length);
                Console.WriteLine("Fajl f2 ime duzinu " + f2.Length);
                return false;
            }

            for (int i = 0; i < f1.Length; i++)
            {
                if (f1[i] != f2[i])
                    return false;
            }

            return true;
        }

        public List<byte> KonvertujBinarniStringUBajtove(string kodiraniPodaci)
        {
            List<byte> bajtovi = new List<byte>();

            if (kodiraniPodaci.Length % 8 == 0)
            {
                bajtovi.Insert(0, (byte)8);
            }

            for (int i = 0; i < kodiraniPodaci.Length; i += 8)
            {
                string bajtStr = kodiraniPodaci.Substring(i, Math.Min(8, kodiraniPodaci.Length - i));
                if (bajtStr.Length < 8)
                {
                    int razlika = 8 - bajtStr.Length;
                    bajtovi.Insert(0, (byte)bajtStr.Length);

                    for (int j = 0; j < razlika; j++)
                    {
                        bajtStr += "0";
                    }
                }

                byte b = Convert.ToByte(bajtStr, 2);
                bajtovi.Add(b);
            }

            return bajtovi;
        }

        public string KonvertujBajtoveUBinarniString(byte[] podaci)
        {
            byte ostatak = podaci[0];

            StringBuilder bitovi = new StringBuilder();

            for (int i = 1; i < podaci.Length; i++)
            {
                bitovi.Append(Convert.ToString(podaci[i], 2).PadLeft(8, '0'));
            }

            string binarniString = bitovi.ToString();

            if (ostatak > 0 && ostatak < 8)
            {
                binarniString = binarniString.Substring(0, binarniString.Length - 8 + ostatak);
            }

            return binarniString;
        }

        public void RazdvojiPodatke(byte[] sviPodaci, out string metapodaci, out byte[] binarniPodaci)
        {
            int index = 0;

            List<byte> metaBajtovi = new List<byte>();

            while (index < sviPodaci.Length && sviPodaci[index] != (byte)'\n')
            {
                metaBajtovi.Add(sviPodaci[index]);
                index++;
            }

            metapodaci = Encoding.UTF8.GetString(metaBajtovi.ToArray());

            index++;

            int preostalo = sviPodaci.Length - index;
            binarniPodaci = new byte[preostalo];

            for (int i = 0; i < preostalo; i++)
            {
                binarniPodaci[i] = sviPodaci[index];
                index++;
            }
        }

        public List<Simbol> UcitajSimboleIzBajtova(string metapodaci)
        {
            List<Simbol> simboli = new List<Simbol>();

            metapodaci = metapodaci.Trim();
            string[] parovi = metapodaci.Split(';');

            foreach (string par in parovi)
            {
                par.Trim();
                string[] delovi = par.Split(':');

                if (delovi.Length != 2)
                {
                    continue;
                }

                byte vrednost = byte.Parse(delovi[0]);

                string kod = delovi[1];

                simboli.Add(new Simbol
                {
                    Vrednost = vrednost,
                    Kod = kod
                });
            }

            return simboli;
        }

        public void UpisiUBinarniFajl_LZ77(List<LZ77Token> podaci, string nazivFajla)
        {
            byte marker = (byte)0;

            using (FileStream fs = new FileStream(nazivFajla, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                foreach (var element in podaci)
                {
                    if (element.Fleg == marker)
                    {
                        writer.Write((byte)0); // Marker 0
                        writer.Write((byte)element.Duzina_Vrednost); // Karakter kao bajt (pretvaramo int u byte)
                    }
                    else // Postoji poklapanje
                    {
                        writer.Write((byte)1); // Marker 1
                        writer.Write((byte)element.Pomeraj); // Offset kao 2 bajta (ushort = 0-65535)
                        writer.Write((byte)element.Duzina_Vrednost); // Dužina kao 2 bajta
                    }
                }
            }
        }

        public List<LZ77Token> ProcitajIzBinarnogFajla_LZ77(string nazivFajla)
        {
            List<LZ77Token> rezultat = new List<LZ77Token>();
            byte marker = (byte)0;

            using (FileStream fs = new FileStream(nazivFajla, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    byte fleg = reader.ReadByte(); // Marker (1 bajt)

                    if (fleg == marker) // Nema poklapanja
                    {
                        byte character = reader.ReadByte(); // Karakter (1 bajt)

                        LZ77Token token = new LZ77Token
                        {
                            Fleg = 0,
                            Pomeraj = 0,
                            Duzina_Vrednost = (int)character
                        };

                        rezultat.Add(token);
                    }
                    else
                    {
                        byte offset = reader.ReadByte(); // Offset (1 bajt)
                        byte length = reader.ReadByte(); // Length (1 bajt)

                        LZ77Token token = new LZ77Token
                        {
                            Fleg = 1,
                            Pomeraj = (int)offset,
                            Duzina_Vrednost = (int)length
                        };

                        rezultat.Add(token);
                    }
                }
            }

            return rezultat;
        }

        public void UpisiUBinarniFajl_LZW(List<int> kodovi, string putanja)
        {
            using (FileStream fs = new FileStream(putanja, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                foreach (int kod in kodovi)
                {
                    writer.Write((ushort)kod); // 2 bajta
                }
            }
        }

        public List<int> ProcitajIzBinarnogFajla_LZW(string putanja)
        {
            List<int> kodovi = new List<int>();

            using (FileStream fs = new FileStream(putanja, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    kodovi.Add(reader.ReadUInt16());
                }
            }

            return kodovi;
        }

        public double IzracunajStepenKompresije(string putanjaDoOriginalnogFajla, string putanjaDoKompresovanogFajla)
        {
            FileInfo originalniFajl = new FileInfo(putanjaDoOriginalnogFajla);
            FileInfo kompresovaniFajl = new FileInfo(putanjaDoKompresovanogFajla);

            double velicinaOriginala = originalniFajl.Length;
            double velicinaKompresovanogFajla = kompresovaniFajl.Length;

            double stepenKompresije = 0.0;

            if (velicinaKompresovanogFajla != 0)
            {
                stepenKompresije = velicinaOriginala / velicinaKompresovanogFajla;
            }

            return stepenKompresije;
        }

        #endregion
    }
}
