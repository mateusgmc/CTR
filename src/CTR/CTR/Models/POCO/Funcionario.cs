namespace CTR.Models.POCO
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Rg { get; set; }

        public string OrgExp { get; set; }

        public string EstadoCivil { get; set; }

        public string Naturalidade { get; set; }

        public virtual Estado Estado { get; set; }
        public int EstadoId { get; set; }

        public string Endereco { get; set; }

        public virtual Bairro Bairro { get; set; }
        public int BairroId { get; set; }

        public virtual Cidade Cidade { get; set; }
        public int CidadeId { get; set; }

        public string Email { get; set; }

        public string Num { get; set; }

        public string Telefone { get; set; }
    }
}
