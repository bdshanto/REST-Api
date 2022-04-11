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
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
      //  services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

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