using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Interfaces;
using Project.Service.Models;
using Project.Service.Services;

var builder = WebApplication.CreateBuilder(args);

Di.Init();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MonoZadatakContext") ?? throw new InvalidOperationException("Connection string 'MonoZadatakContext' not found."),
        b => b.MigrationsAssembly("Project.MVC") 
    )
);

//var kernel = new StandardKernel(new NinjectBindings());
//kernel.Bind<IVehicleService>().To<VehicleService>();
//builder.Services.AddScoped<IVehicleService, VehicleService>();

//builder.Services.AddSingleton<IKernel>(kernel);


builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<VehicleMake, VehicleMakeView>();
    mc.CreateMap<VehicleModel, VehicleModelView>();
    mc.CreateMap<VehicleModelView, VehicleModel>();
   // mc.CreateMap<VehicleMake, VMVehicle>();
    
    mc.CreateMap<VehicleModel, VMVehicle>();
   // mc.CreateMap<VehicleMake, VMVehicle>().ForMember(d => d.make, a=> a.MapFrom(s=> s));

});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
