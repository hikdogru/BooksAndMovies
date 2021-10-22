using BooksAndMovies.Data.Concrete.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Extensions
{
    public static class MigrationManager
    {
        // Auto migrate extension method
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<BookAndMovieContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception)
            {
            }

            return host;
        }
    }
}
