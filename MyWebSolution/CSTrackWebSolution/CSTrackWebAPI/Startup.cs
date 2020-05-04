using Autofac;
using Autofac.Extensions.DependencyInjection;
using CSTrackWebAPI.Model.Context;
using CytometrixWebAPI.APISettings.Swagger;
using CytometrixWebAPI.Extensions;
using CytometrixWebAPI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Cytometrix
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IHostingEnvironment CurrentEnvironment { get; set; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        private const string CorsPolicy = "CorsPolicy";


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SQLContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLContext"),
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(SQLContext).GetTypeInfo().Assembly.GetName().Name);
                        });
            },ServiceLifetime.Scoped);


            services.RegisterServices();

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

           
            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            services.AddSwagger();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            MapperInstaller.Load(containerBuilder);

            return new AutofacServiceProvider(containerBuilder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            if (!CurrentEnvironment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandleMiddleware>();

            app.UseSwaggerBase();

            app.UseStaticFiles();

            app.UseCors(CorsPolicy);

            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SQLContext>();
                context.Database.Migrate();
            }

        }
    }
}
