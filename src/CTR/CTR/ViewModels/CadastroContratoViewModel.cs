using CTR.Infrastructure.Repository;
using CTR.Models.POCO;
using CTR.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace CTR.ViewModels
{
    public class CadastroContratoViewModel : ViewModelBase
    {
        public CadastroContratoViewModel(IRepository repository, DispatcherScheduler dispatcherScheduler)
            : base(repository, dispatcherScheduler)
        {
            // NOTE: Inicialização dos campos

            EstadoCivilSelecionado = EstadosCivis.First();
            NaturalidadeSelecionada = Naturalidades.First();

            // Carrega os estados do banco de maneira assíncrona
            Repository.GetAll<Estado>()
                .Busy(this)
                .ObserveOn(UiDispatcherScheduler)
                .Subscribe(estados =>
                {
                    foreach(var estado in estados)
                    {
                        Estados.Add(estado);
                    }
                },
                ex =>
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                });

            // Carrega os bairros do banco de maneira assíncrona
            Repository.GetAll<Bairro>()
                .Busy(this)
                .ObserveOn(UiDispatcherScheduler)
                .Subscribe(bairros =>
                {
                    foreach (var bairro in bairros)
                    {
                        Bairros.Add(bairro);
                    }
                },
                ex =>
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                });

            // Carrega os cidades do banco de maneira assíncrona
            Repository.GetAll<Cidade>()
                .Busy(this)
                .ObserveOn(UiDispatcherScheduler)
                .Subscribe(cidades =>
                {
                    foreach (var cidade in cidades)
                    {
                        Cidades.Add(cidade);
                    }
                });

            // Carrega as secretarias do banco de maneira assíncrona
            Repository.GetAll<Secretaria>()
                .Busy(this)
                .ObserveOn(UiDispatcherScheduler)
                .Subscribe(secretarias =>
                {
                    foreach (var secretaria in secretarias)
                    {
                        Secretarias.Add(secretaria);
                    }
                },
                ex =>
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                });

            // Carrega as jornadas do banco de maneira assíncrona
            Repository.GetAll<Jornada>()
                .Busy(this)
                .ObserveOn(UiDispatcherScheduler)
                .Subscribe(jornadas =>
                {
                    foreach (var jornada in jornadas)
                    {
                        Jornadas.Add(jornada);
                    }
                },
                ex =>
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                });


            // NOTE: Validação dos campos.
            // Para cada campo abaixo é criado um fluxo que verifica se o campo está preenchido. Caso não esteja
            // uma notificação de erro será propagada. O fluxo é acionado de maneira reativa, através da interação
            // do usuário na aplicação.

            var nomeHasErrors = this.WhenAnyValue(s => s.Nome, texto => string.IsNullOrEmpty(texto));
            ObservarErroCampoObrigatorio(nomeHasErrors, nameof(Nome));


            var cpfHasErros = this.WhenAnyValue(s => s.Cpf, texto => string.IsNullOrEmpty(texto));
            ObservarErroCampoObrigatorio(cpfHasErros, nameof(Cpf));


            var rgHasErros = this.WhenAnyValue(s => s.Rg, texto => string.IsNullOrEmpty(texto));
            ObservarErroCampoObrigatorio(rgHasErros, nameof(Rg));


            var orgExpHasErros = this.WhenAnyValue(s => s.OrgExp, texto => string.IsNullOrEmpty(texto));
            ObservarErroCampoObrigatorio(orgExpHasErros, nameof(OrgExp));


            var estadoCivilHasErros = this.WhenAnyValue(s => s.EstadoCivilSelecionado, texto => string.IsNullOrEmpty(texto));
            ObservarErroCampoObrigatorio(estadoCivilHasErros, nameof(EstadoCivilSelecionado));


            var naturalidadeHasErros = this.WhenAnyValue(s => s.NaturalidadeSelecionada, texto => string.IsNullOrEmpty(texto));
            ObservarErroCampoObrigatorio(naturalidadeHasErros, nameof(NaturalidadeSelecionada));


            var estadoHasErros = this.WhenAny(s => s.EstadoSelecionado, e => e is null);
            ObservarErroCampoObrigatorio(estadoHasErros, nameof(Estados));


            var enderecoHasErros = this.WhenAnyValue(s => s.Endereco, texto => string.IsNullOrEmpty(texto));
            ObservarErroCampoObrigatorio(estadoHasErros, nameof(Endereco));


            var bairroHasErros = this.WhenAny(s => s.BairroSelecionado, e => e is null);
            ObservarErroCampoObrigatorio(bairroHasErros, nameof(Bairros));


            var cidadeHasErros = this.WhenAny(s => s.CidadeSelecionada, e => e is null);
            ObservarErroCampoObrigatorio(cidadeHasErros, nameof(Cidades));


            var secretariaHasErros = this.WhenAny(s => s.SecretariaSelecionada, e => e is null);
            ObservarErroCampoObrigatorio(cidadeHasErros, nameof(Secretarias));


            var orgaoHasErros = this.WhenAny(s => s.OrgaoSelecionado, e => e is null);
            ObservarErroCampoObrigatorio(orgaoHasErros, nameof(Orgaos));


            var deparmentoHasErros = this.WhenAny(s => s.DepartamentoSelecionado, e => e is null);
            ObservarErroCampoObrigatorio(deparmentoHasErros, nameof(Departamentos));


            var dotacaoHasErros = this.WhenAny(s => s.DotacaoSelecionado, e => e is null);
            ObservarErroCampoObrigatorio(dotacaoHasErros, nameof(Dotacoes));


            var descricaoVinculoHasErros = this.WhenAny(s => s.DescricaoVinculoSelecionado, e => e is null);
            ObservarErroCampoObrigatorio(descricaoVinculoHasErros, nameof(DescricaoVinculos));


            var cargoHasErros = this.WhenAny(s => s.CargoSelecionado, e => e is null);
            ObservarErroCampoObrigatorio(cargoHasErros, nameof(Cargos));


            // Criamos um fluxo que é a combinação de todos os fluxos de validação acima.
            // Caso algum fluxo apresente o valor verdadeiro, isto é, caso algum fluxo notifique uma mensagem de erro,
            // este fluxo irá propagar uma notificação que fará com que o comando abaixo não possa ser executado.

            var salvarCanExecute = Observable.CombineLatest(
                    this.WhenAnyValue(s => s.IsBusy),
                    nomeHasErrors,
                    cpfHasErros,
                    rgHasErros,
                    orgExpHasErros,
                    estadoCivilHasErros,
                    naturalidadeHasErros,
                    estadoHasErros,
                    enderecoHasErros,
                    bairroHasErros,
                    cidadeHasErros,
                    secretariaHasErros,
                    orgaoHasErros,
                    deparmentoHasErros,
                    dotacaoHasErros,
                    descricaoVinculoHasErros,
                    cargoHasErros)
                .Select(observables => !observables.Any(r => r == true));

            SalvarCommand = ReactiveCommand.Create(SalvarExecute, salvarCanExecute);



            // Regras de negócio

            // Caso o cargo selecionado mude, alteramos o campo de salário
            this.WhenAnyValue(s => s.CargoSelecionado)
                .ToProperty(this, nameof(Salario));


            // Ao selecionar uma nova secretaria carregamos dados referentes a esta secretaria

            this.WhenAnyValue(s => s.SecretariaSelecionada)
                .Subscribe(newSecretaria =>
                {
                    if(newSecretaria != null)
                    {
                        Repository.Get<DescricaoVinculo>(e => e.SecretariaId == newSecretaria.SecretariaId)
                            .Busy(this)
                            .ObserveOn(UiDispatcherScheduler)
                            .Subscribe(descricaoVinculos =>
                            {
                                foreach(var descricaoVinculo in descricaoVinculos)
                                {
                                    DescricaoVinculos.Add(descricaoVinculo);
                                }
                                DescricaoVinculoSelecionado = DescricaoVinculos.First();
                            },
                            ex =>
                            {
                                System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                            });

                        Repository.Get<Orgao>(e => e.SecretariaId == newSecretaria.SecretariaId)
                            .Busy(this)
                            .ObserveOn(UiDispatcherScheduler)
                            .Subscribe(orgaos =>
                            {
                                foreach (var orgao in orgaos)
                                {
                                    Orgaos.Add(orgao);
                                }
                                OrgaoSelecionado = Orgaos.First();
                            },
                            ex =>
                            {
                                System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                            });

                        Repository.Get<Departamento>(e => e.SecretariaId == newSecretaria.SecretariaId)
                            .Busy(this)
                            .ObserveOn(UiDispatcherScheduler)
                            .Subscribe(departamentos =>
                            {
                                foreach (var departamento in departamentos)
                                {
                                    Departamentos.Add(departamento);
                                }
                                DepartamentoSelecionado = Departamentos.First();
                            },
                            ex =>
                            {
                                System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                            });

                        Repository.Get<Dotacao>(e => e.SecretariaId == newSecretaria.SecretariaId)
                            .Busy(this)
                            .ObserveOn(UiDispatcherScheduler)
                            .Subscribe(dotacoes =>
                            {
                                foreach (var dotacao in dotacoes)
                                {
                                    Dotacoes.Add(dotacao);
                                }
                                DotacaoSelecionado = Dotacoes.First();
                            },
                            ex =>
                            {
                                System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                            });
                    }
                });
        }

        [Reactive] public string Nome { get; set; }

        [Reactive] public string Cpf { get; set; }

        [Reactive] public string Rg { get; set; }

        [Reactive] public string OrgExp { get; set; }

        [Reactive] public string EstadoCivilSelecionado { get; set; }

        public ObservableCollection<string> EstadosCivis { get; } = new ObservableCollection<string>
        {
            "CASADO(A)",
            "SOLTEIRO(A)",
            "VIÚVO(A)",
            "UNIÃO ESTÁVEL",
            "DIVORCIADO(A)"
        };

        [Reactive] public string NaturalidadeSelecionada { get; set; }

        public ObservableCollection<string> Naturalidades { get; } = new ObservableCollection<string>
        {
            "BRASILEIRO NATO",
            "BRASILEIRO NATURALIZADO",
            "ESTRANGEIRO"
        };

        [Reactive] public Estado EstadoSelecionado { get; set; }
        public ObservableCollection<Estado> Estados { get; } = new ObservableCollection<Estado>();

        [Reactive] public string Endereco { get; set; }

        [Reactive] public Bairro BairroSelecionado { get; set; }
        public ObservableCollection<Bairro> Bairros { get; } = new ObservableCollection<Bairro>();

        [Reactive] public Cidade CidadeSelecionada { get; set; }
        public ObservableCollection<Cidade> Cidades { get; } = new ObservableCollection<Cidade>();

        [Reactive] public string Email { get; set; } = "@";

        [Reactive] public string Num { get; set; }

        [Reactive] public string Telefone { get; set; }

        [Reactive] public Jornada JornadaSelecionada { get; set; }
        public ObservableCollection<Jornada> Jornadas { get; } = new ObservableCollection<Jornada>();

        [Reactive] public Secretaria SecretariaSelecionada { get; set; }
        public ObservableCollection<Secretaria> Secretarias { get; } = new ObservableCollection<Secretaria>();

        [Reactive] public Orgao OrgaoSelecionado { get; set; }
        public ObservableCollection<Orgao> Orgaos { get; } = new ObservableCollection<Orgao>();

        [Reactive] public Departamento DepartamentoSelecionado { get; set; }
        public ObservableCollection<Departamento> Departamentos { get; } = new ObservableCollection<Departamento>();

        [Reactive] public Dotacao DotacaoSelecionado { get; set; }
        public ObservableCollection<Dotacao> Dotacoes { get; } = new ObservableCollection<Dotacao>();

        [Reactive] public DescricaoVinculo DescricaoVinculoSelecionado { get; set; }
        public ObservableCollection<DescricaoVinculo> DescricaoVinculos { get; } = new ObservableCollection<DescricaoVinculo>();

        [Reactive] public string Alocacao { get; set; }

        public decimal Salario { get => CargoSelecionado?.Salario ?? 0; }
        [Reactive] public Cargo CargoSelecionado { get; set; }
        public ObservableCollection<Cargo> Cargos { get; } = new ObservableCollection<Cargo>();

        [Reactive] public DateTime VigenciaInicio { get; set; } = DateTime.Now;

        [Reactive] public DateTime VigenciaFim { get; set; } = DateTime.Now;



        public ReactiveCommand<Unit, Unit> SalvarCommand { get; }



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

            Repository.Add<Contrato>(contrato)
                .Subscribe(c =>
                {
                    System.Windows.Forms.MessageBox.Show("Contrato salvo com sucesso!");
                },
                ex =>
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                });
        }
    }
}
