namespace Projekat_2
{
    public class Gallager_A_B_algoritam
    {
        #region promenljive

        private int[,] H;
        private int r, n;

        #endregion

        public Gallager_A_B_algoritam(int[,] H)
        {
            this.H = H;
            this.r = H.GetLength(0);
            this.n = H.GetLength(1);
        }

        #region implementacija

        public int[] Dekodiraj(int[] y, double th0, double th1, int maxIteracija)
        {
            if (DaLiJeKodnaRec(y))
            {
                return y;
            }

            int[] x = new int[n];
            int[] pomX = new int[n];

            for (int i = 0; i < n; i++)
            {
                x[i] = y[i];
            }

            for (int k = 0; k < maxIteracija; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    pomX[i] = 0;
                }

                for (int j = 0; j < n; j++)
                {
                    int brojGlasova_0 = 0;
                    int brojGlasova_1 = 0;
                    int brojSuseda = 0;

                    for (int i = 0; i < r; i++)
                    {
                        if (H[i, j] == 1)
                        {
                            brojSuseda++;

                            int zbir = 0;

                            for (int l = 0; l < n; l++)
                            {
                                if (l != j && H[i, l] == 1)
                                {
                                    zbir ^= x[l];
                                }
                            }

                            if (zbir == 0)
                            {
                                brojGlasova_0++;
                            }
                            else
                            {
                                brojGlasova_1++;
                            }
                        }
                    }

                    if (brojSuseda == 0)
                    {
                        pomX[j] = x[j];
                        continue;
                    }

                    if (brojGlasova_0 > th0 * brojSuseda)
                        pomX[j] = 0;
                    else if (brojGlasova_1 > th1 * brojSuseda)
                        pomX[j] = 1;
                    else
                        pomX[j] = x[j];
                }

                for (int i = 0; i < n; i++)
                {
                    x[i] = pomX[i];
                }

                if (DaLiJeKodnaRec(x))
                {
                    return x;
                }
            }

            return x;
        }

        public bool DaLiJeKodnaRec(int[] x)
        {
            for (int i = 0; i < r; i++)
            {
                int zbir = 0;

                for (int j = 0; j < n; j++)
                {
                    if (H[i, j] == 1)
                        zbir ^= x[j];
                }

                if (zbir != 0)
                    return false;
            }

            return true;
        }

        public int OdrediNajmanjiBrojJedinica(double th0, double th1, int maxIter)
        {
            for (int tezina = 1; tezina <= n; tezina++)
            {
                foreach (var e in GenerisiVektore(tezina))
                {
                    int[] rezultat = Dekodiraj(e, th0, th1, maxIter);

                    if (!DaLiJeKodnaRec(rezultat))
                    {
                        return tezina;
                    }
                }
            }

            return 0;
        }

        public List<int[]> GenerisiVektore(int tezina)
        {
            List<int[]> rezultat = new List<int[]>();
            int[] vector = new int[n];

            for (int i = 0; i < n; i++)
            {
                vector[i] = 0;
            }

            GenerisiVektor_e(rezultat, vector, 0, tezina);

            return rezultat;
        }

        public void GenerisiVektor_e(List<int[]> rezultat, int[] vektor, int pocetak, int tezina)
        {
            if (tezina == 0)
            {
                int[] pom = new int[n];

                for (int i = 0; i < n; i++)
                {
                    pom[i] = vektor[i];
                }

                rezultat.Add(pom);
                return;
            }

            for (int i = pocetak; i <= n - tezina; i++)
            {
                vektor[i] = 1;
                GenerisiVektor_e(rezultat, vektor, i + 1, tezina - 1);
                vektor[i] = 0;
            }
        }

        #endregion
    }
}
