using Auth.Applications;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp.Client.Extensions;
using WebApp.Service;
using Module.PMV.Core;

var builder = WebApplication.CreateBuilder(args);

var baseAddress = builder.Configuration.GetValue<string>("BaseUrl") ?? "";

// Add services to the container.
//add main core api
//builder.Services.AddApplication();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddService(baseAddress);

builder.Services
    .AddAuthApplication(builder.Configuration)
    .AddJwtValidation(builder.Configuration);

builder.Services.AddPMVCore(builder.Configuration)
    .AddAssetService();


builder.Services.AddAppExtension()
    .AddServiceExtension()
    .AddProviderExtension();

builder.Services.AddAuthorizationCore();

builder.Services.Configure<CookiePolicyOptions>(options => {
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthentication();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
