using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace HelloMaui
{
	public class Startup : IStartup
	{
		public void Configure(IAppHostBuilder appBuilder)
		{
			appBuilder
				.UseFormsCompatibility()
#if USE_MS
				.UseMicrosoftExtensionsServiceProviderFactory()
#endif
				.UseMauiApp<App>()
				.ConfigureFonts(fonts => {
					fonts.AddFont("ionicons.ttf", "IonIcons");
				})
				.ConfigureLifecycleEvents(lifecycle => {
					#if ANDROID
					lifecycle.AddAndroid(d => {
						d.OnBackPressed(activity => {
							System.Diagnostics.Debug.WriteLine("Back button pressed!");
						});
					});
					#endif
				});
		}
	}

#if USE_MS
	public static class Extensions
	{
		public static IAppHostBuilder UseMicrosoftExtensionsServiceProviderFactory(this IAppHostBuilder builder)
		{
			builder.UseServiceProviderFactory(new DIExtensionsServiceProviderFactory());
			return builder;
		}

		// To use the Microsoft.Extensions.DependencyInjection ServiceCollection and not the MAUI one
		class DIExtensionsServiceProviderFactory : IServiceProviderFactory<ServiceCollection>
		{
			public ServiceCollection CreateBuilder(IServiceCollection services)
				=> new ServiceCollection { services };

			public IServiceProvider CreateServiceProvider(ServiceCollection containerBuilder)
				=> containerBuilder.BuildServiceProvider();
		}
	}
#endif
}