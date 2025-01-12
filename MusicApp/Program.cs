using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor(); // HttpContext'e eri�im i�in gerekli

// Veritaban� ba�lant�s�n� yap�land�r
builder.Services.AddDbContext<MusicAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Session servislerini ekle
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi
    options.Cookie.HttpOnly = true; // G�venlik i�in yaln�zca sunucu eri�imi
    options.Cookie.IsEssential = true; // �erezin zorunlu oldu�unu belirt
});

// MVC servislerini ekle
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS y�nlendirme ve statik dosyalar
app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware s�ralamas�
app.UseRouting();
app.UseSession(); // Session middleware
app.UseAuthorization(); // Yetkilendirme

// Varsay�lan rotay� ayarla
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");



// Uygulamay� �al��t�r
app.Run();
