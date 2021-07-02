using Fast.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Fast.API {
	public class Startup {
		public IConfiguration Configuration { get; }

		private readonly string _MyOrigins = "myOrigins";

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			IConfiguration appSettings = Configuration.GetSection("Injectable");
			Repo repo = new(appSettings["ConnectionString"]);

			services.AddCors(options => {
				options.AddPolicy(_MyOrigins, builder => {
					builder.AllowAnyMethod();
					builder.AllowAnyHeader();
					builder.AllowAnyOrigin();
				});
			});
			services.AddTransient(domain => new Domain(repo));
			services.AddControllers();
			services.AddSwaggerGen(c => {
				c.SwaggerDoc("1.0.0", new OpenApiInfo { Title = "Fast Coding Assignment API", Version = "1.0.0" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if(env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseHsts();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("./swagger/1.0.0/swagger.json", "Fast Coding Assignment API");
				c.RoutePrefix = string.Empty;
			});
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors(_MyOrigins);
			app.UseAuthorization();
			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
