using Microsoft.AspNetCore.Authentication.Negotiate; // Korištenje ASP.NET Core Negotiate autentifikacije
using Microsoft.EntityFrameworkCore; // Korištenje Entity Framework Core za kontekst baze podataka i operacije
using mrzim_te.Models; // Uvoz modela iz projektne domene
using mrzim_te.Hubs; // Uvoz hubova iz projektne domene

var builder = WebApplication.CreateBuilder(args); // Stvaranje nove instance WebApplication buildera s predanim argumentima iz naredbenog retka

// Dodavanje servisa u spremnik.
builder.Services.AddControllers(); // Dodavanje servisa za kontrolere u spremnik za ovisnosti
builder.Services.AddRazorPages(); // Omogućavanje Razor stranica
builder.Services.AddSignalR(); // Dodavanje SignalR servisa

// Konfiguracija Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer(); // Dodavanje podrške za istraživanje API krajnjih točaka
builder.Services.AddSwaggerGen(); // Dodavanje servisa za generiranje Swagger dokumentacije za API

// Konfiguracija Entity Framework Core za SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Konfiguriranje EF Core za korištenje SQL Servera s vezom iz konfiguracije

// Dodavanje HttpClient servisa
builder.Services.AddHttpClient(); // Registracija HttpClient-a za ubrizgavanje ovisnosti

// Isključi autentifikaciju i autorizaciju radi testiranja
// builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//     .AddNegotiate(); // Dodavanje Negotiate autentifikacijske sheme

// builder.Services.AddAuthorization(options =>
// {
//     // Prema zadanim postavkama, svi dolazni zahtjevi će biti autorizirani prema zadanoj politici.
//     options.FallbackPolicy = options.DefaultPolicy; // Postavljanje rezervne politike na zadanu politiku autorizacije
// });

var app = builder.Build(); // Izgradnja WebApplication instance

// Konfiguriranje HTTP zahtjevnog tijeka
if (app.Environment.IsDevelopment()) // Provjera je li okruženje razvojno (Development)
{
    app.UseSwagger(); // Omogućavanje Swagger posrednika za posluživanje generiranog Swagger JSON-a
    app.UseSwaggerUI(); // Omogućavanje Swagger UI posrednika za posluživanje Swagger UI-a
}

app.UseHttpsRedirection(); // Omogućavanje posrednika za preusmjeravanje na HTTPS

// Omogućavanje posluživanja statičkih datoteka iz mape "wwwroot"
app.UseStaticFiles(); // Posrednik za posluživanje statičkih datoteka poput slika

// app.UseAuthentication(); // Isključi ovo za testiranje // Omogućavanje posrednika za autentifikaciju
app.UseAuthorization(); // Omogućavanje posrednika za autorizaciju

app.MapControllers(); // Mapiranje ruta kontrolera u aplikaciji
app.MapRazorPages(); // Mapiranje Razor stranica

app.MapHub<NotificationHub>("/notificationHub"); // Mapiranje NotificationHub-a na krajnju točku /notificationHub

app.Run(); // Pokretanje aplikacije
