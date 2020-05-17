using CTR.Models.POCO;
using System.Data.Entity;

namespace CTR.Infrastructure.Initializers
{
    public class CTRInitializer : CreateDatabaseIfNotExists<CTRContext>
    {
        protected override void Seed(CTRContext context)
        {
            // TODO: Alimentar o banco de dados abaixo

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

            context.SaveChanges();
        }
    }
}
