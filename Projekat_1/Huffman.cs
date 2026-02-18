using System.Text;

namespace Projekat_1
{
    public class Huffman
    {
        #region promenljive

        private List<Simbol> _simboli;

        #endregion

        public Huffman(List<Simbol> simboli)
        {
            this._simboli = simboli;
        }

        #region implementacija

        public Cvor KreirajHuffmanStablo()
        {
            List<Cvor> cvorovi = new List<Cvor>();

            for (int i = 0; i < this._simboli.Count; i++)
            {
                Cvor noviCvor = new Cvor(this._simboli[i]);

                cvorovi.Add(noviCvor);
            }

            while (cvorovi.Count > 1)
            {
                cvorovi.Sort((a, b) => a.Simbol.Verovatnoca.CompareTo(b.Simbol.Verovatnoca));

                Cvor levi = cvorovi[0];
                Cvor desni = cvorovi[1];

                cvorovi.RemoveAt(0);
                cvorovi.RemoveAt(0);

                Simbol noviSimbol = new Simbol
                {
                    Verovatnoca = levi.Simbol.Verovatnoca + desni.Simbol.Verovatnoca,
                };
                Cvor noviCvor = new Cvor(noviSimbol);
                noviCvor.Levi = levi;
                noviCvor.Desni = desni;

                cvorovi.Add(noviCvor);
            }

            return cvorovi[0];
        }

        public void OdrediHuffmanKodove(Cvor trenutniCvor, string trenutniKod)
        {
            if (trenutniCvor == null)
            {
                return;
            }

            if (trenutniCvor.Levi == null && trenutniCvor.Desni == null)
            {
                trenutniCvor.Simbol.Kod = trenutniKod;
                return;
            }

            OdrediHuffmanKodove(trenutniCvor.Levi, trenutniKod + "0");
            OdrediHuffmanKodove(trenutniCvor.Desni, trenutniKod + "1");
        }

        public string KodirajFajl(byte[] podaci)
        {
            StringBuilder kodiraniString = new StringBuilder();

            for (int i = 0; i < podaci.Length; i++)
            {
                for (int j = 0; j < this._simboli.Count; j++)
                {
                    if (this._simboli[j].Vrednost == podaci[i])
                    {
                        kodiraniString.Append(this._simboli[j].Kod);
                    }
                }
            }

            return kodiraniString.ToString();
        }

        public List<byte> DekodirajPodatke(string kodiraniPodaci, List<Simbol> dekodiraniSimboli)
        {
            List<byte> dekodiraniPodaci = new List<byte>();
            string trenutniKod = "";

            foreach (char bit in kodiraniPodaci)
            {
                trenutniKod += bit;

                foreach (var simbol in dekodiraniSimboli)
                {
                    if (trenutniKod == simbol.Kod)
                    {
                        dekodiraniPodaci.Add(simbol.Vrednost);
                        trenutniKod = "";
                        break;
                    }
                }
            }

            return dekodiraniPodaci;
        }

        override
        public string ToString()
        {
            string ispis = "";

            for (int i = 0; i < this._simboli.Count; i++)
            {
                if (this._simboli[i] == null)
                {
                    break;
                }

                ispis += "Vrednost: " + this._simboli[i].Vrednost + ", kod: " + this._simboli[i].Kod + "\n";
            }

            return ispis;
        }

        #endregion
    }
}
