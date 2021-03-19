using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class LibreriaContext : DbContext
    {
        public LibreriaContext()
        {
        }

        public LibreriaContext(DbContextOptions<LibreriaContext> options)
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
                optionsBuilder.UseSqlServer("data source=chiyuserver.database.windows.net;Database=ChiYuDatabase;user id=eric861129;password=@Libreria;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("建立日期");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sort).HasComment("排序");

                entity.Property(e => e.Updatetime)
                    .HasColumnType("datetime")
                    .HasComment("更新日期");
            });

            modelBuilder.Entity<Exhibition>(entity =>
            {
                entity.ToTable("Exhibition");

                entity.Property(e => e.ExhibitionId).HasColumnName("ExhibitionID");

                entity.Property(e => e.EditModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ExName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExPhoto).IsRequired();

                entity.Property(e => e.ExhibitionEndTime).HasColumnType("datetime");

                entity.Property(e => e.ExhibitionIntro).IsRequired();

                entity.Property(e => e.ExhibitionPrice).HasColumnType("money");

                entity.Property(e => e.ExhibitionStartTime).HasColumnType("datetime");

                entity.Property(e => e.MasterUnit)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ExhibitionCustomer>(entity =>
            {
                entity.HasKey(e => e.ExCustomerId);

                entity.ToTable("ExhibitionCustomer");

                entity.Property(e => e.ExCustomerEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ExCustomerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ExCustomerPhone)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ExhibitionOrder>(entity =>
            {
                entity.HasKey(e => e.ExOrderId);

                entity.ToTable("ExhibitionOrder");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.ExCustomer)
                    .WithMany(p => p.ExhibitionOrders)
                    .HasForeignKey(d => d.ExCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExhibitionOrder_ExhibitionCustomer");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.ToTable("Favorite");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

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

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.TypeInfo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("Manager");

                entity.Property(e => e.ManagerId).HasMaxLength(128);

                entity.Property(e => e.JobTitle).HasMaxLength(50);

                entity.Property(e => e.ManagerName).HasMaxLength(50);

                entity.Property(e => e.ManagerPassword).HasMaxLength(50);

                entity.Property(e => e.ManagerUsername).HasMaxLength(50);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HomeNumber).HasMaxLength(50);

                entity.Property(e => e.Idnumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("IDnumber");

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("memberName");

                entity.Property(e => e.MemberPassword)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("memberPassword")
                    .HasComment("會員密碼");

                entity.Property(e => e.MemberUserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("memberUserName")
                    .HasComment("會員帳號");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_member_Role");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("建立日期");

                entity.Property(e => e.InvoiceInfo)
                    .HasMaxLength(50)
                    .IsFixedLength(true)
                    .HasComment("發票號碼");

                entity.Property(e => e.InvoiceType).HasComment("發票種類 1為手機條碼載具 2為自然人憑載具 3為紙本證明連 4為捐贈發票");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasComment("訂購日期");

                entity.Property(e => e.PaymentType).HasComment("付款方式 1為取貨付款 2為ATM 3為信用卡");

                entity.Property(e => e.ShipAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("發貨地址");

                entity.Property(e => e.ShipCity)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("發貨城市");

                entity.Property(e => e.ShipName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShipPostalCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("郵政編碼");

                entity.Property(e => e.ShipRegion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("發貨地區");

                entity.Property(e => e.ShippingDate)
                    .HasColumnType("datetime")
                    .HasComment("發貨日期");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasComment("更新日期");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Customer");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

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
                entity.ToTable("Preview");

                entity.Property(e => e.ImgUrl)
                    .IsRequired()
                    .HasComment("圖片");

                entity.Property(e => e.Sort).HasComment("0為主圖,之後為預覽圖");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Previews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Preview_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("作者");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("建立日期");

                entity.Property(e => e.Inventory).HasComment("庫存");

                //entity.Property(e => e.IsFav).HasColumnName("isFav");

                entity.Property(e => e.IsSpecial).HasColumnName("isSpecial");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PublishDate)
                    .HasColumnType("date")
                    .HasComment("出版日期");

                entity.Property(e => e.Sort).HasComment("排序");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasComment("更新日期");

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

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("ShoppingCart");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

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
                entity.ToTable("Supplier");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Sort).HasComment("排序");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
