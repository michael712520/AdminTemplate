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

        public virtual DbSet<LatitudeCategory> LatitudeCategory { get; set; }
        public virtual DbSet<LatitudeDetail> LatitudeDetail { get; set; }
        public virtual DbSet<LatitudeDetailItem> LatitudeDetailItem { get; set; }
        public virtual DbSet<LatitudeGrade> LatitudeGrade { get; set; }
        public virtual DbSet<MbDetail> MbDetail { get; set; }
        public virtual DbSet<MbDetailItem> MbDetailItem { get; set; }
        public virtual DbSet<QtDetail> QtDetail { get; set; }
        public virtual DbSet<QtDetailItem> QtDetailItem { get; set; }
        public virtual DbSet<QtDetailbatch> QtDetailbatch { get; set; }
        public virtual DbSet<QtLatitudeDetail> QtLatitudeDetail { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=47.107.238.102;uid=root;pwd=000000;port=3306;database=question;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<LatitudeCategory>(entity =>
            {
                entity.ToTable("latitude_category", "question");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.LatitudeDetails)
                    .HasColumnName("latitude_details")
                    .IsUnicode(false);

                entity.Property(e => e.MbDetailId)
                    .HasColumnName("mb_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LatitudeDetail>(entity =>
            {
                entity.ToTable("latitude_detail", "question");

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BaseScore)
                    .HasColumnName("base_score")
                    .HasColumnType("double(11,2)");

                entity.Property(e => e.Coefficient)
                    .HasColumnName("coefficient")
                    .HasColumnType("double(11,2)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.MbDetailId)
                    .HasColumnName("mb_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Pattern)
                    .HasColumnName("pattern")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Relationship)
                    .HasColumnName("relationship")
                    .IsUnicode(false);

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("double(11,2)");

                entity.Property(e => e.Sort)
                    .HasColumnName("sort")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<LatitudeDetailItem>(entity =>
            {
                entity.ToTable("latitude_detail_item", "question");

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BaseScore)
                    .HasColumnName("base_score")
                    .HasColumnType("double(11,2)");

                entity.Property(e => e.Coefficient)
                    .HasColumnName("coefficient")
                    .HasColumnType("double(11,2)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.MbDetailId)
                    .HasColumnName("mb_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ReturnInfo)
                    .HasColumnName("returnInfo")
                    .IsUnicode(false);

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("double(11,2)");

                entity.Property(e => e.Sort)
                    .HasColumnName("sort")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<LatitudeGrade>(entity =>
            {
                entity.ToTable("latitude_grade", "question");

                entity.HasIndex(e => e.LatitudeDetailId)
                    .HasName("mb_grade_ibfk_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DownScore)
                    .HasColumnName("downScore")
                    .HasColumnType("double(255,2)");

                entity.Property(e => e.LatitudeDetailId)
                    .HasColumnName("latitude_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Titile)
                    .HasColumnName("titile")
                    .IsUnicode(false);

                entity.Property(e => e.UpScore)
                    .HasColumnName("upScore")
                    .HasColumnType("double(255,2)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.LatitudeDetail)
                    .WithMany(p => p.LatitudeGrade)
                    .HasForeignKey(d => d.LatitudeDetailId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("latitude_grade_ibfk_1");
            });

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

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Fee)
                    .HasColumnName("fee")
                    .HasColumnType("double(11,0)");

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

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

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

                entity.HasIndex(e => e.LatitudeDetailId)
                    .HasName("latitude_detail_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Bcontemt).IsUnicode(false);

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DetailId)
                    .HasColumnName("detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.LatitudeDetailId)
                    .HasColumnName("latitude_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.LatitudeDetailIds)
                    .HasColumnName("latitudeDetailIds")
                    .IsUnicode(false);

                entity.Property(e => e.LatitudeDetailName)
                    .HasColumnName("latitude_detail_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(255)");

                entity.Property(e => e.PageInfo)
                    .HasColumnName("pageInfo")
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

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.MbDetailItem)
                    .HasForeignKey(d => d.DetailId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("mb_detail_item_ibfk_1");

                entity.HasOne(d => d.LatitudeDetail)
                    .WithMany(p => p.MbDetailItem)
                    .HasForeignKey(d => d.LatitudeDetailId)
                    .HasConstraintName("mb_detail_item_ibfk_2");
            });

            modelBuilder.Entity<QtDetail>(entity =>
            {
                entity.ToTable("qt_detail", "question");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CallBack)
                    .HasColumnName("callBack")
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Fee)
                    .HasColumnName("fee")
                    .HasColumnType("double(11,0)");

                entity.Property(e => e.ForeignType)
                    .HasColumnName("foreignType")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MbDetailId)
                    .HasColumnName("mb_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Openid)
                    .HasColumnName("openid")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(255)");

                entity.Property(e => e.Score).HasColumnType("double(11,0)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("int(255)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.StudentIdCard)
                    .HasColumnName("studentIdCard")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherIdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QtDetailItem>(entity =>
            {
                entity.ToTable("qt_detail_item", "question");

                entity.HasIndex(e => e.LatitudeDetailItemId)
                    .HasName("latitude_detail_id");

                entity.HasIndex(e => e.MbDetailId)
                    .HasName("mb_detail_item_ibfk_1");

                entity.HasIndex(e => e.QtDetailId)
                    .HasName("qt_detail_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Bcontemt).IsUnicode(false);

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.LatitudeDetailIds)
                    .HasColumnName("latitudeDetailIds")
                    .IsUnicode(false);

                entity.Property(e => e.LatitudeDetailItemId)
                    .HasColumnName("latitude_detail_item_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.LatitudeDetailItemName)
                    .HasColumnName("latitude_detail_item_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MbDetailId)
                    .HasColumnName("mb_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(255)");

                entity.Property(e => e.PageInfo)
                    .HasColumnName("pageInfo")
                    .IsUnicode(false);

                entity.Property(e => e.QtDetailId)
                    .HasColumnName("qt_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Score).HasColumnType("double(11,0)");

                entity.Property(e => e.SelectResult)
                    .HasColumnName("select_result")
                    .IsUnicode(false);

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

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.LatitudeDetailItem)
                    .WithMany(p => p.QtDetailItem)
                    .HasForeignKey(d => d.LatitudeDetailItemId)
                    .HasConstraintName("qt_detail_item_ibfk_2");

                entity.HasOne(d => d.MbDetail)
                    .WithMany(p => p.QtDetailItem)
                    .HasForeignKey(d => d.MbDetailId)
                    .HasConstraintName("qt_detail_item_ibfk_4");

                entity.HasOne(d => d.QtDetail)
                    .WithMany(p => p.QtDetailItem)
                    .HasForeignKey(d => d.QtDetailId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("qt_detail_item_ibfk_3");
            });

            modelBuilder.Entity<QtDetailbatch>(entity =>
            {
                entity.ToTable("qt_detailbatch", "question");

                entity.HasIndex(e => e.QtDetailId)
                    .HasName("qt_detailbatch_ibfk_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BatchNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Fee)
                    .HasColumnName("fee")
                    .HasColumnType("double(11,0)");

                entity.Property(e => e.ForeignType)
                    .HasColumnName("foreignType")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Openid)
                    .HasColumnName("openid")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.QtDetailId)
                    .HasColumnName("qt_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.SignerName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StudentIdCard)
                    .HasColumnName("studentIdCard")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherIdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.QtDetail)
                    .WithMany(p => p.QtDetailbatch)
                    .HasForeignKey(d => d.QtDetailId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("qt_detailbatch_ibfk_1");
            });

            modelBuilder.Entity<QtLatitudeDetail>(entity =>
            {
                entity.ToTable("qt_latitude_detail", "question");

                entity.HasIndex(e => e.LatitudeDetailId)
                    .HasName("latitude_detail_id");

                entity.HasIndex(e => e.QtDetailId)
                    .HasName("qt_latitude_detail_ibfk_2");

                entity.HasIndex(e => e.QtDetailbatchId)
                    .HasName("qt_latitude_detail_ibfk_3");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BatchNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LatitudeDetailId)
                    .HasColumnName("latitude_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.QtDetailId)
                    .HasColumnName("qt_detail_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.QtDetailbatchId)
                    .HasColumnName("qt_detailbatch_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("double(11,0)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.LatitudeDetail)
                    .WithMany(p => p.QtLatitudeDetail)
                    .HasForeignKey(d => d.LatitudeDetailId)
                    .HasConstraintName("qt_latitude_detail_ibfk_1");

                entity.HasOne(d => d.QtDetail)
                    .WithMany(p => p.QtLatitudeDetail)
                    .HasForeignKey(d => d.QtDetailId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("qt_latitude_detail_ibfk_2");

                entity.HasOne(d => d.QtDetailbatch)
                    .WithMany(p => p.QtLatitudeDetail)
                    .HasForeignKey(d => d.QtDetailbatchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("qt_latitude_detail_ibfk_3");
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.ToTable("sys_user", "question");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Createby)
                    .HasColumnName("createby")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.NickName)
                    .HasColumnName("nickName")
                    .HasMaxLength(255)
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

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
