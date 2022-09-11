using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseFileServer(new FileServerOptions()
{
    // configuration["ApplicationSettings:MEDIAS"].ToString() is c:/medias or d:/medias
    FileProvider = new PhysicalFileProvider(@"c:/medias"),
    RequestPath = new Microsoft.AspNetCore.Http.PathString(@"/assets/medias"),
    EnableDirectoryBrowsing = false // you make this true or false.
});
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;
// yitong
app.Run();
