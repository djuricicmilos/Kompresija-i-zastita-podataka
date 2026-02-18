using System.Text;

namespace Projekat_2
{
    public class Linearni_blok_kodovi
    {
        #region promenljive

        private int _n, _k;
        private int _wc, _wr;
        private int[,] H;
        private Dictionary<string, int[]> tabela;
        private int kodnoRastojanje;
        private Random rand;

        #endregion

        public Linearni_blok_kodovi(int n, int k, int wr, int wc)
        {
            if (n * wc != (n - k) * wr)
            {
                throw new ArgumentException("Parametri moraju da ispunjavaju uslov: n * wc == (n - k) * wr");
            }

            this._n = n;
            this._k = k;
            this._wr = wr;
            this._wc = wc;

            int r = _n - _k;
            H = new int[r, _n];
            rand = new Random(422021);

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    H[i, j] = 0;
                }
            }

            tabela = new Dictionary<string, int[]>();
            this.kodnoRastojanje = -1;
        }

        #region implementacija
        public int[,] GenerisiKontrolnuMatricuH()
        {
            int brojVrstaPoBloku = (_n - _k) / _wc;

            do
            {
                for (int i = 0; i < brojVrstaPoBloku; i++)
                {
                    for (int j = i * _wr; j < (i + 1) * _wr; j++)
                    {
                        H[i, j] = 1;
                    }
                }

                for (int blok = 1; blok < _wc; blok++)
                {
                    int pocetnaVrsta = blok * brojVrstaPoBloku;

                    int[] permutacija = KreirajPermutaciju();

                    for (int j = 0; j < _n; j++)
                    {
                        int kolona = permutacija[j];

                        for (int i = 0; i < brojVrstaPoBloku; i++)
                        {
                            H[i + pocetnaVrsta, j] = H[i, kolona];
                        }
                    }
                }
            }
            while (ImaDuplikatKolona(H));

            return H;
        }

        public int[] KreirajPermutaciju()
        {
            int[] permutacija = new int[_n];
            for (int i = 0; i < _n; i++)
            {
                permutacija[i] = i;
            }

            for (int i = _n - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);

                int temp = permutacija[i];
                permutacija[i] = permutacija[j];
                permutacija[j] = temp;
            }

            return permutacija;
        }

        public bool ImaDuplikatKolona(int[,] H)
        {
            int r = _n - _k;
            List<string> listaStringova = new List<string>();

            for (int j = 0; j < _n; j++)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < r; i++)
                {
                    sb.Append(H[i, j]);
                }

                for (int i = 0; i < listaStringova.Count; i++)
                {
                    if (listaStringova[i].Equals(sb.ToString()))
                    {
                        return true;
                    }
                }

                listaStringova.Add(sb.ToString());
            }

            return false;
        }

        public void StampajMatricu()
        {
            for (int i = 0; i < (_n - _k); i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    Console.Write(H[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        public Dictionary<string, int[]> GenerisiTabeluSindroma()
        {
            int r = _n - _k;
            int ukupanBrojSindroma = (int)Math.Pow(2, r);

            kodnoRastojanje = -1;

            for (int tezina = 0; tezina < _n; tezina++)
            {
                List<int[]> vektori = GenerisiVektore(tezina);

                foreach (int[] e in vektori)
                {
                    string sindrom = IzracunajSindrom(e);

                    if (sindrom.Equals(new string('0', r)) && tezina > 0 && kodnoRastojanje == -1)
                    {
                        kodnoRastojanje = tezina;
                    }

                    if (!tabela.ContainsKey(sindrom))
                    {
                        tabela.Add(sindrom, e);

                        if (tabela.Count == ukupanBrojSindroma)
                            return tabela;
                    }
                }
            }

            return tabela;
        }

        private string IzracunajSindrom(int[] e)
        {
            int r = _n - _k;
            int[] sindrom = new int[r];

            for (int i = 0; i < r; i++)
            {
                int zbir = 0;

                for (int j = 0; j < _n; j++)
                {
                    zbir += H[i, j] * e[j];
                }

                sindrom[i] = zbir % 2;
            }

            return string.Join("", sindrom);
        }

        public List<int[]> GenerisiVektore(int tezina)
        {
            List<int[]> rezultat = new List<int[]>();
            int[] vektor = new int[_n];

            for (int i = 0; i < _n; i++)
            {
                vektor[i] = 0;
            }

            GenerisiVektor_e(rezultat, vektor, 0, tezina);

            return rezultat;
        }

        public void GenerisiVektor_e(List<int[]> rezultat, int[] vektor, int pocetak, int tezina)
        {
            if (tezina == 0)
            {
                int[] pom = new int[_n];

                for (int i = 0; i < _n; i++)
                {
                    pom[i] = vektor[i];
                }

                rezultat.Add(pom);
                return;
            }

            for (int i = pocetak; i <= _n - tezina; i++)
            {
                vektor[i] = 1;
                GenerisiVektor_e(rezultat, vektor, i + 1, tezina - 1);
                vektor[i] = 0;
            }
        }

        public void StampajTabelu()
        {
            Console.WriteLine("Tabela sindroma:");
            foreach (var element in tabela)
            {
                Console.WriteLine("Sindrom: " + element.Key + " -> Korektor: " + string.Join("", element.Value));
            }
        }

        public int DajKodnoRastojanje()
        {
            return kodnoRastojanje;
        }

        #endregion
    }
}
