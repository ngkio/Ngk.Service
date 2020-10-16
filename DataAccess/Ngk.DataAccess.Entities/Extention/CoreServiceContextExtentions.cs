using Thor.Framework.Common.Options;
using Thor.Framework.Data.DbContext.Relational;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ngk.DataAccess.Entities
{
    public partial class CoreServiceContext : MySqlDbContext
    {
        public CoreServiceContext(IOptions<DbContextOption> option)
    : base(option)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            base.OnConfiguring(optionsBuilder);

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkMySql()
                .AddSingleton<IModelCustomizer, NgknModelCustomizer>()
                .BuildServiceProvider();
            optionsBuilder.UseInternalServiceProvider(serviceProvider);
        }
    }

    public class NgknModelCustomizer : RelationalModelCustomizer
    {
        public override void Customize(ModelBuilder modelBuilder, DbContext context)
        {
            base.Customize(modelBuilder, context);
        }

        public NgknModelCustomizer(ModelCustomizerDependencies dependencies) : base(dependencies)
        {
        }
    }
}
