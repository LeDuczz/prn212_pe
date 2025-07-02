using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ResearchProjectManagement.DAL.Entities;

public partial class Su25researchDbContext : DbContext
{
    public Su25researchDbContext()
    {
    }

    public Su25researchDbContext(DbContextOptions<Su25researchDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ResearchProject> ResearchProjects { get; set; }

    public virtual DbSet<Researcher> Researchers { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ResearchProject>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Research__761ABED0EF4AC289");

            entity.ToTable("ResearchProject");

            entity.Property(e => e.ProjectId)
                .ValueGeneratedNever()
                .HasColumnName("ProjectID");
            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LeadResearcherId).HasColumnName("LeadResearcherID");
            entity.Property(e => e.ProjectTitle).HasMaxLength(200);
            entity.Property(e => e.ResearchField).HasMaxLength(100);

            entity.HasOne(d => d.LeadResearcher).WithMany(p => p.ResearchProjects)
                .HasForeignKey(d => d.LeadResearcherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ResearchP__LeadR__3F466844");
        });

        modelBuilder.Entity<Researcher>(entity =>
        {
            entity.HasKey(e => e.ResearcherId).HasName("PK__Research__7CC06F05123FC40A");

            entity.ToTable("Researcher");

            entity.HasIndex(e => e.Email, "UQ__Research__A9D105341F3441C8").IsUnique();

            entity.Property(e => e.ResearcherId)
                .ValueGeneratedNever()
                .HasColumnName("ResearcherID");
            entity.Property(e => e.Affiliation).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Expertise).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAcco__1788CCACA8F62451");

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.Email, "UQ__UserAcco__A9D1053482259EA0").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(60);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
