using Microsoft.EntityFrameworkCore.Design;


namespace DbRepository
{
    public class TemplateDatabaseContextFactory : IDesignTimeDbContextFactory<TemplateDatabaseContext>
    {
        public TemplateDatabaseContext CreateDbContext(string[] args)
        {
            return new TemplateDatabaseContext();
        }
    }
}
