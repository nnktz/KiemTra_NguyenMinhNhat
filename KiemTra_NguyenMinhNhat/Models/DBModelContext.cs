using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KiemTra_NguyenMinhNhat.Models
{
    public partial class DBModelContext : DbContext
    {
        public DBModelContext()
            : base("name=DBModelContext")
        {
        }

        public virtual DbSet<DangKy> DangKies { get; set; }
        public virtual DbSet<HocPhan> HocPhans { get; set; }
        public virtual DbSet<NganhHoc> NganhHocs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DangKy>()
                .Property(e => e.MaSV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DangKy>()
                .HasMany(e => e.HocPhans)
                .WithMany(e => e.DangKies)
                .Map(m => m.ToTable("ChiTietDangKy").MapLeftKey("MaDK").MapRightKey("MaHP"));

            modelBuilder.Entity<HocPhan>()
                .Property(e => e.MaHP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NganhHoc>()
                .Property(e => e.MaNganh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MaSV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.Hinh)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MaNganh)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
