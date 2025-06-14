using LicenseeRecords.Web.Services;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration
    .GetSection("ApiSettings")
    .GetValue<string>("BaseUrl") ?? "http://localhost:5267/api/";

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient<IAccountsService, AccountsService>(client =>
{
    client.BaseAddress = new Uri(connectionString);
});
builder.Services.AddHttpClient<IProductsService, ProductsService>(client =>
{
    client.BaseAddress = new Uri(connectionString);
});

builder.Services.AddScoped<IJSONService, JSONService>();

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
