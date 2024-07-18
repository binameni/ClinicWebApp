using Clinic.Core.Domain.IdentityEntities;
using Clinic.Infrastructure.DbContext;
using ClinicWebApp.Extensions.StartupExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbContext();
builder.Services.AddRazorPages();
builder.ConfigureUserCredentials();
builder.ConfigureServices();
builder.ConfigureQuartz();
builder.ConfigureModels();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
