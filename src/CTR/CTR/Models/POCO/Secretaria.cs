using System.Collections.Generic;

namespace CTR.Models.POCO
{
    public class Secretaria
    {
        public int SecretariaId { get; set; }

        public string Nome { get; set; }

        public virtual ICollection<DescricaoVinculo> DescricaoVinculos { get; set; } = new List<DescricaoVinculo>();

        public virtual ICollection<Orgao> Orgaos { get; set; } = new List<Orgao>();

        public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();

        public virtual ICollection<Dotacao> Dotacoes { get; set; } = new List<Dotacao>();

        public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();

        public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}