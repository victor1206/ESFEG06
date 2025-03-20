using System;
using System.Collections.Generic;
using ESFEG06.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESFEG06.DataAccess;

public partial class QuotationContext : DbContext
{
    public QuotationContext()
    {
    }

    public QuotationContext(DbContextOptions<QuotationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Quotation> Quotations { get; set; }

    public virtual DbSet<QuotationDetail> QuotationDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=VictorDuran;Initial Catalog=dbmyquotations;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__brands__5E5A8E27E57F876B");

            entity.ToTable("brands");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("brand_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF5A419AC44");

            entity.ToTable("products");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.PriceUnitPurchase)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price_unit_purchase");
            entity.Property(e => e.PriceUnitSale)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price_unit_sale");
            entity.Property(e => e.ProductCode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("product_code");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("product_description");
            entity.Property(e => e.ProductImage)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("product_image");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductStatus).HasColumnName("product_status");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("supplier_name");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__products__brand___3E52440B");
        });

        modelBuilder.Entity<Quotation>(entity =>
        {
            entity.HasKey(e => e.QuotationId).HasName("PK__quotatio__7841D7DB00EE08BB");

            entity.ToTable("quotations");

            entity.Property(e => e.QuotationId).HasColumnName("quotation_id");
            entity.Property(e => e.ClientName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("client_name");
            entity.Property(e => e.ClientPhone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("client_phone");
            entity.Property(e => e.PaymentMethodName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("payment_method_name");
            entity.Property(e => e.QuotationNumber).HasColumnName("quotation_number");
            entity.Property(e => e.QuotationRegistration)
                .HasColumnType("datetime")
                .HasColumnName("quotation_registration");
            entity.Property(e => e.QuotationStatus).HasColumnName("quotation_status");
            entity.Property(e => e.SellerName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("seller_name");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ValidityDays).HasColumnName("validity_days");

            entity.HasOne(d => d.User).WithMany(p => p.Quotations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__quotation__user___412EB0B6");
        });

        modelBuilder.Entity<QuotationDetail>(entity =>
        {
            entity.HasKey(e => e.QuotationDetailId).HasName("PK__quotatio__131ABC602A14432A");

            entity.ToTable("quotation_details");

            entity.Property(e => e.QuotationDetailId).HasColumnName("quotation_detail_id");
            entity.Property(e => e.Discount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("discount");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.QuotationId).HasColumnName("quotation_id");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.Product).WithMany(p => p.QuotationDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__quotation__produ__44FF419A");

            entity.HasOne(d => d.Quotation).WithMany(p => p.QuotationDetails)
                .HasForeignKey(d => d.QuotationId)
                .HasConstraintName("FK__quotation__quota__440B1D61");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__roles__CF32E443C03A320B");

            entity.ToTable("roles");

            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.RolName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("rol_name");
            entity.Property(e => e.RolStatus).HasColumnName("rol_status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F3076CB4B");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("user_name");
            entity.Property(e => e.UserNickname)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("user_nickname");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_password");
            entity.Property(e => e.UserStatus).HasColumnName("user_status");

            entity.HasOne(d => d.Rol).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users__rol_id__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
