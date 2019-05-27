using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdminTemplate.DataBase.Models
{
    public partial class questionContext : DbContext
    {
        public questionContext()
        {
        }

        public questionContext(DbContextOptions<questionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MbDetail> MbDetail { get; set; }
        public virtual DbSet<MbDetailItem> MbDetailItem { get; set; }
        public virtual DbSet<QtDetail> QtDetail { get; set; }
        public virtual DbSet<QtDetailItem> QtDetailItem { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=192.168.70.250;uid=root;pwd=jinhe;port=3306;database=question;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<MbDetail>(entity =>
            {
                entity.ToTable("mb_detail", "question");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(255)");

                entity.Property(e => e.Score).HasColumnType("double(11,0)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("int(255)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MbDetailItem>(entity =>
            {
                entity.ToTable("mb_detail_item", "question");

                entity.HasIndex(e => e.DetailId)
                    .HasName("mb_detail_item_ibfk_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Bcontemt).IsUnicode(false);

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.DetailId)
                    .HasColumnName("detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(255)");

                entity.Property(e => e.Score).HasColumnType("double(11,0)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("int(255)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.MbDetailItem)
                    .HasForeignKey(d => d.DetailId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("mb_detail_item_ibfk_1");
            });

            modelBuilder.Entity<QtDetail>(entity =>
            {
                entity.ToTable("qt_detail", "question");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(255)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("int(255)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QtDetailItem>(entity =>
            {
                entity.ToTable("qt_detail_item", "question");

                entity.HasIndex(e => e.MbDetailItemId)
                    .HasName("mb_detail_item_id");

                entity.HasIndex(e => e.QtDetailId)
                    .HasName("qt_detail_item_ibfk_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Bcontemt).IsUnicode(false);

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.MbDetailItemId)
                    .HasColumnName("mb_detail_item_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(255)");

                entity.Property(e => e.QtDetailId)
                    .HasColumnName("qt_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Score).HasColumnType("double(11,0)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("int(255)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.MbDetailItem)
                    .WithMany(p => p.QtDetailItem)
                    .HasForeignKey(d => d.MbDetailItemId)
                    .HasConstraintName("qt_detail_item_ibfk_2");

                entity.HasOne(d => d.QtDetail)
                    .WithMany(p => p.QtDetailItem)
                    .HasForeignKey(d => d.QtDetailId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("qt_detail_item_ibfk_1");
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.ToTable("sys_user", "question");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Createby)
                    .HasColumnName("createby")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Openid)
                    .HasColumnName("openid")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
