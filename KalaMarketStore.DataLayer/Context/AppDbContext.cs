using KalaMarketStore.DataLayer.Entities;
using KalaMarketStore.DataLayer.Entities.Address;
using KalaMarketStore.DataLayer.Entities.Comments_Q_A;
using KalaMarketStore.DataLayer.Entities.Discount;
using KalaMarketStore.DataLayer.Entities.OfferCodes;
using KalaMarketStore.DataLayer.Entities.Products;
using KalaMarketStore.DataLayer.Entities.Products.M_to_M;
using KalaMarketStore.DataLayer.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {


    }

    public DbSet<MainSlider> MainSliders { get; set; }
    public DbSet<ImageAdvertise> ImageAdvertises { get; set; }


    #region RolePermission

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }


    #endregion


    #region Comments_Q_A
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Question> Questions { get; set; }
    #endregion


    #region Product

    public DbSet<Category_Property> Category_Properties { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }
    public DbSet<ProductGallery> ProductGalleries { get; set; }
    public DbSet<ProductGuarantee> ProductGuarantees { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyValue> PropertyValues { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ProductPrice> ProductPrices { get; set; }


    #endregion


    #region Address

    public DbSet<Province> Provinces { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }

    #endregion


    #region Cart

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartDeatail> CartDeatails { get; set; }

    #endregion


    #region DisCount

    public DbSet<DisCount> DisCounts { get; set; }
    public DbSet<UserDisCount> UserDisCounts { get; set; }

    #endregion






    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
    //    {
    //        relationship.DeleteBehavior = DeleteBehavior.Restrict;
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;

        base.OnModelCreating(modelBuilder);
    }


    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
    //    {
    //        relationship.DeleteBehavior = DeleteBehavior.Restrict;
    //    }
    //    base.OnModelCreating(modelBuilder);
    //}
}

