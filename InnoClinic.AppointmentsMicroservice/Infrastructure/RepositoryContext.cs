using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure;

public class RepositoryContext : DbContext
{
	public DbSet<Appointment> Appointments { get; set; }
	public DbSet<Result> Results { get; set; }

	public RepositoryContext(DbContextOptions options)
		: base(options)
	{
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<Appointment>().HasKey(e => e.Id);
		builder.Entity<Appointment>().Property(e => e.Id);
		builder.Entity<Appointment>().Property(e => e.Date).HasColumnType("date");
		builder.Entity<Appointment>().Property(e => e.Time).HasColumnType("time");
        builder.Entity<Result>().HasKey(e => e.Id);
		builder.Entity<Result>().Property(e => e.Id);
        builder.Entity<Result>().HasOne(e => e.Appointment).WithOne(e => e.Result);
	}
}
