using _06_Entity.DAO;
using _06_Entity.Models;
using _06_Entity.Services;
using _06_Entity.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-6.0
// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();

// https://csharp-video-tutorials.blogspot.com/2019/05/logging-to-file-in-aspnet-core-using.html
// - Install NLog.Web.AspNetCore nuget package
// - Create nlog.config file
// - Enable copy to bin folder (nlog.config => clic droit => propriétés => "copier dans le répertoire de sortie : copier si plus récent
// - Enable NLog as one of the Logging Provider
builder.Logging.AddNLog();

// Add services to the container.
builder.Services.AddControllersWithViews();

// ---------------------
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// ---------------------


// builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("MailSettings"));

var mailSettings = builder.Configuration.GetSection("MailSettings");

builder.Services.Add(new ServiceDescriptor(typeof(EmailSettings),
                                               c => new EmailSettings()
                                               {
                                                   Mail = mailSettings.GetValue<string>("Mail"),
                                                   DisplayName = mailSettings.GetValue<string>("DisplayName"),
                                                   Password = mailSettings.GetValue<string>("Password"),
                                                   Host = mailSettings.GetValue<string>("Host"),
                                                   Port = mailSettings.GetValue<int>("Port")
                                               }, ServiceLifetime.Transient));

builder.Services.AddScoped<IEmailService, EmailService>();

// ----------------- Pour accéder à la couche d'accès aux données des produits
builder.Services.AddScoped<IProductDAO, ProductDAO>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    /*
     * ------------------
     * https://youtu.be/DVo138knAHQ
     * https://youtu.be/9CwgiSxrkeQ
     * passer de "ASPNETCORE_ENVIRONMENT": "Development"
     * à "ASPNETCORE_ENVIRONMENT": "Production"
     * dans launchSettings
     * 
     * Créer un Controller "ErrorController"
     * ------------------
     */
    // app.UseStatusCodePages();
    // app.UseStatusCodePagesWithRedirects("/Error/{0}");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseHttpsRedirection();

/*var supportedCultures = new[]
{
 new CultureInfo("en-US"),
 new CultureInfo("fr"),
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    // Formatting numbers, dates, etc. 
    SupportedCultures = supportedCultures,
    // UI strings that we have localized.
    SupportedUICultures = supportedCultures
});
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US")
});*/

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Logger.LogInformation("Adding Routes");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Logger.LogInformation("Starting the app");
app.Run();