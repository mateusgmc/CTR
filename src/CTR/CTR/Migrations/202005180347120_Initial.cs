namespace CTR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bairro",
                c => new
                    {
                        BairroId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.BairroId);
            
            CreateTable(
                "dbo.Cargo",
                c => new
                    {
                        CargoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        SecretariaId = c.Int(nullable: false),
                        Salario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CargoId)
                .ForeignKey("dbo.Secretaria", t => t.SecretariaId)
                .Index(t => t.SecretariaId);
            
            CreateTable(
                "dbo.Contrato",
                c => new
                    {
                        ContratoId = c.Int(nullable: false, identity: true),
                        DataCadastro = c.DateTime(nullable: false),
                        FuncionarioId = c.Int(nullable: false),
                        SecretariaId = c.Int(nullable: false),
                        OrgaoId = c.Int(nullable: false),
                        DepartamentoId = c.Int(nullable: false),
                        DotacaoId = c.Int(nullable: false),
                        DescricaoVinculoId = c.Int(nullable: false),
                        Alocacao = c.String(maxLength: 8000, unicode: false),
                        CargoId = c.Int(nullable: false),
                        JornadaId = c.Int(nullable: false),
                        VigenciaInicio = c.DateTime(nullable: false),
                        VigenciaFim = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContratoId)
                .ForeignKey("dbo.Cargo", t => t.CargoId)
                .ForeignKey("dbo.Departamento", t => t.DepartamentoId)
                .ForeignKey("dbo.Secretaria", t => t.SecretariaId)
                .ForeignKey("dbo.Orgao", t => t.OrgaoId)
                .ForeignKey("dbo.DescricaoVinculo", t => t.DescricaoVinculoId)
                .ForeignKey("dbo.Dotacao", t => t.DotacaoId)
                .ForeignKey("dbo.Funcionario", t => t.FuncionarioId)
                .ForeignKey("dbo.Jornada", t => t.JornadaId)
                .Index(t => t.FuncionarioId)
                .Index(t => t.SecretariaId)
                .Index(t => t.OrgaoId)
                .Index(t => t.DepartamentoId)
                .Index(t => t.DotacaoId)
                .Index(t => t.DescricaoVinculoId)
                .Index(t => t.CargoId)
                .Index(t => t.JornadaId);
            
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        DepartamentoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        SecretariaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartamentoId)
                .ForeignKey("dbo.Secretaria", t => t.SecretariaId)
                .Index(t => t.SecretariaId);
            
            CreateTable(
                "dbo.Secretaria",
                c => new
                    {
                        SecretariaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.SecretariaId);
            
            CreateTable(
                "dbo.DescricaoVinculo",
                c => new
                    {
                        DescricaoVinculoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        SecretariaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DescricaoVinculoId)
                .ForeignKey("dbo.Secretaria", t => t.SecretariaId)
                .Index(t => t.SecretariaId);
            
            CreateTable(
                "dbo.Dotacao",
                c => new
                    {
                        DotacaoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        SecretariaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DotacaoId)
                .ForeignKey("dbo.Secretaria", t => t.SecretariaId)
                .Index(t => t.SecretariaId);
            
            CreateTable(
                "dbo.Orgao",
                c => new
                    {
                        OrgaoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        SecretariaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrgaoId)
                .ForeignKey("dbo.Secretaria", t => t.SecretariaId)
                .Index(t => t.SecretariaId);
            
            CreateTable(
                "dbo.Funcionario",
                c => new
                    {
                        FuncionarioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 8000, unicode: false),
                        Cpf = c.String(maxLength: 8000, unicode: false),
                        Rg = c.String(maxLength: 8000, unicode: false),
                        OrgExp = c.String(maxLength: 8000, unicode: false),
                        EstadoCivil = c.String(maxLength: 8000, unicode: false),
                        Naturalidade = c.String(maxLength: 8000, unicode: false),
                        EstadoId = c.Int(nullable: false),
                        Endereco = c.String(maxLength: 8000, unicode: false),
                        BairroId = c.Int(nullable: false),
                        CidadeId = c.Int(nullable: false),
                        Email = c.String(maxLength: 8000, unicode: false),
                        Num = c.String(maxLength: 8000, unicode: false),
                        Telefone = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.FuncionarioId)
                .ForeignKey("dbo.Bairro", t => t.BairroId)
                .ForeignKey("dbo.Cidade", t => t.CidadeId)
                .ForeignKey("dbo.Estado", t => t.EstadoId)
                .Index(t => t.EstadoId)
                .Index(t => t.BairroId)
                .Index(t => t.CidadeId);
            
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        CidadeId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.CidadeId);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.EstadoId);
            
            CreateTable(
                "dbo.Jornada",
                c => new
                    {
                        JornadaId = c.Int(nullable: false, identity: true),
                        Horas = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoJornadaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JornadaId)
                .ForeignKey("dbo.TipoJornada", t => t.TipoJornadaId)
                .Index(t => t.TipoJornadaId);
            
            CreateTable(
                "dbo.TipoJornada",
                c => new
                    {
                        TipoJornadaId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.TipoJornadaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contrato", "JornadaId", "dbo.Jornada");
            DropForeignKey("dbo.Jornada", "TipoJornadaId", "dbo.TipoJornada");
            DropForeignKey("dbo.Contrato", "FuncionarioId", "dbo.Funcionario");
            DropForeignKey("dbo.Funcionario", "EstadoId", "dbo.Estado");
            DropForeignKey("dbo.Funcionario", "CidadeId", "dbo.Cidade");
            DropForeignKey("dbo.Funcionario", "BairroId", "dbo.Bairro");
            DropForeignKey("dbo.Contrato", "DotacaoId", "dbo.Dotacao");
            DropForeignKey("dbo.Contrato", "DescricaoVinculoId", "dbo.DescricaoVinculo");
            DropForeignKey("dbo.Orgao", "SecretariaId", "dbo.Secretaria");
            DropForeignKey("dbo.Contrato", "OrgaoId", "dbo.Orgao");
            DropForeignKey("dbo.Dotacao", "SecretariaId", "dbo.Secretaria");
            DropForeignKey("dbo.DescricaoVinculo", "SecretariaId", "dbo.Secretaria");
            DropForeignKey("dbo.Departamento", "SecretariaId", "dbo.Secretaria");
            DropForeignKey("dbo.Contrato", "SecretariaId", "dbo.Secretaria");
            DropForeignKey("dbo.Cargo", "SecretariaId", "dbo.Secretaria");
            DropForeignKey("dbo.Contrato", "DepartamentoId", "dbo.Departamento");
            DropForeignKey("dbo.Contrato", "CargoId", "dbo.Cargo");
            DropIndex("dbo.Jornada", new[] { "TipoJornadaId" });
            DropIndex("dbo.Funcionario", new[] { "CidadeId" });
            DropIndex("dbo.Funcionario", new[] { "BairroId" });
            DropIndex("dbo.Funcionario", new[] { "EstadoId" });
            DropIndex("dbo.Orgao", new[] { "SecretariaId" });
            DropIndex("dbo.Dotacao", new[] { "SecretariaId" });
            DropIndex("dbo.DescricaoVinculo", new[] { "SecretariaId" });
            DropIndex("dbo.Departamento", new[] { "SecretariaId" });
            DropIndex("dbo.Contrato", new[] { "JornadaId" });
            DropIndex("dbo.Contrato", new[] { "CargoId" });
            DropIndex("dbo.Contrato", new[] { "DescricaoVinculoId" });
            DropIndex("dbo.Contrato", new[] { "DotacaoId" });
            DropIndex("dbo.Contrato", new[] { "DepartamentoId" });
            DropIndex("dbo.Contrato", new[] { "OrgaoId" });
            DropIndex("dbo.Contrato", new[] { "SecretariaId" });
            DropIndex("dbo.Contrato", new[] { "FuncionarioId" });
            DropIndex("dbo.Cargo", new[] { "SecretariaId" });
            DropTable("dbo.TipoJornada");
            DropTable("dbo.Jornada");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
            DropTable("dbo.Funcionario");
            DropTable("dbo.Orgao");
            DropTable("dbo.Dotacao");
            DropTable("dbo.DescricaoVinculo");
            DropTable("dbo.Secretaria");
            DropTable("dbo.Departamento");
            DropTable("dbo.Contrato");
            DropTable("dbo.Cargo");
            DropTable("dbo.Bairro");
        }
    }
}
