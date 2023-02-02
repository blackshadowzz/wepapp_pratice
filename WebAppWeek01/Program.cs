using Microsoft.Extensions.FileProviders;
using WebAppWeek01.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<appDbContext>();

 //Add Request pipeline
var app = builder.Build();


//Middleware: Add Static File
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(builder.Environment.ContentRootPath, "Example")),
//    RequestPath = "/Example/Ex"
//}); 
//Middleware 
app.UseRouting();


app.MapDefaultControllerRoute();

app.MapControllerRoute("home", pattern: "{controller=Employee}/{action=Index}/{id?}");


app.Run();
