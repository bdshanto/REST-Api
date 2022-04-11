using TweetBook.Installers;

namespace TweetBook;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigurationService(IServiceCollection services)
    {
        services.InstallerServices(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        var swaggerOptions = new Options.SwaggerOptions();
        Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

        app.UseSwagger(options =>
        {
            options.RouteTemplate = swaggerOptions.JsonRoute;
            // options.SerializeAsV2 = true;*/
        });

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            options.RoutePrefix = string.Empty;
        });


        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        });
    }
}