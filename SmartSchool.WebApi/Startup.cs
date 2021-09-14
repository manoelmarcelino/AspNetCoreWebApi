using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Data.UnitOfWork;

namespace SmartSchool.WebApi
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
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAlunoRepository, AlunoRepository>();
            services.AddTransient<IProfessorRepository, ProfessorRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddVersionedApiExplorer(options => {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            }).AddApiVersioning(options => {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            var apiVersions = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddControllers();
            
            services.AddSwaggerGen(c => {
                
                foreach(var apiDescription in apiVersions.ApiVersionDescriptions) {

                      c.SwaggerDoc(apiDescription.GroupName, new OpenApiInfo { 
                        Title = "SmartSchool API", 
                        Version = apiDescription.ApiVersion.ToString(),
                        Description = "Descrição das APIs ASP.NET 5 ",
                        TermsOfService = new Uri("http://www.termosdeuso.com.br"),
                        License = new OpenApiLicense {
                            Name = "Teste de Linceça",
                            Url = new Uri("http://www.manuvmarcelino.com"),
                        },
                        Contact = new OpenApiContact(){
                            Name = "Manoel Vitor Marcelino",
                            Email = "manoellvictor@gmail.com",
                            Url = new Uri("http://www.empresaparacontato.com"),
                        }

                    });
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersions)
        {
            if (env.IsDevelopment()){   
                
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>  { 
                
                foreach(var apiDescription in apiVersions.ApiVersionDescriptions) {
                    c.SwaggerEndpoint(
                        $"/swagger/{apiDescription.GroupName}/swagger.json", 
                        apiDescription.GroupName.ToUpperInvariant()
                    );
                }
                
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
