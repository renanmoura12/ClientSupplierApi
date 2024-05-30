using ClientSupplierApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientSupplierApi.Services
{
    public static class DbService
    {
        public static IServiceCollection Db(this IServiceCollection services, IConfiguration config)
        {
            var mySqlConnection = config.GetConnectionString("conexao");

            services.AddDbContextPool<DataContext>(
                a => a.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection),
                a => a.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));

            return services;
        }
    }
}
