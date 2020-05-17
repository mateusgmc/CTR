using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTR.Models.POCO
{
    public class DescricaoVinculo
    {
        public int DescricaoVinculoId { get; set; }

        public string Descricao { get; set; }

        public virtual Secretaria Secretaria { get; set; }
        public int SecretariaId { get; set; }
    }
}