namespace CTR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relacionamentos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstadoCivil",
                c => new
                    {
                        EstadoCivilId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.EstadoCivilId);
            
            CreateTable(
                "dbo.Naturalidade",
                c => new
                    {
                        NaturalidadeId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.NaturalidadeId);
            
            AddColumn("dbo.Funcionario", "EstadoCivilId", c => c.Int(nullable: false));
            AddColumn("dbo.Funcionario", "NaturalidadeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Funcionario", "EstadoCivilId");
            CreateIndex("dbo.Funcionario", "NaturalidadeId");
            AddForeignKey("dbo.Funcionario", "EstadoCivilId", "dbo.EstadoCivil", "EstadoCivilId");
            AddForeignKey("dbo.Funcionario", "NaturalidadeId", "dbo.Naturalidade", "NaturalidadeId");
            DropColumn("dbo.Funcionario", "EstadoCivil");
            DropColumn("dbo.Funcionario", "Naturalidade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Funcionario", "Naturalidade", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Funcionario", "EstadoCivil", c => c.String(maxLength: 8000, unicode: false));
            DropForeignKey("dbo.Funcionario", "NaturalidadeId", "dbo.Naturalidade");
            DropForeignKey("dbo.Funcionario", "EstadoCivilId", "dbo.EstadoCivil");
            DropIndex("dbo.Funcionario", new[] { "NaturalidadeId" });
            DropIndex("dbo.Funcionario", new[] { "EstadoCivilId" });
            DropColumn("dbo.Funcionario", "NaturalidadeId");
            DropColumn("dbo.Funcionario", "EstadoCivilId");
            DropTable("dbo.Naturalidade");
            DropTable("dbo.EstadoCivil");
        }
    }
}
