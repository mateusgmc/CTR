using System.Collections.Generic;

namespace CTR.Models.POCO
{
    public class EstadoCivil
    {
        public int EstadoCivilId { get; set; }

        public string Descricao { get; set; }

        public IEnumerable<Funcionario> Funcionarios { get; set; }
    }
}