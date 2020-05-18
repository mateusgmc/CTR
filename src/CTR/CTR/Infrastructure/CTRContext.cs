using CTR.Infrastructure.Initializers;
using CTR.Models.POCO;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CTR.Infrastructure
{
    public class CTRContext : DbContext
    {
        public CTRContext(string nameOrConnectionString) :
            base(nameOrConnectionString)
        {
            Database.SetInitializer(new CTRInitializer());
        }

        public CTRContext() :
            this("connectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConve‌​ntion>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConve‌​ntion>();

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<string>()
                .Configure((p => p.HasColumnType("varchar")));
        }

        public virtual DbSet<Bairro> Bairros { get; set; }

        public virtual DbSet<Cargo> Cargos { get; set; }

        public virtual DbSet<Cidade> Cidades { get; set; }

        public virtual DbSet<Contrato> Contratos { get; set; }

        public virtual DbSet<Departamento> Departamentos { get; set; }

        public virtual DbSet<DescricaoVinculo> DescricaoVinculos { get; set; }

        public virtual DbSet<Dotacao> Dotacoes { get; set; }

        public virtual DbSet<Estado> Estados { get; set; }

        public virtual DbSet<Funcionario> Funcionarios { get; set; }

        public virtual DbSet<Jornada> Jornadas { get; set; }

        public virtual DbSet<TipoJornada> TipoJornadas { get; set; }

        public virtual DbSet<Orgao> Orgaos { get; set; }

        public virtual DbSet<Secretaria> Secretarias { get; set; }

        public virtual DbSet<Naturalidade> Naturalidades { get; set; }

        public virtual DbSet<EstadoCivil> EstadoCivis { get; set; }
    }
}
