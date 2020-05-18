using CTR.Models.POCO;
using System.Data.Entity;
using System.Linq;

namespace CTR.Infrastructure.Initializers
{
    public class CTRInitializer : CreateDatabaseIfNotExists<CTRContext>
    {
        protected override void Seed(CTRContext context)
        {
            // TODO: Alimentar o banco de dados abaixo

            // Populando Estados civis
            context.EstadoCivis.Add(new EstadoCivil
            {
                Descricao = "CASADO(A)"
            });
            context.EstadoCivis.Add(new EstadoCivil
            {
                Descricao = "SOLTEIRO(A)"
            });
            context.EstadoCivis.Add(new EstadoCivil
            {
                Descricao = "VIÚVO(A)"
            });
            context.EstadoCivis.Add(new EstadoCivil
            {
                Descricao = "UNIÃO ESTÁVEL"
            });
            context.EstadoCivis.Add(new EstadoCivil
            {
                Descricao = "DIVORCIADO(A)"
            });

            // Populando Naturalidades
            context.Naturalidades.Add(new Naturalidade
            {
                Descricao = "BRASILEIRO NATO"
            });
            context.Naturalidades.Add(new Naturalidade
            {
                Descricao = "BRASILEIRO NATURALIZADO"
            });
            context.Naturalidades.Add(new Naturalidade
            {
                Descricao = "ESTRANGEIRO"
            });

            // Populando estados
            context.Estados.Add(new Estado
            {
                Nome = "BA"
            });
            context.Estados.Add(new Estado
            {
                Nome = "PE"
            });

            // Populando bairros
            context.Bairros.Add(new Bairro
            {
                Nome = "CENTRO"
            });

            // Populando cidades
            context.Cidades.Add(new Cidade
            {
                Nome = "PORTO SEGURO"
            });

            // Populando secretarias
            var secretariaSaude = new Secretaria
            {
                Nome = "SAÚDE"
            };
            context.Secretarias.Add(secretariaSaude);
            context.SaveChanges();

            // Populando dotações
            context.Dotacoes.Add(new Dotacao
            {
                Secretaria = secretariaSaude,
                Descricao = "1012200072.079-MANUTENÇÃO DAS ATIVIDADES DE COORDENAÇÃO E CONTROLE DOS SERVIÇOS DA"
            });
            context.SaveChanges();

            // Populando orgãos
            context.Orgaos.Add(new Orgao
            {
                Secretaria = secretariaSaude,
                Descricao = "11900-SECRETARIA DE SAÚDE"
            });
            context.SaveChanges();

            // Populando departamentos
            context.Departamentos.Add(new Departamento
            {
                Secretaria = secretariaSaude,
                Descricao = "11993-FUNDO MUNICIPAL DE SAÚDE"
            });
            context.SaveChanges();

            // Populando descrições vínculos
            context.DescricaoVinculos.Add(new DescricaoVinculo
            {
                Secretaria = secretariaSaude,
                Descricao = "3.1.90.04-CONTRATAÇÃO POR TEMPO DETERMINADO"
            });
            context.SaveChanges();

            // Populando cargos
            context.Cargos.Add(new Cargo
            {
                Secretaria = secretariaSaude,
                Descricao = "TÉC. DE ENFERMAGEM",
                Salario = 1039
            });
            context.SaveChanges();

            // Populando tipos jornadas
            var tipoJornadaHorasSemanais = new TipoJornada
            {
                Descricao = "Horas semanais"
            };
            context.TipoJornadas.Add(tipoJornadaHorasSemanais);
            context.SaveChanges();

            // Populando jornadas
            context.Jornadas.Add(new Jornada
            {
                Horas = 120,
                TipoJornada = tipoJornadaHorasSemanais
            });
            context.SaveChanges();
        }
    }
}
