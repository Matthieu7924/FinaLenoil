var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------------
// --Ajout (abonnement) aux services dont aura besoin l'application--
// ------------------------------------------------------------------

// Add services to the container.
builder.Services.AddControllersWithViews();

// ------------------------------------------------------------------
// ----------------   Build Application   ---------------------------
// ------------------------------------------------------------------
var app = builder.Build();

// ------------------------------------------------------------------
// - CONFIGURATION DU PIPELINE PERMETTANT DE REPONDRE A LA REQUETE --
// ------------------------------------------------------------------

// Le pipeline spécifie la manière dont l'application doit répondre à la requête
// Quand l'application recoit une requete de la part du client, la requête fait aller/retour à travers le pipeline
// Et plus précisément à partir d'un ensemble de middelwares composant ledit pipeline

// Ces middlewares permettent chacun d'adresser un (et un seul) point technique lié au traitement de la requête.
// - La gestion des erreuers
// - la gestion des cookies
// - l'authentification
// - la gestion des sessions
// - le routage...
// - ...

// Le passsage de la reqête à travers l'ensemble des middleware permettra de créer la réponse.
// L'ordre des middleware est très important car ils sont invoqués séquentiellement
// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    // Si pas en environnement de développement, on redirige vers l'action 'Error' du Controller 'Home'
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // indique au client qu'on préfère qu'il utilise HTTPS pour faire ces requêtes
}

app.UseHttpsRedirection(); // Middleware de redirection vers https

app.UseStaticFiles(); // Middleware permettant d'utiliser des ressources statiques (css, js, fichiers...) placées dans le dossier 'wwwroot'

app.UseRouting();

app.UseAuthorization();

// Le middleware 'UseRouting' autorise la création d'un ensemble de 'route'
// La compatibilité de l'URL de la reqête avec chaque route est successivement testée jusqu'à ce que l'une d'entre elles matche
// La route qui matche va permettre de router le traitement de la re^quête vers : 
// - un controller
// - une action du controller (avec paramètres optionnels)

// Dès lors qu'une route matche, les suivantes ne pas testées
// Si aucune route ne matche, la requête est transférée au middleware suivant.

/*app.MapControllerRoute(
    name: "reverse",
    pattern: "{action=Privacy}/{controller=Home}/{id?}");*/

/*app.MapControllerRoute(
    name: "custom",
    pattern: "{controller=new}/{action=ActionReturnString}/{id?}");*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Middleware de mise en cache
app.UseResponseCaching();
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(10)
        };

    await next();
});

// -------------------------------------------------------
// ---------------------RUN Application ------------------
// -------------------------------------------------------
app.Run();
