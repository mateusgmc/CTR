namespace CTR.Models.POCO
{
    public class Jornada
    {
        public int JornadaId { get; set; }

        public decimal Horas { get; set; }

        public virtual TipoJornada TipoJornada { get; set; }
        public int TipoJornadaId { get; set; }
    }
}