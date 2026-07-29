namespace GerenciamentoPlantao.Data.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            await RoleSeeder.SeedAsync(services);
            await DepartamentoSeeder.SeedAsync(services);
            await EstabelecimentoSeeder.SeedAsync(services);
            await SetorSeeder.SeedAsync(services);
            await CanalSeeder.SeedAsync(services);
            await CategoriaSeeder.SeedAsync(services);
            await SolucaoSeeder.SeedAsync(services);
            await UsuarioSeeder.SeedAsync(services);
        }
    }
}