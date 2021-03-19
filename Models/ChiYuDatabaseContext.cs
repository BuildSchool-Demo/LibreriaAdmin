using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class ChiYuDatabaseContext : DbContext
    {
        public ChiYuDatabaseContext()
        {
        }

        public ChiYuDatabaseContext(DbContextOptions<ChiYuDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Exhibition> Exhibitions { get; set; }
        public virtual DbSet<ExhibitionCustomer> ExhibitionCustomers { get; set; }
        public virtual DbSet<ExhibitionOrder> ExhibitionOrders { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Preview> Previews { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=chiyuserver.database.windows.net;initial catalog=ChiYuDatabase;user id=eric861129;password=@Libreria;MultipleActiveResultSets=True;App=EntityFramework");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CreateTime).HasComment("建立日期");

                entity.Property(e => e.Sort).HasComment("排序");

                entity.Property(e => e.Updatetime).HasComment("更新日期");
            });

            modelBuilder.Entity<Exhibition>(entity =>
            {
                entity.Property(e => e.ExName).HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ExhibitionOrder>(entity =>
            {
                entity.HasOne(d => d.ExCustomer)
                    .WithMany(p => p.ExhibitionOrders)
                    .HasForeignKey(d => d.ExCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExhibitionOrder_ExhibitionCustomer");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Favorite_member");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Favorite_Product");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.Property(e => e.ManagerId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberPassword).HasComment("會員密碼");

                entity.Property(e => e.MemberUserName).HasComment("會員帳號");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_member_Role");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreateTime).HasComment("建立日期");

                entity.Property(e => e.InvoiceInfo)
                    .IsFixedLength(true)
                    .HasComment("發票號碼");

                entity.Property(e => e.InvoiceType).HasComment("發票種類 1為手機條碼載具 2為自然人憑載具 3為紙本證明連 4為捐贈發票");

                entity.Property(e => e.OrderDate).HasComment("訂購日期");

                entity.Property(e => e.PaymentType).HasComment("付款方式 1為取貨付款 2為ATM 3為信用卡");

                entity.Property(e => e.ShipAddress).HasComment("發貨地址");

                entity.Property(e => e.ShipCity).HasComment("發貨城市");

                entity.Property(e => e.ShipPostalCode).HasComment("郵政編碼");

                entity.Property(e => e.ShipRegion).HasComment("發貨地區");

                entity.Property(e => e.ShippingDate).HasComment("發貨日期");

                entity.Property(e => e.UpdateTime).HasComment("更新日期");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Customer");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Quantity).HasComment("數量");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetailId_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetailId_Product");
            });

            modelBuilder.Entity<Preview>(entity =>
            {
                entity.Property(e => e.ImgUrl).HasComment("圖片");

                entity.Property(e => e.Sort).HasComment("0為主圖,之後為預覽圖");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Previews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Preview_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Author).HasComment("作者");

                entity.Property(e => e.CreateTime).HasComment("建立日期");

                entity.Property(e => e.Inventory).HasComment("庫存");

                entity.Property(e => e.PublishDate).HasComment("出版日期");

                entity.Property(e => e.Sort).HasComment("排序");

                entity.Property(e => e.UpdateTime).HasComment("更新日期");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Supplier");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_member");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_Product");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Sort).HasComment("排序");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
