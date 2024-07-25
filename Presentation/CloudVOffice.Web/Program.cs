using CloudVOffice.Web;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var logger = builder.Host.UseSerilog((hostingContext, loggerConfig) =>
                        loggerConfig
                            .ReadFrom.Configuration(hostingContext.Configuration));

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); // calling ConfigureServices method


var app = builder.Build();

startup.Configure(app, builder.Environment, Log.Logger);

//

//var startup = new Startup(builder.Configuration);
//startup.ConfigureServices(builder.Services); // calling ConfigureServices method




//var app = builder.Build();


//startup.Configure(app, builder.Environment);



// Add services to the container.

//var mvcCoreBuilder = builder.Services.AddMvcCore();



//foreach (var directory in _fileProvider.GetDirectories(uploadedPath))
//{
//    var moveTo = _fileProvider.Combine(pluginsDirectory, _fileProvider.GetDirectoryNameOnly(directory));

//    if (_fileProvider.DirectoryExists(moveTo))
//        _fileProvider.DeleteDirectory(moveTo);

//    _fileProvider.DirectoryMove(directory, moveTo);
//}

//Assembly assembly2 = Assembly.LoadFrom
//         (@".\Plugins\Accounts.Base\Accounts.Base.dll");
//var part2 = new AssemblyPart(assembly2);
//builder.Services.AddControllersWithViews().PartManager.ApplicationParts.Add(part2);

//builder.Services.AddMvc();
//builder.Services.AddControllers();
//builder.Services.AddRazorPages();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "area",
//    pattern: "{Area=Dashbaord}/{controller=Home}/{action=Index}/{id?}");




//app.Run();
//