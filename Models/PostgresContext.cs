using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace coreAPI_banking_app.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Instrument> Instruments { get; set; }

    public virtual DbSet<Portfolio> Portfolios { get; set; }

    public virtual DbSet<Trade> Trades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Clientid).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Accounttype)
                .HasMaxLength(50)
                .HasColumnName("accounttype");
            entity.Property(e => e.Contactnumber)
                .HasMaxLength(15)
                .HasColumnName("contactnumber");
            entity.Property(e => e.Createdon)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdon");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
        });

        modelBuilder.Entity<Instrument>(entity =>
        {
            entity.HasKey(e => e.Instrumentid).HasName("instruments_pkey");

            entity.ToTable("instruments");

            entity.Property(e => e.Instrumentid).HasColumnName("instrumentid");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .HasColumnName("currency");
            entity.Property(e => e.Market)
                .HasMaxLength(50)
                .HasColumnName("market");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Tickersymbol)
                .HasMaxLength(15)
                .HasColumnName("tickersymbol");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Portfolio>(entity =>
        {
            entity.HasKey(e => e.Portfolioid).HasName("portfolios_pkey");

            entity.ToTable("portfolios");

            entity.Property(e => e.Portfolioid).HasColumnName("portfolioid");
            entity.Property(e => e.Averagecost)
                .HasPrecision(12, 2)
                .HasColumnName("averagecost");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Instrumentid).HasColumnName("instrumentid");
            entity.Property(e => e.Lastupdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastupdated");
            entity.Property(e => e.Quantityheld).HasColumnName("quantityheld");

            entity.HasOne(d => d.Client).WithMany(p => p.Portfolios)
                .HasForeignKey(d => d.Clientid)
                .HasConstraintName("portfolios_clientid_fkey");

            entity.HasOne(d => d.Instrument).WithMany(p => p.Portfolios)
                .HasForeignKey(d => d.Instrumentid)
                .HasConstraintName("portfolios_instrumentid_fkey");
        });

        modelBuilder.Entity<Trade>(entity =>
        {
            entity.HasKey(e => e.Tradeid).HasName("trades_pkey");

            entity.ToTable("trades");

            entity.Property(e => e.Tradeid).HasColumnName("tradeid");
            entity.Property(e => e.Brokername)
                .HasMaxLength(100)
                .HasColumnName("brokername");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Instrumentid).HasColumnName("instrumentid");
            entity.Property(e => e.Priceperunit)
                .HasPrecision(12, 2)
                .HasColumnName("priceperunit");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Settlementdate).HasColumnName("settlementdate");
            entity.Property(e => e.Tradedate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("tradedate");
            entity.Property(e => e.Tradestatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDING'::character varying")
                .HasColumnName("tradestatus");
            entity.Property(e => e.Tradetype)
                .HasMaxLength(10)
                .HasColumnName("tradetype");

            entity.HasOne(d => d.Client).WithMany(p => p.Trades)
                .HasForeignKey(d => d.Clientid)
                .HasConstraintName("trades_clientid_fkey");

            entity.HasOne(d => d.Instrument).WithMany(p => p.Trades)
                .HasForeignKey(d => d.Instrumentid)
                .HasConstraintName("trades_instrumentid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
