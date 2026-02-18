namespace Projekat_1
{
    public class LZ77
    {
        #region promenljive

        private int _duzinaProzora;

        #endregion

        public LZ77(int duzinaProzora)
        {
            this._duzinaProzora = duzinaProzora;
        }

        #region implementacija

        public List<LZ77Token> KodirajFajl(byte[] ulaz)
        {
            List<LZ77Token> izlaz = new List<LZ77Token>();
            int i = 0;

            while (i < ulaz.Length)
            {
                (int pomeraj, int duzinaPoklapanja) = NadjiNajduzePoklapanje(ulaz, i);

                if (duzinaPoklapanja > 0)
                {
                    int duzina = Math.Min(duzinaPoklapanja, 255);

                    LZ77Token token = new LZ77Token
                    {
                        Fleg = (byte)1,
                        Pomeraj = pomeraj,
                        Duzina_Vrednost = duzina
                    };

                    izlaz.Add(token);
                    i += duzina;
                }
                else
                {
                    LZ77Token token = new LZ77Token
                    {
                        Fleg = (byte)0,
                        Pomeraj = 0,
                        Duzina_Vrednost = ulaz[i]
                    };

                    izlaz.Add(token);
                    i++;
                }
            }

            return izlaz;
        }

        private (int offset, int length) NadjiNajduzePoklapanje(byte[] podaci, int trenutnaPozicija)
        {
            int maxMatchLength = 0;
            int maxMatchOffset = 0;

            int start = Math.Max(0, trenutnaPozicija - _duzinaProzora);

            for (int j = start; j < trenutnaPozicija; j++)
            {
                int matchLength = 0;

                while (trenutnaPozicija + matchLength < podaci.Length && podaci[j + matchLength] == podaci[trenutnaPozicija + matchLength])
                {
                    matchLength++;
                }

                if (matchLength > maxMatchLength)
                {
                    maxMatchLength = matchLength;
                    maxMatchOffset = trenutnaPozicija - j;
                }
            }

            return (maxMatchOffset, maxMatchLength);
        }

        public List<byte> DekodirajPodatke(List<LZ77Token> ulaz)
        {
            List<byte> dekompresovaniPodaci = new List<byte>();

            foreach (var element in ulaz)
            {
                if (element.Fleg == 0)
                {
                    byte vrednost = (byte)element.Duzina_Vrednost;
                    dekompresovaniPodaci.Add(vrednost);
                }
                else
                {
                    int startIndex = dekompresovaniPodaci.Count - element.Pomeraj;

                    for (int i = 0; i < element.Duzina_Vrednost; i++)
                    {
                        byte vrednost = dekompresovaniPodaci[startIndex + i];
                        dekompresovaniPodaci.Add(vrednost);
                    }
                }
            }

            return dekompresovaniPodaci;
        }

        #endregion
    }
}
