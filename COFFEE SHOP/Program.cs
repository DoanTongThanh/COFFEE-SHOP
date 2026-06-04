using COFFEE_SHOP.Data;
using COFFEE_SHOP.Models.Interface;
using COFFEE_SHOP.Models.Service;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<CoffeeshopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeShopDbContextConnection")));

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseSession();


app.Run();
