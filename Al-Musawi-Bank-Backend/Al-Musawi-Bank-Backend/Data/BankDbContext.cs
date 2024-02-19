using Microsoft.EntityFrameworkCore;
using Al_Musawi_Bank_Backend.Models;
public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Transfer> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define relationships, indexes, and other constraints here

        modelBuilder.Entity<User>()
            .HasMany(u => u.Accounts)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Account>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId);

        // If you're using a Transfer entity that references Accounts
        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.FromAccount)
            .WithMany() // Assuming you don't need to navigate back to Transfer from Account
            .HasForeignKey(t => t.FromAccountId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.ToAccount)
            .WithMany()
            .HasForeignKey(t => t.ToAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        // Add any additional model configuration here
    }
}