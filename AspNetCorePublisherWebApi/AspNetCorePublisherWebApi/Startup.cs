using AspNetCorePublisherWebApi.Entities;
using AspNetCorePublisherWebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace AspNetCorePublisherWebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
            
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionString = Configuration["connectionStrings:sqlConnection"];
            services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(connectionString));
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Book, Models.BookDTO>();
                config.CreateMap<Models.BookDTO, Book>();
                config.CreateMap<Publisher, Models.PublisherDTO>();
                config.CreateMap<Models.PublisherDTO, Publisher>();
                config.CreateMap<Publisher, Models.PublisherUpdateDTO>();
                config.CreateMap<Models.PublisherUpdateDTO, Publisher>();
                config.CreateMap<Book, Models.BookUpdateDTO>();
                config.CreateMap<Models.BookUpdateDTO, Book>();
            });

            services.AddScoped<IBookStoreRepository, BookstoreSqlRepository>();
            services.AddScoped<IGenericEFRepository, GenericEFRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseMvc();

           /* app.Run(async (context) =>
            {
                var message = Configuration["Message"];
                await context.Response.WriteAsync(message);
            }); */
        }
    }
}
