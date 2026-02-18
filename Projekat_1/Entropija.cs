namespace Projekat_1
{
    public class Entropija
    {
        #region promenljive

        private List<Simbol> _simboli;

        #endregion

        public Entropija(List<Simbol> simboli)
        {
            this._simboli = simboli;
        }

        #region implementacija

        public double Izracunaj()
        {
            double entropija = 0.0;

            foreach (Simbol simbol in this._simboli)
            {
                if (simbol.Verovatnoca > 0)
                {
                    entropija -= simbol.Verovatnoca * Math.Log(simbol.Verovatnoca, 2);
                }
            }

            return entropija;
        }

        #endregion
    }
}
