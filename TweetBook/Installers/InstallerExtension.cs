namespace TweetBook.Installers;

public static class InstallerExtension
{
    public static void InstallerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var installers = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
        installers.ForEach(c => c.InstallServices(services, configuration));

    }
}