using InventorySampleUI.Data;
using InventorySampleUI.Services;
using InventorySampleUI.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using InventorySampleUI.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorBootstrap();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<ICustomerService,CustomerService>();
builder.Services.AddScoped<BaseService>();
builder.Services.AddScoped<StoreService>();
builder.Services.AddScoped<PartService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
