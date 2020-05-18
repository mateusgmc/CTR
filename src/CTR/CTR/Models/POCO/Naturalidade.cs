using System.Collections.Generic;

namespace CTR.Models.POCO
{
    public class Naturalidade
    {
        public int NaturalidadeId { get; set; }

        public string Descricao { get; set; }

        public IEnumerable<Funcionario> Funcionarios { get; set; }
    }
}