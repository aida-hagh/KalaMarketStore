using KalaMarketStore.Core.Service.Address;
using KalaMarketStore.Core.Service.Brand;
using KalaMarketStore.Core.Service.Cart;
using KalaMarketStore.Core.Service.Category;
using KalaMarketStore.Core.Service.City;
using KalaMarketStore.Core.Service.Color;
using KalaMarketStore.Core.Service.DisCount;
using KalaMarketStore.Core.Service.Guarantee;
using KalaMarketStore.Core.Service.MainSlider;
using KalaMarketStore.Core.Service.Permission;
using KalaMarketStore.Core.Service.Product;
using KalaMarketStore.Core.Service.Property;
using KalaMarketStore.Core.Service.PropertyValue;
using KalaMarketStore.Core.Service.Province;
using KalaMarketStore.Core.Service.QuestionAnswer;
using KalaMarketStore.Core.Service.Review;
using KalaMarketStore.Core.Service.RolePermission;
using KalaMarketStore.Core.Service.User;
using KalaMarketStore.Core.Service.UserRole;
using KalaMarketStore.DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using static Kalamarket.Core.ExtentionMethod.RenderEmail;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



// Add AddDbContext

builder.Services.AddDbContext<AppDbContext>( 
    x => x.UseSqlServer(builder.Configuration.GetConnectionString("connection")));




builder.Services.AddAuthentication(o => {
    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}
)
    .AddCookie(options => {
        options.LoginPath = "/Account/Login/";
        options.AccessDeniedPath = "/Account/Login/";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(100);
    });



#region AddScoped(IOC)

builder.Services.AddScoped<IMainSliderService, MainSliderService>();
builder.Services.AddScoped<ICategoryService,CategoryService >();
builder.Services.AddScoped<IGuaranteeService, GuaranteeService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IPropertyValueService, PropertyValueService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IViewRenderService, RenderViewToString>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IDisCountService, DisCountService>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();


#endregion



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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
