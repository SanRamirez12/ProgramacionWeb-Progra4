var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuracion de las variables de sesion
builder.Services.AddDistributedMemoryCache(); //se asigna un poco de memoria cache pa las cookies
builder.Services.AddSession(options => 
{ //Tiempo deexpiracion de la sesion
    options.IOTimeout = TimeSpan.FromMinutes(20); //20min
    options.Cookie.HttpOnly = true; //Seguridad del cookie
    options.Cookie.IsEssential = true; //Hacer que lacookie de sesion sea esencial
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//Usar la sesion
app.UseSession();

//app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
