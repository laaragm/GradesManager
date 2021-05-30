using AutoMapper;
using Base.Infra.Abstractions;
using GradesManager.API.Settings;
using GradesManager.Infra.Abstractions.Repositories;
using GradesManager.Infra.Extensions;
using GradesManager.Infra.Repositories;
using GradesManager.Services;
using GradesManager.Services.Abstractions;
using GradesManager.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradesManager.API
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public AppSettings AppSettings { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			AppSettings = Configuration.Get<AppSettings>();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddScoped<IInfraSettings>(x => AppSettings.InfraSettings)
				.AddRepositories()
				.AddServices();

			services.AddControllers();

			services.AddSwaggerGen(x =>
			{
				x.SwaggerDoc("v1", new OpenApiInfo { Title = "Grades Manager API", Version = "v1" });
			});
			services.AddApiVersioning(x =>
			{
				x.AssumeDefaultVersionWhenUnspecified = true;
				x.ReportApiVersions = true;
			});

			var mapperConfig = new MapperConfiguration(x =>
			{
				x.AddProfile(new MappingProfile());
			});
			IMapper mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);

			services
				.AddMvc()
				.AddJsonOptions(x =>
				{
					x.JsonSerializerOptions.MaxDepth = 100;
					x.JsonSerializerOptions.IgnoreNullValues = true;
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors(x =>
			{
				x
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowAnyOrigin();
			});

			app.UseSwagger();

			app.UseSwaggerUI(x =>
			{
				x.SwaggerEndpoint("v1/swagger.json", "GradesManager");
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
