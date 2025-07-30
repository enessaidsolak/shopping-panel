var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
   name: "ÜrünEkleme",
   pattern: "/yeni-urun",
   defaults: new { controller = "Product", action = "AddProducts" });

app.MapControllerRoute(
   name: "Ürünlerim",
   pattern: "/urunlerim",
   defaults: new { controller = "Product", action = "MyProducts" });

app.MapControllerRoute(
   name: "Kategorim",
   pattern: "/kategorim",
   defaults: new { controller = "Category", action = "MyCategory" });

app.MapControllerRoute(
   name: "KategoriEkleme",
   pattern: "/kategori-ekle",
   defaults: new { controller = "Category", action = "AddCategory" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
