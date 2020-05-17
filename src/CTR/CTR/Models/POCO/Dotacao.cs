namespace CTR.Models.POCO
{
    public class Dotacao
    {
        public int DotacaoId { get; set; }

        public string Descricao { get; set; }

        public int SecretariaId { get; set; }
        public virtual Secretaria Secretaria { get; set; }
    }
}