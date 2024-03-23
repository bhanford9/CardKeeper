using CardManager.Components;
using CardManager.Models;
using CardManager.Services;
using CardManager.ViewModels;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SerializationServices;
using WebScraping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazorBootstrap();

// Dependency Injection
builder.Services
    .RegisterServices()
    .RegisterViewModels()
    .RegisterModels()
    .RegisterWebScraping()
    .RegisterSerializationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
