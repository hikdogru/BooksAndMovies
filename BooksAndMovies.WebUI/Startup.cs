using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using BooksAndMovies.Data;
using Microsoft.EntityFrameworkCore;
using BooksAndMovies.Data.Concrete.Ef;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Business.Concrete;
using BooksAndMovies.Data.Abstract;
using FluentValidation.AspNetCore;
using BooksAndMovies.Entity;
using FluentValidation;
using BooksAndMovies.Business.ValidationRules.FluentValidation;
using System.Reflection;

namespace BooksAndMovies.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {



            //Automapper
            services.AddAutoMapper(typeof(Startup));

            // DbContext
            services.AddDbContext<BookAndMovieContext>(option => option.UseSqlServer(Configuration.GetConnectionString("BookAndMovieConnectionString")));

            #region Services
            services.AddScoped<IBookService, BookManager>();
            services.AddScoped<IMovieService, MovieManager>();
            services.AddScoped<ITVShowService, TVShowManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserMovieService, UserMovieManager>();
            services.AddScoped<IUserTVShowService, UserTVShowManager>();
            services.AddScoped<IUserBookService, UserBookManager>();
            #endregion Services


            #region UnitOfWork

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion UnitOfWork



            // Fluent validation
            services.AddControllersWithViews().AddFluentValidation();
            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<IValidator<TVShow>, TVShowValidator>();
            services.AddTransient<IValidator<Movie>, MovieValidator>();

            // Session
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(20);
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Session
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
