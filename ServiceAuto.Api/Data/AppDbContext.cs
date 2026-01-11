using Microsoft.EntityFrameworkCore;
using ServiceAuto.Api.Entities;

namespace ServiceAuto.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Client> Clienti => Set<Client>();
    public DbSet<Masina> Masini => Set<Masina>();
    public DbSet<Mecanic> Mecanici => Set<Mecanic>();
    public DbSet<Serviciu> Servicii => Set<Serviciu>();
    public DbSet<Programare> Programari => Set<Programare>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Masina>()
            .HasIndex(m => m.NrInmatriculare)
            .IsUnique();

        modelBuilder.Entity<Masina>()
            .HasOne(m => m.Client)
            .WithMany(c => c.Masini)
            .HasForeignKey(m => m.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Programare>()
            .HasOne(p => p.Masina)
            .WithMany(m => m.Programari)
            .HasForeignKey(p => p.MasinaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Programare>()
            .HasOne(p => p.Serviciu)
            .WithMany(s => s.Programari)
            .HasForeignKey(p => p.ServiciuId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Programare>()
            .HasOne(p => p.Mecanic)
            .WithMany(m => m.Programari)
            .HasForeignKey(p => p.MecanicId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
