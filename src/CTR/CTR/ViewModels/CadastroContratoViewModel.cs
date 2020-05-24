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
        // Isso é um construtor, presente em qualquer linguagem de programação.
        // Todo o código escrito aqui será chamado SOMENTE UMA VEZ durante a inicialização da janela
        public CadastroContratoViewModel() : base(new Repository())
        {
            // Carregamos todos os Contratos gravados no banco de dados. isso é similar a um "SELECT * FROM Contrato";
            var contratos = Repository.GetAll<Contrato>();
            // Populamos o combobox com os contatos recuperados
            foreach (var contrato in contratos)
            {
                ContratosSalvos.Add(contrato);
            }

            // Carregamos todas as Naturalidades gravados no banco de dados. isso é similar a um "SELECT * FROM Naturalidade";
            var naturalidades = Repository.GetAll<Naturalidade>();
            // Populamos o combobox com as naturalidades recuperados
            foreach (var naturalidade in naturalidades)
            {
                Naturalidades.Add(naturalidade);
            }

            // Carregamos todas os estados civis gravados no banco de dados. isso é similar a um "SELECT * FROM EstadoCivil";
            var estadosCivis = Repository.GetAll<EstadoCivil>();
            // Populamos o combobox com os estadosCivis recuperados
            foreach (var estadoCivil in estadosCivis)
            {
                EstadosCivis.Add(estadoCivil);
            }
            // Aqui definimos que inicialmente o estado civil selecionado será o primeiro elemento do combobox
            EstadoCivilSelecionado = EstadosCivis.First();

            // Carregamos todas os Estados gravados no banco de dados. isso é similar a um "SELECT * FROM Estado";
            var estados = Repository.GetAll<Estado>();
            // Populamos o combobox com os estados recuperados
            foreach (var estado in estados)
            {
                Estados.Add(estado);
            }

            // Carregamos todas os Bairros gravados no banco de dados. isso é similar a um "SELECT * FROM Bairro";
            var bairros = Repository.GetAll<Bairro>();
            // Populamos o combobox com os bairros recuperados
            foreach (var bairro in bairros)
            {
                Bairros.Add(bairro);
            }

            // Carregamos todas as Cidades gravados no banco de dados. isso é similar a um "SELECT * FROM Cidade";
            var cidades = Repository.GetAll<Cidade>();
            // Populamos o combobox com as cidades recuperados
            foreach (var cidade in cidades)
            {
                Cidades.Add(cidade);
            }

            // Carregamos todas as Secretarias gravados no banco de dados. isso é similar a um "SELECT * FROM Secretaria";
            var secretarias = Repository.GetAll<Secretaria>();
            // Populamos o combobox com as secretarias recuperados
            foreach (var secretaria in secretarias)
            {
                Secretarias.Add(secretaria);
            }

            // Carregamos todas as Jornadas gravados no banco de dados. isso é similar a um "SELECT * FROM Jornada";
            var jornadas = Repository.GetAll<Jornada>();
            // Populamos o combobox com as jornadas recuperados
            foreach (var jornada in jornadas)
            {
                Jornadas.Add(jornada);
            }

            // Criamos um comando para o botão de Salvar. O método "SalvarExecute" será chamado sempre que o usuário acionar o botão
            SalvarCommand = new DelegateCommand(SalvarExecute);


            // Criamos uma rotina que será executada sempre que a secretaria selecionada mudar.
            // Diferente dos demais códigos escritos no construtor, esse código pode ser chamado mais de uma vez.
            this.WhenAnyValue(s => s.SecretariaSelecionada)
                .Subscribe(newSecretaria =>
                {
                    // Atenção: O código de todo esse bloco será executado sempre que uma nova secretaria for selecionada.
                    // "newSecretaria" representa a nova secretaria selecionada pelo usuário.


                    // Caso a nova secretaria selecionada não seja null, isto é, não seja uma secretária inválida
                    if (newSecretaria != null)
                    {
                        // Carregamos todas as descrições associadas a nova secretaria selecionada. isso é similar a um "SELECT * FROM DescricaoVinculo WHERE SecretariaId = @newSecretariaId";
                        var descricaoVinculos = Repository.Get<DescricaoVinculo>(e => e.SecretariaId == newSecretaria.SecretariaId);
                        // Populamos o combobox com as descricaoVinculos recuperados
                        foreach (var descricaoVinculo in descricaoVinculos)
                        {
                            DescricaoVinculos.Add(descricaoVinculo);
                        }

                        // Carregamos todos os orgãos associados a nova secretaria selecionada. isso é similar a um "SELECT * FROM Orgao WHERE SecretariaId = @newSecretariaId";
                        var orgaos = Repository.Get<Orgao>(e => e.SecretariaId == newSecretaria.SecretariaId);
                        // Populamos o combobox com os orgaos recuperados
                        foreach (var orgao in orgaos)
                        {
                            Orgaos.Add(orgao);
                        }

                        // Carregamos todos os departamentos associados a nova secretaria selecionada. isso é similar a um "SELECT * FROM Departamento WHERE SecretariaId = @newSecretariaId";
                        var departamentos = Repository.Get<Departamento>(e => e.SecretariaId == newSecretaria.SecretariaId);
                        // Populamos o combobox com os departamentos recuperados
                        foreach (var departamento in departamentos)
                        {
                            Departamentos.Add(departamento);
                        }

                        // Carregamos todos as dotações associadas a nova secretaria selecionada. isso é similar a um "SELECT * FROM Dotacao WHERE SecretariaId = @newSecretariaId";
                        var dotacoes = Repository.Get<Dotacao>(e => e.SecretariaId == newSecretaria.SecretariaId);
                        // Populamos o combobox com os departamentos recuperados
                        foreach (var dotacao in dotacoes)
                        {
                            Dotacoes.Add(dotacao);
                        }

                        // Carregamos todos os Cargos associados a nova secretaria selecionada. isso é similar a um "SELECT * FROM Cargo WHERE SecretariaId = @newSecretariaId";
                        var cargos = Repository.Get<Cargo>(e => e.SecretariaId == newSecretaria.SecretariaId);
                        // Populamos o combobox com os cargos recuperados
                        foreach (var cargo in cargos)
                        {
                            Cargos.Add(cargo);
                        }
                    }
                });
        }


        // Essa propriedade é um atalho para o texto da text box do Nome. Tudo que acontecer nessa propriedade é refletida de volta para a text box e vice-versa.
        public string Nome { get; set; }


        // Essa propriedade é um atalho para o texto da text box do Cpf. Tudo que acontecer nessa propriedade é refletida de volta para a text box e vice-versa.
        public string Cpf { get; set; }


        // Essa propriedade é um atalho para o texto da text box do Rg. Tudo que acontecer nessa propriedade é refletida de volta para a text box e vice-versa.
        public string Rg { get; set; }


        // Essa propriedade é um atalho para o texto da text box do OrgExp. Tudo que acontecer nessa propriedade é refletida de volta para a text box e vice-versa.
        public string OrgExp { get; set; }


        // Essa propriedade é um atalho para o item selecionado do combobox do estado civil. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public EstadoCivil EstadoCivilSelecionado { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox do estado civil. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<EstadoCivil> EstadosCivis { get; } = new ObservableCollection<EstadoCivil>();


        // Essa propriedade é um atalho para o item selecionado do combobox de naturalidades. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public Naturalidade NaturalidadeSelecionada { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de naturalidades. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<Naturalidade> Naturalidades { get; } = new ObservableCollection<Naturalidade>();


        // Essa propriedade é um atalho para o item selecionado do combobox de estados. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public Estado EstadoSelecionado { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de estados. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<Estado> Estados { get; } = new ObservableCollection<Estado>();


        // Essa propriedade é um atalho para o texto da text box do endereço. Tudo que acontecer nessa propriedade é refletida de volta para a text box e vice-versa.
        public string Endereco { get; set; }


        // Essa propriedade é um atalho para o item selecionado do combobox de bairros. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public Bairro BairroSelecionado { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de bairros. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<Bairro> Bairros { get; } = new ObservableCollection<Bairro>();


        // Essa propriedade é um atalho para o item selecionado do combobox de cidades. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public Cidade CidadeSelecionada { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de cidades. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<Cidade> Cidades { get; } = new ObservableCollection<Cidade>();

        // Essa propriedade é um atalho para o texto da text box do email. Tudo que acontecer nessa propriedade é refletida de volta para a text box e vice-versa.
        public string Email { get; set; } = "@";


        // Essa propriedade é um atalho para o texto da text box do número. Tudo que acontecer nessa propriedade é refletida de volta para a text box e vice-versa.
        public string Num { get; set; }


        // Essa propriedade é um atalho para o texto da text box do telefone. Tudo que acontecer nessa propriedade é refletida de volta para a text box e vice-versa.
        public string Telefone { get; set; }


        // Essa propriedade é um atalho para o item selecionado do combobox de jornadas. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public Jornada JornadaSelecionada { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de jornadas. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<Jornada> Jornadas { get; } = new ObservableCollection<Jornada>();


        // Essa propriedade é um atalho para o item selecionado do combobox de secretarias. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public Secretaria SecretariaSelecionada { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de secretarias. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<Secretaria> Secretarias { get; } = new ObservableCollection<Secretaria>();


        // Essa propriedade é um atalho para o item selecionado do combobox de orgãos. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public Orgao OrgaoSelecionado { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de orgãos. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<Orgao> Orgaos { get; } = new ObservableCollection<Orgao>();


        // Essa propriedade é um atalho para o item selecionado do combobox de departamentos. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public Departamento DepartamentoSelecionado { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de departamentos. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<Departamento> Departamentos { get; } = new ObservableCollection<Departamento>();


        // Essa propriedade é um atalho para o item selecionado do combobox de dotações. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public Dotacao DotacaoSelecionado { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de dotações. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<Dotacao> Dotacoes { get; } = new ObservableCollection<Dotacao>();


        // Essa propriedade é um atalho para o item selecionado do combobox de descrições de vínculos. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public DescricaoVinculo DescricaoVinculoSelecionado { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de descrições de vínculos. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<DescricaoVinculo> DescricaoVinculos { get; } = new ObservableCollection<DescricaoVinculo>();

        // Essa propriedade é um atalho para o texto da text box do alocação. Tudo que acontecer nessa propriedade é refletida de volta para a text box e vice-versa.
        public string Alocacao { get; set; }


        // Essa propriedade é um atalho para o texto da text box de salário, que é somente leitura. Somente ela irá reflir seu estado para o texbox
        public decimal Salario { get => CargoSelecionado?.Salario ?? 0; }


        // Essa propriedade é um atalho para o item selecionado do combobox de cargos. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public Cargo CargoSelecionado { get; set; }


        // Essa propriedade é um atalho para os itens presentes no combobox de cargos. Tudo que acontecer nessa propriedade é refletida de volta para o combobox.
        public ObservableCollection<Cargo> Cargos { get; } = new ObservableCollection<Cargo>();


        // Essa propriedade é um atalho para o calendário de início de vigência.
        public DateTime VigenciaInicio { get; set; } = DateTime.Now;


        // Essa propriedade é um atalho para o calendário de fim de vigência.
        public DateTime VigenciaFim { get; set; } = DateTime.Now;


        // Essa propriedade é um atalho para os itens presentes no datagrid de contratos salvos. Tudo que acontecer nessa propriedade é refletida de volta para o datagrid.
        public ObservableCollection<Contrato> ContratosSalvos { get; } = new ObservableCollection<Contrato>();


        // Essa propriedade é um atalho o botão de Salvar
        public DelegateCommand SalvarCommand { get; }


        // Esse método será chamado sempre que o usuário clicar no botão salvar, pois ele foi associado ao comando "SalvarCommand" no construtor
        private void SalvarExecute()
        {
            // Criamos um Funcionario e passamos os dados recolhidos do usuário para esse Funcionario.
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

            // Criamos um contrato e passamos os dados recolhidos do usuário para esse contrato.
            var contrato = new Contrato
            {
                Funcionario = funcionario, // definimos o Funcionario criado acima para esse contrato.
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

            // Ok. Agora é a hora de persistir esse Contrato no banco:

            // Gravamos o objeto criado acima no banco de dados. Isso é similar a "INSERT INTO Contrato (colunas do banco aqui) VaLUES (valores do usuário aqui);"
            Repository.Add(contrato);

            // Após salvar o contrato, adicionamos ao datagrid de contratos salvos o novo contrato inserido
            ContratosSalvos.Add(contrato);
        }
    }
}
