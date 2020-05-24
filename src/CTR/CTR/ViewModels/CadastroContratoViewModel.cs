using CTR.Infrastructure.Repository;
using CTR.Models.POCO;
using Prism.Commands;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CTR.ViewModels
{
    public class CadastroContratoViewModel : BaseViewModel
    {
        public CadastroContratoViewModel() : base(new Repository())
        {
            var contratos = Repository.GetAll<Contrato>();
            foreach (var contrato in contratos)
            {
                ContratosSalvos.Add(contrato);
            }

            var naturalidades = Repository.GetAll<Naturalidade>();
            foreach (var naturalidade in naturalidades)
            {
                Naturalidades.Add(naturalidade);
            }

            var estadosCivis = Repository.GetAll<EstadoCivil>();
            foreach (var estadoCivil in estadosCivis)
            {
                EstadosCivis.Add(estadoCivil);
            }
            EstadoCivilSelecionado = EstadosCivis.First();

            var estados = Repository.GetAll<Estado>();
            foreach (var estado in estados)
            {
                Estados.Add(estado);
            }

            var bairros = Repository.GetAll<Bairro>();
            foreach (var bairro in bairros)
            {
                Bairros.Add(bairro);
            }

            var cidades = Repository.GetAll<Cidade>();
            foreach (var cidade in cidades)
            {
                Cidades.Add(cidade);
            }

            var secretarias = Repository.GetAll<Secretaria>();
            foreach (var secretaria in secretarias)
            {
                Secretarias.Add(secretaria);
            }

            var jornadas = Repository.GetAll<Jornada>();
            foreach (var jornada in jornadas)
            {
                Jornadas.Add(jornada);
            }

            SalvarCommand = new DelegateCommand(SalvarExecute);


            this.WhenAnyValue(s => s.SecretariaSelecionada)
                .Subscribe(newSecretaria =>
                {
                    if (newSecretaria != null)
                    {
                        var descricaoVinculos = Repository.Get<DescricaoVinculo>(e => e.SecretariaId == newSecretaria.SecretariaId);
                        foreach (var descricaoVinculo in descricaoVinculos)
                        {
                            DescricaoVinculos.Add(descricaoVinculo);
                        }

                        var orgaos = Repository.Get<Orgao>(e => e.SecretariaId == newSecretaria.SecretariaId);
                        foreach (var orgao in orgaos)
                        {
                            Orgaos.Add(orgao);
                        }

                        var departamentos = Repository.Get<Departamento>(e => e.SecretariaId == newSecretaria.SecretariaId);
                        foreach (var departamento in departamentos)
                        {
                            Departamentos.Add(departamento);
                        }

                        var dotacoes = Repository.Get<Dotacao>(e => e.SecretariaId == newSecretaria.SecretariaId);
                        foreach (var dotacao in dotacoes)
                        {
                            Dotacoes.Add(dotacao);
                        }

                        var cargos = Repository.Get<Cargo>(e => e.SecretariaId == newSecretaria.SecretariaId);
                        foreach (var cargo in cargos)
                        {
                            Cargos.Add(cargo);
                        }
                    }
                });
        }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Rg { get; set; }

        public string OrgExp { get; set; }

        public EstadoCivil EstadoCivilSelecionado { get; set; }

        public ObservableCollection<EstadoCivil> EstadosCivis { get; } = new ObservableCollection<EstadoCivil>();

        public Naturalidade NaturalidadeSelecionada { get; set; }

        public ObservableCollection<Naturalidade> Naturalidades { get; } = new ObservableCollection<Naturalidade>();

        public Estado EstadoSelecionado { get; set; }
        public ObservableCollection<Estado> Estados { get; } = new ObservableCollection<Estado>();

        public string Endereco { get; set; }

        public Bairro BairroSelecionado { get; set; }
        public ObservableCollection<Bairro> Bairros { get; } = new ObservableCollection<Bairro>();

        public Cidade CidadeSelecionada { get; set; }
        public ObservableCollection<Cidade> Cidades { get; } = new ObservableCollection<Cidade>();

        public string Email { get; set; } = "@";

        public string Num { get; set; }

        public string Telefone { get; set; }

        public Jornada JornadaSelecionada { get; set; }
        public ObservableCollection<Jornada> Jornadas { get; } = new ObservableCollection<Jornada>();

        public Secretaria SecretariaSelecionada { get; set; }
        public ObservableCollection<Secretaria> Secretarias { get; } = new ObservableCollection<Secretaria>();

        public Orgao OrgaoSelecionado { get; set; }
        public ObservableCollection<Orgao> Orgaos { get; } = new ObservableCollection<Orgao>();

        public Departamento DepartamentoSelecionado { get; set; }
        public ObservableCollection<Departamento> Departamentos { get; } = new ObservableCollection<Departamento>();

        public Dotacao DotacaoSelecionado { get; set; }
        public ObservableCollection<Dotacao> Dotacoes { get; } = new ObservableCollection<Dotacao>();

        public DescricaoVinculo DescricaoVinculoSelecionado { get; set; }
        public ObservableCollection<DescricaoVinculo> DescricaoVinculos { get; } = new ObservableCollection<DescricaoVinculo>();

        public string Alocacao { get; set; }

        public decimal Salario { get => CargoSelecionado?.Salario ?? 0; }
        public Cargo CargoSelecionado { get; set; }
        public ObservableCollection<Cargo> Cargos { get; } = new ObservableCollection<Cargo>();

        public DateTime VigenciaInicio { get; set; } = DateTime.Now;

        public DateTime VigenciaFim { get; set; } = DateTime.Now;


        public ObservableCollection<Contrato> ContratosSalvos { get; } = new ObservableCollection<Contrato>();



        public DelegateCommand SalvarCommand { get; }



        private void SalvarExecute()
        {
            var funcionario = new Funcionario
            {
                Nome = Nome,
                Cpf = Cpf,
                Rg = Rg,
                OrgExp = OrgExp,
                EstadoCivil = EstadoCivilSelecionado,
                Naturalidade = NaturalidadeSelecionada,
                EstadoId = EstadoSelecionado.EstadoId,
                Endereco = Endereco,
                BairroId = BairroSelecionado.BairroId,
                CidadeId = CidadeSelecionada.CidadeId,
                Email = Email,
                Num = Num,
                Telefone = Telefone
            };

            var contrato = new Contrato
            {
                Funcionario = funcionario,
                SecretariaId = SecretariaSelecionada.SecretariaId,
                OrgaoId = OrgaoSelecionado.OrgaoId,
                DepartamentoId = DepartamentoSelecionado.DepartamentoId,
                DotacaoId = DotacaoSelecionado.DotacaoId,
                DescricaoVinculoId = DescricaoVinculoSelecionado.DescricaoVinculoId,
                Alocacao = Alocacao,
                CargoId = CargoSelecionado.CargoId,
                JornadaId = JornadaSelecionada.JornadaId,
                VigenciaInicio = VigenciaInicio,
                VigenciaFim = VigenciaFim
            };

            Repository.AddOrUpdate(contrato);

            if (!ContratosSalvos.Contains(contrato))
            {
                ContratosSalvos.Add(contrato);
            }
        }
    }
}
