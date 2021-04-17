using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Evaluation.Utilities
{
	public static class ServiceRegistryUtility
	{
		public static IServiceCollection AddValidators(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			Assembly.GetExecutingAssembly().GetTypes()
				.Where(t => t.IsClass && t.Namespace == "Evaluation.Models.Validators" && t.Name.EndsWith("Validator"))
				.ToList()
				.ForEach(type =>
				{
					var iValidator = type.BaseType?.GetInterfaces().First(t => t.Name.StartsWith("IValidator`1"));

					if (iValidator != null)
					{
						services.Add(new ServiceDescriptor(iValidator, type, serviceLifetime));
					}
				});

			return services;
		}
	}
}