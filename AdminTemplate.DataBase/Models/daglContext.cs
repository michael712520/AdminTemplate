using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdminTemplate.DataBase.Models
{
    public partial class daglContext : DbContext
    {
        public daglContext()
        {
        }

        public daglContext(DbContextOptions<daglContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MagFiedmanage> MagFiedmanage { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysUsermenu> SysUsermenu { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=192.168.70.250;uid=root;pwd=jinhe;port=3306;database=dagl;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<MagFiedmanage>(entity =>
            {
                entity.ToTable("mag_fiedmanage", "dagl");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Createtime).HasColumnName("createtime");

                entity.Property(e => e.Fiedgenres)
                    .HasColumnName("fiedgenres")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fiedid)
                    .HasColumnName("fiedid")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Fiedisopen)
                    .HasColumnName("fiedisopen")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fiedmenuid)
                    .HasColumnName("fiedmenuid")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Fiedname)
                    .HasColumnName("fiedname")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fiedpostfix)
                    .HasColumnName("fiedpostfix")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FiedsNo)
                    .HasColumnName("fiedsNo")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Fiedurl)
                    .HasColumnName("fiedurl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Namefied)
                    .HasColumnName("namefied")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ShareUrl)
                    .HasColumnName("shareUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Update).HasColumnName("update");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Year)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysMenu>(entity =>
            {
                entity.ToTable("sys_menu", "dagl");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Createtime).HasColumnName("createtime");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasColumnName("menuName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Pid)
                    .HasColumnName("pid")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("int(255)");

                entity.Property(e => e.Updatetime).HasColumnName("updatetime");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.ToTable("sys_user", "dagl");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Companyid)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Companyname)
                    .HasColumnName("companyname")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Createtime).HasColumnName("createtime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updatetime).HasColumnName("updatetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Userpwd)
                    .HasColumnName("userpwd")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysUsermenu>(entity =>
            {
                entity.ToTable("sys_usermenu", "dagl");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Menuid)
                    .HasColumnName("menuid")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
