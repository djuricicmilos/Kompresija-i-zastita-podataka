namespace Projekat_1
{
    public class Cvor
    {
        public Simbol Simbol;
        public Cvor Levi { get; set; }
        public Cvor Desni { get; set; }

        public Cvor(Simbol simbol)
        {
            Simbol = simbol;
        }
    }
}
