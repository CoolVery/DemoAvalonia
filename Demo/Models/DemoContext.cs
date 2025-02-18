using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models;

public partial class DemoContext : DbContext
{
    public DemoContext()
    {
    }

    public DemoContext(DbContextOptions<DemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerProduct> PartnerProducts { get; set; }

    public virtual DbSet<PartnerType> PartnerTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Demo;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.MaterialTypeId).HasName("material_type_pkey");

            entity.ToTable("material_type");

            entity.Property(e => e.MaterialTypeId).HasColumnName("material_type_id");
            entity.Property(e => e.MaterialTypeDefectsPercent).HasColumnName("material_type_defects_percent");
            entity.Property(e => e.MaterialTypeName).HasColumnName("material_type_name");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.PartnerId).HasName("partner_pkey");

            entity.ToTable("partner");

            entity.Property(e => e.PartnerId).HasColumnName("partner_id");
            entity.Property(e => e.PartnerAdress).HasColumnName("partner_adress");
            entity.Property(e => e.PartnerDirectorCredentials).HasColumnName("partner_director_credentials");
            entity.Property(e => e.PartnerEmail).HasColumnName("partner_email");
            entity.Property(e => e.PartnerInn).HasColumnName("partner_inn");
            entity.Property(e => e.PartnerName).HasColumnName("partner_name");
            entity.Property(e => e.PartnerPhone).HasColumnName("partner_phone");
            entity.Property(e => e.PartnerRating).HasColumnName("partner_rating");
            entity.Property(e => e.PartnerTypeId).HasColumnName("partner_type_id");

            entity.HasOne(d => d.PartnerType).WithMany(p => p.Partners)
                .HasForeignKey(d => d.PartnerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partner_partner_type_id_fkey");
        });

        modelBuilder.Entity<PartnerProduct>(entity =>
        {
            entity.HasKey(e => e.PartnerProductId).HasName("partner_products_pkey");

            entity.ToTable("partner_products");

            entity.Property(e => e.PartnerProductId).HasColumnName("partner_product_id");
            entity.Property(e => e.PartnerId).HasColumnName("partner_id");
            entity.Property(e => e.PartnerProductAmount).HasColumnName("partner_product_amount");
            entity.Property(e => e.PartnerProductDate).HasColumnName("partner_product_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Partner).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partner_products_partner_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partner_products_product_id_fkey");
        });

        modelBuilder.Entity<PartnerType>(entity =>
        {
            entity.HasKey(e => e.PartnerTypeId).HasName("partner_type_pkey");

            entity.ToTable("partner_type");

            entity.Property(e => e.PartnerTypeId).HasColumnName("partner_type_id");
            entity.Property(e => e.PartnerTypeName).HasColumnName("partner_type_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductArticul).HasColumnName("product_articul");
            entity.Property(e => e.ProductMinPrice).HasColumnName("product_min_price");
            entity.Property(e => e.ProductName).HasColumnName("product_name");
            entity.Property(e => e.ProductTypeId).HasColumnName("product_type_id");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_product_type_id_fkey");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId).HasName("product_type_pkey");

            entity.ToTable("product_type");

            entity.Property(e => e.ProductTypeId).HasColumnName("product_type_id");
            entity.Property(e => e.ProductTypeName).HasColumnName("product_type_name");
            entity.Property(e => e.ProductTypeRatio).HasColumnName("product_type_ratio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
