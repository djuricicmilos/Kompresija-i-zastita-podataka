using System.Text;

namespace Projekat_1
{
    public class ShannonFano
    {
        #region promenljive

        List<Simbol> _simboli;

        #endregion

        public ShannonFano(List<Simbol> simboli)
        {
            this._simboli = simboli;
        }

        #region implementacija

        public void OdrediShannonFanoKodoveZaSimbole()
        {
            this._simboli.Sort((a, b) => b.Verovatnoca.CompareTo(a.Verovatnoca));
            this.PronadjiKodove(this._simboli);
        }

        public void PronadjiKodove(List<Simbol> lista)
        {
            if (lista.Count == 1)
            {
                return;
            }

            double ukupnaSuma = 0.0;
            foreach (var s in lista)
                ukupnaSuma += s.Verovatnoca;

            double minRazlika = double.MaxValue;
            int splitIndex = 0;
            double trenutnaSuma = 0.0;

            for (int i = 0; i < lista.Count - 1; i++) // lista.Count - 1 -> time garantuješ da desna grupa nikad nije prazna
            {
                trenutnaSuma += lista[i].Verovatnoca;
                double ostatak = ukupnaSuma - trenutnaSuma;

                double trenutnaRazlika = Math.Abs(trenutnaSuma - ostatak);

                if (trenutnaRazlika < minRazlika)
                {
                    minRazlika = trenutnaRazlika;
                    splitIndex = i;
                }
            }

            for (int i = 0; i < lista.Count; i++)
                lista[i].Kod += (i <= splitIndex) ? "0" : "1";

            PronadjiKodove(lista.GetRange(0, splitIndex + 1));
            PronadjiKodove(lista.GetRange(splitIndex + 1, lista.Count - splitIndex - 1));
        }

        public string KodirajPodatke(byte[] podaci)
        {
            StringBuilder binarniString = new StringBuilder();

            foreach (byte bajt in podaci)
            {
                for (int i = 0; i < this._simboli.Count; i++)
                {
                    if (bajt == this._simboli[i].Vrednost)
                    {
                        binarniString.Append(_simboli[i].Kod);
                        break;
                    }
                }
            }

            return binarniString.ToString();
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
