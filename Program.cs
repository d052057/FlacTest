using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddCors(
             options =>
             {
                 options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("*")
                    .AllowAnyOrigin()
                    .SetIsOriginAllowed(origin => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    //.AllowCredentials()
                    //.AllowCredentials() and AllowAnyOrigin() can not be used at the same time
                    );
             }
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}


app.UseStaticFiles(new StaticFileOptions()
{
    //FileProvider = new PhysicalFileProvider(@"c:/medias"),
    //RequestPath = new PathString("/assets/medias")
});
app.UseFileServer(new FileServerOptions()
{
    // configuration["ApplicationSettings:MEDIAS"].ToString() is c:/medias or d:/medias
    FileProvider = new PhysicalFileProvider(@"c:/medias"),
    RequestPath = new Microsoft.AspNetCore.Http.PathString(@"/assets/medias"),
    EnableDirectoryBrowsing = true // you make this true or false.
});

app.UseCors("CorsPolicy");
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;
// yitong
app.Run();
