using FeirasEspinhoBlazorApp.Data;
using FeirasEspinhoBlazorApp.Shared;
using FeirasEspinhoBlazorApp.SourceCode;
using FeirasEspinhoBlazorApp.SourceCode.WebClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<SistemaFeiras>();
builder.Services.AddScoped<SistemaFeiras>(x => SistemaFeiras.GetInstance());
builder.Services.AddScoped<NavBar>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");
});

app.Run();
