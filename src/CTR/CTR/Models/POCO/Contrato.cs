using System;

namespace CTR.Models.POCO
{
    public class Contrato
    {
        public int ContratoId { get; set; }


        private DateTime? _dataCadastro;
        public DateTime DataCadastro
        {
            get { return _dataCadastro ?? (DateTime.Now); }
            set { _dataCadastro = value; }
        }

        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        public virtual Secretaria Secretaria { get; set; }
        public int SecretariaId { get; set; }

        public virtual Orgao Orgao { get; set; }
        public int OrgaoId { get; set; }

        public virtual Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }

        public virtual Dotacao Dotacao { get; set; }
        public int DotacaoId { get; set; }

        public virtual DescricaoVinculo DescricaoVinculo { get; set; }
        public int DescricaoVinculoId { get; set; }

        public string Alocacao { get; set; }

        public virtual Cargo Cargo { get; set; }
        public int CargoId { get; set; }

        public virtual Jornada Jornada { get; set; }
        public int JornadaId { get; set; }

        public DateTime VigenciaInicio { get; set; }

        public DateTime VigenciaFim { get; set; }
    }
}
