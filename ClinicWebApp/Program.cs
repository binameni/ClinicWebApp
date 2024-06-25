using Clinic.Core.Domain.IdentityEntities;
using Clinic.Infrastructure.DbContext;
using ClinicWebApp.Extensions.StartupExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbContext();

builder.Services.AddRazorPages();

builder.ConfigureUserCredentials();
builder.ConfigureServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
