using System.Linq;
using DNTCaptcha.Core;
using Evaluation.Data;
using Evaluation.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Evaluation
{
	public class Startup
	{
		//private readonly IWebHostEnvironment _env;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		private IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public virtual void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services
				.AddControllersWithViews(options =>
				{
					options.Filters.Add<OperationCancelledExceptionFilter>();
				})
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.PropertyNamingPolicy = null;
				});

			services.AddResponseCompression(options =>
			{
				options.Providers.Add<BrotliCompressionProvider>();
				options.Providers.Add<GzipCompressionProvider>();
				options.MimeTypes =
					ResponseCompressionDefaults.MimeTypes.Concat(
						new[] { "text/javascript" });
			});

			services.AddDNTCaptcha(options =>
			{
				options.UseCookieStorageProvider();
				//options.UseCustomFont(Path.Combine(_env.WebRootPath, "fonts", "IranSans.ttf"));
			});

			services.AddDbContext<EvaluationContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("EvaluationContext")));

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
			{
				options.LoginPath = "/";
				options.LogoutPath = "/Home/Logout";
				options.AccessDeniedPath = "/Home/AccessDenied";
				options.Cookie.IsEssential = true;
				options.SlidingExpiration = true;
			});
			
			services.AddValidators();
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
			}

			app.UseResponseCompression();

			//app.Use(async (context, next) =>
			//{
			//	await next();
			//	if (context.Response.StatusCode == 404)
			//	{
			//		context.Request.Path = "/";
			//		await next();
			//	}
			//});

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}