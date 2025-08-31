using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoneyManagerApp.Presentation.Models;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=moneymanager;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountsId).HasName("accounts_pkey");

            entity.ToTable("accounts");

            entity.Property(e => e.AccountsId).HasColumnName("accounts_id");
            entity.Property(e => e.AccountsTitle)
                .HasMaxLength(255)
                .HasColumnName("accounts_title");
            entity.Property(e => e.FkUsersId).HasColumnName("fk_users_id");

            entity.HasOne(d => d.FkUsers).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.FkUsersId)
                .HasConstraintName("accounts_fk_users_id_fkey");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.GoalsId).HasName("goals_pkey");

            entity.ToTable("goals");

            entity.Property(e => e.GoalsId).HasColumnName("goals_id");
            entity.Property(e => e.FkAccountsId).HasColumnName("fk_accounts_id");
            entity.Property(e => e.GoalsAmounttocollect)
                .HasPrecision(18, 2)
                .HasColumnName("goals_amounttocollect");
            entity.Property(e => e.GoalsDescription).HasColumnName("goals_description");
            entity.Property(e => e.GoalsTitle)
                .HasMaxLength(255)
                .HasColumnName("goals_title");

            entity.HasOne(d => d.FkAccounts).WithMany(p => p.Goals)
                .HasForeignKey(d => d.FkAccountsId)
                .HasConstraintName("goals_fk_accounts_id_fkey");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionsId).HasName("transactions_pkey");

            entity.ToTable("transactions");

            entity.Property(e => e.TransactionsId).HasColumnName("transactions_id");
            entity.Property(e => e.FkAccountsIdFrom).HasColumnName("fk_accounts_id_from");
            entity.Property(e => e.FkAccountsIdTo).HasColumnName("fk_accounts_id_to");
            entity.Property(e => e.TransactionsDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("transactions_date");
            entity.Property(e => e.TransactionsDescription).HasColumnName("transactions_description");
            entity.Property(e => e.TransactionsSum)
                .HasPrecision(18, 2)
                .HasColumnName("transactions_sum");
            entity.Property(e => e.TransactionsType).HasColumnName("transactions_type");

            entity.HasOne(d => d.FkAccountsIdFromNavigation).WithMany(p => p.TransactionFkAccountsIdFromNavigations)
                .HasForeignKey(d => d.FkAccountsIdFrom)
                .HasConstraintName("transactions_fk_accounts_id_from_fkey");

            entity.HasOne(d => d.FkAccountsIdToNavigation).WithMany(p => p.TransactionFkAccountsIdToNavigations)
                .HasForeignKey(d => d.FkAccountsIdTo)
                .HasConstraintName("transactions_fk_accounts_id_to_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UsersId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UsersId).HasColumnName("users_id");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("password_salt");
            entity.Property(e => e.UsersEmail)
                .HasMaxLength(255)
                .HasColumnName("users_email");
            entity.Property(e => e.UsersName)
                .HasMaxLength(255)
                .HasColumnName("users_name");
            entity.Property(e => e.UsersPhonenumber)
                .HasMaxLength(40)
                .HasColumnName("users_phonenumber");
            entity.Property(e => e.UsersPhoto).HasColumnName("users_photo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
