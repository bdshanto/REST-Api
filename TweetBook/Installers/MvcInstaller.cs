namespace TweetBook.Installers;

public class MvcInstaller: IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        //  services.AddEndpointsApiExplorer();
        //  services.AddMvcCore().AddApiExplorer();
        services.AddSwaggerGen(options =>
        {
            // c.SwaggerDoc("v1", new Info { Title = "TweetBook API", Version = "v1" });
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Tweet Book API",
                Description = "An ASP.NET Core Web API for managing ToDo items"
            });
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            /*
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            */
        });
        //services.AddSwaggerGenNewtonsoftSupport();
    }
}