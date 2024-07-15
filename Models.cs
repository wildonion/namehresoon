
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<MailInfo> MailInfos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MailInfo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Mail).IsRequired();
            entity.Property(e => e.AppKey).IsRequired();
        });
    }
}



public class MailInfo
{
    public int Id { get; set; }
    public string Mail { get; set; }
    public string AppKey { get; set; }
}