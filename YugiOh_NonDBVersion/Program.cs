using YugiOh_NonDBVersion.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

// Add services to the container.
builder.Services.AddControllersWithViews();

// // This will make it so that when we crequest a categoryRepository it will auto give us the implementation that we have defined
// // This will make it so we can also use ICategory rather than the ApplicationDbConext in our CategoryController
// // it will also make sure when we choose the ICategoryRepository, that interface will have the CategoryRepository class injected into it.
// builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//
builder.Services.AddRazorPages().AddRazorRuntimeCompilation(); //This does not always need to be added due to the hot reload

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