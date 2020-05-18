using CTR.Infrastructure.Repository;
using CTR.Models.POCO;
using CTR.Utils;
using ReactiveUI;
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

            // Carrega os contratos do banco
            Repository.GetAll<Contrato>()
                .Busy(this)
                .Subscribe(contratos =>
                {
                    foreach (var contrato in contratos)
                    {
                        ContratosSalvos.Add(contrato);
                    }
                },
                ex =>
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                });

            // Carrega as naturalidades do banco de maneira assíncrona
            Repository.GetAll<Naturalidade>()
                .Busy(this)
                .Subscribe(naturalidades =>
                {
                    foreach (var naturalidade in naturalidades)
                    {
                        Naturalidades.Add(naturalidade);
                    }
                    NaturalidadeSelecionada = Naturalidades.First();
                },
                ex =>
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                });

            Repository.GetAll<EstadoCivil>()
                .Busy(this)
                .Subscribe(estadosCivis =>
                {
                    foreach (var estadoCivil in estadosCivis)
                    {
                        EstadosCivis.Add(estadoCivil);
                    }
                    EstadoCivilSelecionado = EstadosCivis.First();
                },
                ex =>
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                });

            // Carrega os estados do banco de maneira assíncrona
            Repository.GetAll<Estado>()
                .Busy(this)
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
                .Subscribe(cidades =>
                {
                    foreach (var cidade in cidades)
                    {
                        Cidades.Add(cidade);
                    }
                },
                ex =>
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                });

            // Carrega as secretarias do banco de maneira assíncrona
            Repository.GetAll<Secretaria>()
                .Busy(this)
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


            var enderecoHasErros = this.WhenAnyValue(s => s.Endereco, texto => string.IsNullOrEmpty(texto));
            ObservarErroCampoObrigatorio(enderecoHasErros, nameof(Endereco));


            var estadoCivilHasErros = this.WhenAny(s => s.EstadoCivilSelecionado, e => e.Value is null);


            var naturalidadeHasErros = this.WhenAny(s => s.NaturalidadeSelecionada, e => e.Value is null);


            var estadoHasErros = this.WhenAny(s => s.EstadoSelecionado, e => e.Value is null);


            var bairroHasErros = this.WhenAny(s => s.BairroSelecionado, e => e.Value is null);


            var cidadeHasErros = this.WhenAny(s => s.CidadeSelecionada, e => e.Value is null);


            var secretariaHasErros = this.WhenAny(s => s.SecretariaSelecionada, e => e.Value is null);


            var orgaoHasErros = this.WhenAny(s => s.OrgaoSelecionado, e => e.Value is null);


            var deparmentoHasErros = this.WhenAny(s => s.DepartamentoSelecionado, e => e.Value is null);


            var dotacaoHasErros = this.WhenAny(s => s.DotacaoSelecionado, e => e.Value is null);


            var descricaoVinculoHasErros = this.WhenAny(s => s.DescricaoVinculoSelecionado, e => e.Value is null);


            var cargoHasErros = this.WhenAny(s => s.CargoSelecionado, e => e.Value is null);

            var jornadaHasErros = this.WhenAny(s => s.JornadaSelecionada, e => e.Value is null);


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
                    cargoHasErros,
                    jornadaHasErros)
                .Select(observables => !observables.Any(r => r == true));

            SalvarCommand = ReactiveCommand.Create(SalvarExecute, salvarCanExecute);



            // Regras de negócio

            // Ao selecionar uma nova secretaria carregamos dados referentes a esta secretaria
            this.WhenAnyValue(s => s.SecretariaSelecionada)
                .Subscribe(newSecretaria =>
                {
                    if(newSecretaria != null)
                    {
                        Repository.Get<DescricaoVinculo>(e => e.SecretariaId == newSecretaria.SecretariaId)
                            .Busy(this)
                            .Subscribe(descricaoVinculos =>
                            {
                                foreach(var descricaoVinculo in descricaoVinculos)
                                {
                                    DescricaoVinculos.Add(descricaoVinculo);
                                }
                            },
                            ex =>
                            {
                                System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                            });

                        Repository.Get<Orgao>(e => e.SecretariaId == newSecretaria.SecretariaId)
                            .Busy(this)
                            .Subscribe(orgaos =>
                            {
                                foreach (var orgao in orgaos)
                                {
                                    Orgaos.Add(orgao);
                                }
                            },
                            ex =>
                            {
                                System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                            });

                        Repository.Get<Departamento>(e => e.SecretariaId == newSecretaria.SecretariaId)
                            .Busy(this)
                            .Subscribe(departamentos =>
                            {
                                foreach (var departamento in departamentos)
                                {
                                    Departamentos.Add(departamento);
                                }
                            },
                            ex =>
                            {
                                System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                            });

                        Repository.Get<Dotacao>(e => e.SecretariaId == newSecretaria.SecretariaId)
                            .Busy(this)
                            .Subscribe(dotacoes =>
                            {
                                foreach (var dotacao in dotacoes)
                                {
                                    Dotacoes.Add(dotacao);
                                }
                            },
                            ex =>
                            {
                                System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                            });

                        Repository.Get<Cargo>(e => e.SecretariaId == newSecretaria.SecretariaId)
                            .Busy(this)
                            .Subscribe(cargos =>
                            {
                                foreach (var cargo in cargos)
                                {
                                    Cargos.Add(cargo);
                                }
                            },
                            ex =>
                            {
                                System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                            });
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

            Repository.AddOrUpdate<Contrato>(contrato)
                .Busy(this)
                .Subscribe(e =>
                {
                    System.Windows.Forms.MessageBox.Show("Contrato salvo com sucesso!");
                    if (!ContratosSalvos.Contains(contrato))
                    {
                        ContratosSalvos.Add(contrato);
                    }
                },
                ex =>
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Erro");
                });
        }
    }
}
