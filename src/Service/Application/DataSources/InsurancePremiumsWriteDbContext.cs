using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Service.Application.DataSources;

public partial class InsurancePremiumsWriteDbContext : DbContext
{
    public InsurancePremiumsWriteDbContext()
    {
    }

    public InsurancePremiumsWriteDbContext(DbContextOptions<InsurancePremiumsWriteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coverage> Coverages { get; set; }

    public virtual DbSet<InvestmentRequest> InvestmentRequests { get; set; }

    public virtual DbSet<InvestmentValue> InvestmentValues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Database=InsurancePremiums;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coverage>(entity =>
        {
            entity.ToTable("Coverage");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Rate).HasColumnType("decimal(10, 4)");
        });

        modelBuilder.Entity<InvestmentRequest>(entity =>
        {
            entity.ToTable("InvestmentRequest");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Title).HasMaxLength(1024);
        });

        modelBuilder.Entity<InvestmentValue>(entity =>
        {
            entity.ToTable("InvestmentValue");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Coverage).WithMany(p => p.InvestmentValues)
                .HasForeignKey(d => d.CoverageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestmentValue_Coverage");

            entity.HasOne(d => d.InvestmentRequest).WithMany(p => p.InvestmentValues)
                .HasForeignKey(d => d.InvestmentRequestId)
                .HasConstraintName("FK_InvestmentValue_InvestmentRequest");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
