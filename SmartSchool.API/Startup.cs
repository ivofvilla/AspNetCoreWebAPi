using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.Api.Data;
using SmartSchool.API.Data;

namespace SmartSchool.API
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
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("default"))
            );

            services.AddControllers()
                    .AddNewtonsoftJson(
                        opt => opt.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore
                     );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRepository, Repository>();

            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
            });

            var apiProviderDescricao = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(opt =>
            {
                foreach (var descricao in apiProviderDescricao.ApiVersionDescriptions)
                {
                    opt.SwaggerDoc(descricao.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "SmartSchool API",
                        Version = descricao.ApiVersion.ToString(),
                        TermsOfService = new Uri("http://testesTermoUso.com"),
                        Description = "descricao da api",
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "SmartSchool License",
                            Url = new Uri("http://mit.com")
                        },
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Eu mesmo",
                            Email = "",
                            Url = new Uri("http://wwww.meusite.com.br")
                        }
                    });
                }

                var xmlComentario = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCaminho = Path.Combine(AppContext.BaseDirectory, xmlComentario);

                opt.IncludeXmlComments(xmlCaminho);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiDescricao)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger()
               .UseSwaggerUI(opt => {

                   foreach (var descricao in apiDescricao.ApiVersionDescriptions)
                   {
                       opt.SwaggerEndpoint($"/swagger/{descricao.GroupName}/swagger.json", descricao.GroupName.ToUpperInvariant());
                   }
                   opt.RoutePrefix = "";
               });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
