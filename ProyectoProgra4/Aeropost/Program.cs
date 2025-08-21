using System.Globalization;
using Microsoft.AspNetCore.Localization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Activamos localización de vistas y DataAnnotations, y configuramos mensajes del model binding en español.
builder.Services
    .AddControllersWithViews(options =>
    {
        var m = options.ModelBindingMessageProvider;
        m.SetValueIsInvalidAccessor(_ => "El valor no es válido.");
        m.SetValueMustBeANumberAccessor(_ => "El campo debe ser numérico.");
        m.SetMissingBindRequiredValueAccessor(name => $"Falta el valor para “{name}”.");
        m.SetUnknownValueIsInvalidAccessor(name => $"El valor de “{name}” no es válido.");
        m.SetValueMustNotBeNullAccessor(name => $"El campo “{name}” no puede ser nulo.");
        m.SetAttemptedValueIsInvalidAccessor((value, name) => $"“{value}” no es un valor válido para “{name}”.");
    })
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Configuración de sesión
builder.Services.AddDistributedMemoryCache(); // cache para la sesión
builder.Services.AddSession(options =>
{
    options.IOTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// --- Cultura por defecto: español (Costa Rica) ---
var culturas = new[] { new CultureInfo("es-CR"), new CultureInfo("es-ES") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-CR"),
    SupportedCultures = culturas,
    SupportedUICultures = culturas
});
// -----------------------------------------------

app.UseRouting();
app.UseSession();     // usar sesión
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();

