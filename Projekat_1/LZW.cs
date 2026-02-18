namespace Projekat_1
{
    public class LZW
    {
        #region promenljive

        private int MAX_VELICINA_RECNIKA = 65536;

        #endregion

        #region implementacija

        public List<int> KodirajPodatke(byte[] ulaz)
        {
            Dictionary<string, int> recnik = new Dictionary<string, int>();

            for (int i = 0; i < 256; i++)
            {
                recnik.Add(((char)i).ToString(), i);
            }

            int sledeciKod = 256;

            List<int> kodiraniIzlaz = new List<int>();

            string rec = ((char)ulaz[0]).ToString();

            for (int i = 1; i < ulaz.Length; i++)
            {
                string karakter = ((char)ulaz[i]).ToString();
                string novaRec = rec + karakter;

                if (recnik.ContainsKey(novaRec))
                {
                    rec = novaRec;
                }
                else
                {
                    kodiraniIzlaz.Add(recnik[rec]);

                    if (sledeciKod < MAX_VELICINA_RECNIKA)
                    {
                        recnik.Add(novaRec, sledeciKod);

                        sledeciKod++;
                    }
                    else
                    {
                        recnik.Clear();

                        for (int j = 0; j < 256; j++)
                        {
                            recnik.Add(((char)j).ToString(), j);
                        }

                        sledeciKod = 256;
                    }

                    rec = karakter;
                }
            }

            if (!string.IsNullOrEmpty(rec))
            {
                kodiraniIzlaz.Add(recnik[rec]);
            }

            return kodiraniIzlaz;
        }

        public List<byte> DekodirajPodatke(List<int> kodovi)
        {
            Dictionary<int, string> recnik = new Dictionary<int, string>();

            for (int i = 0; i < 256; i++)
            {
                recnik.Add(i, ((char)i).ToString());
            }

            int sledeciKod = 256;

            string rec = recnik[kodovi[0]];

            List<byte> dekodiraniIzlaz = new List<byte>();

            foreach (char c in rec)
            {
                dekodiraniIzlaz.Add((byte)c);
            }

            for (int i = 1; i < kodovi.Count; i++)
            {
                string novaRec = null;

                if (recnik.ContainsKey(kodovi[i]))
                {
                    novaRec = recnik[kodovi[i]];
                }
                else if (kodovi[i] == sledeciKod)
                {
                    novaRec = rec + rec[0];
                }
                else
                {
                    throw new Exception("Trazis nepostojeci kljuc!");
                }

                foreach (char c in novaRec)
                {
                    dekodiraniIzlaz.Add((byte)c);
                }

                if (sledeciKod == MAX_VELICINA_RECNIKA)
                {
                    recnik.Clear();

                    for (int j = 0; j < 256; j++)
                    {
                        recnik.Add(j, ((char)j).ToString());
                    }

                    sledeciKod = 256;
                }
                else
                {
                    recnik.Add(sledeciKod, rec + novaRec[0]);

                    sledeciKod++;
                }

                rec = novaRec;
            }

            return dekodiraniIzlaz;
        }

        #endregion
    }
}
