using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ngk.DataAccess.Entities
{
    public partial class CoreServiceContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<BlackContract> BlackContract { get; set; }
        public virtual DbSet<Captcha> Captcha { get; set; }
        public virtual DbSet<ChainConfig> ChainConfig { get; set; }
        public virtual DbSet<ConfigData> ConfigData { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<ContactsAccount> ContactsAccount { get; set; }
        public virtual DbSet<CreateAccountRecord> CreateAccountRecord { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<LoginLog> LoginLog { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Node> Node { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<TransferRecord> TransferRecord { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Version> Version { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Access).HasDefaultValueSql("''");

                entity.Property(e => e.IsCheck).HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.PublicKey).HasDefaultValueSql("''");

                entity.Property(e => e.State).HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Code).HasDefaultValueSql("''");
            });

            modelBuilder.Entity<BlackContract>(entity =>
            {
                entity.Property(e => e.State).HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Captcha>(entity =>
            {
                entity.Property(e => e.CountryCode).HasDefaultValueSql("''");

                entity.Property(e => e.State).HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<ChainConfig>(entity =>
            {
                entity.Property(e => e.DefaultNode).HasDefaultValueSql("''");

                entity.Property(e => e.DeleteIp).HasDefaultValueSql("''");

                entity.Property(e => e.State).HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<ConfigData>(entity =>
            {
                entity.Property(e => e.IsApp).HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.Remark).HasDefaultValueSql("''");

                entity.Property(e => e.State).HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.Property(e => e.Desc).HasDefaultValueSql("''");

                entity.Property(e => e.Mobile).HasDefaultValueSql("''");

                entity.Property(e => e.State).HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<ContactsAccount>(entity =>
            {
                entity.Property(e => e.State).HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<CreateAccountRecord>(entity =>
            {
                entity.Property(e => e.Access).HasDefaultValueSql("''");

                entity.Property(e => e.ClientIp).HasDefaultValueSql("''");

                entity.Property(e => e.PublicKey).HasDefaultValueSql("''");

                entity.Property(e => e.Remark).HasDefaultValueSql("''");

                entity.Property(e => e.State).HasDefaultValueSql("'1'");

                entity.Property(e => e.Uuid).HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.BusinessState).HasDefaultValueSql("'0'");

                entity.Property(e => e.Remark).HasDefaultValueSql("''");

                entity.Property(e => e.Type).HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Code).HasDefaultValueSql("''");

                entity.Property(e => e.Desc).HasDefaultValueSql("''");

                entity.Property(e => e.En).HasDefaultValueSql("''");

                entity.Property(e => e.Ko).HasDefaultValueSql("''");

                entity.Property(e => e.State).HasDefaultValueSql("'1'");

                entity.Property(e => e.Zh).HasDefaultValueSql("''");

                entity.Property(e => e.Fr).HasDefaultValueSql("''");

                entity.Property(e => e.De).HasDefaultValueSql("''");

                entity.Property(e => e.Es).HasDefaultValueSql("''");

                entity.Property(e => e.Ru).HasDefaultValueSql("''");

                entity.Property(e => e.Pt).HasDefaultValueSql("''");

                entity.Property(e => e.Ar).HasDefaultValueSql("''");

                entity.Property(e => e.Tw).HasDefaultValueSql("''");

                entity.Property(e => e.Ja).HasDefaultValueSql("''");
            });

            modelBuilder.Entity<LoginLog>(entity =>
            {
                entity.Property(e => e.CountryCode).HasDefaultValueSql("''");

                entity.Property(e => e.LoginClient).HasDefaultValueSql("''");

                entity.Property(e => e.LoginIp).HasDefaultValueSql("''");

                entity.Property(e => e.LoginMethod).HasDefaultValueSql("'0'");

                entity.Property(e => e.Mobile).HasDefaultValueSql("''");

                entity.Property(e => e.Uuid).HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.Property(e => e.State).HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Node>(entity =>
            {
                entity.Property(e => e.Address).HasDefaultValueSql("''");

                entity.Property(e => e.DeleteIp).HasDefaultValueSql("''");

                entity.Property(e => e.Deleter).HasDefaultValueSql("''");

                entity.Property(e => e.ErrorCount).HasDefaultValueSql("'0'");

                entity.Property(e => e.Name).HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Tokens>(entity =>
            {
                entity.Property(e => e.Desc).HasDefaultValueSql("''");

                entity.Property(e => e.DollarPrice).HasDefaultValueSql("'0.0000'");

                entity.Property(e => e.FaceBookUrl).HasDefaultValueSql("''");

                entity.Property(e => e.IssueCost).HasDefaultValueSql("''");

                entity.Property(e => e.IssueState).HasDefaultValueSql("''");

                entity.Property(e => e.Order).HasDefaultValueSql("'0'");

                entity.Property(e => e.Precision).HasDefaultValueSql("'0'");

                entity.Property(e => e.RmbPrice).HasDefaultValueSql("'0.0000'");

                entity.Property(e => e.State).HasDefaultValueSql("'0'");

                entity.Property(e => e.TransactionPrecision).HasDefaultValueSql("'0'");

                entity.Property(e => e.TwitterUrl).HasDefaultValueSql("''");

                entity.Property(e => e.WebSite).HasDefaultValueSql("''");

                entity.Property(e => e.WhitePaperUrl).HasDefaultValueSql("''");
            });

            modelBuilder.Entity<TransferRecord>(entity =>
            {
                entity.Property(e => e.Amount).HasDefaultValueSql("'0.0000000000'");

                entity.Property(e => e.BlockNum).HasDefaultValueSql("'0'");

                entity.Property(e => e.Contract).HasDefaultValueSql("''");

                entity.Property(e => e.ErrorTimes).HasDefaultValueSql("'0'");

                entity.Property(e => e.FromAccount).HasDefaultValueSql("''");

                entity.Property(e => e.Memo).HasDefaultValueSql("''");

                entity.Property(e => e.Remark).HasDefaultValueSql("''");

                entity.Property(e => e.ToAccount).HasDefaultValueSql("''");

                entity.Property(e => e.TransactionSign).HasDefaultValueSql("''");

                entity.Property(e => e.TransferFee).HasDefaultValueSql("'0.0000'");

                entity.Property(e => e.TransferState).HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasDefaultValueSql("''");

                entity.Property(e => e.InviteCode).HasDefaultValueSql("''");

                entity.Property(e => e.LoginIp).HasDefaultValueSql("''");

                entity.Property(e => e.Nickname).HasDefaultValueSql("'匿名用户'");

                entity.Property(e => e.ParentUserCode).HasDefaultValueSql("''");

                entity.Property(e => e.ParentUserMobile).HasDefaultValueSql("''");

                entity.Property(e => e.State).HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Version>(entity =>
            {
                entity.Property(e => e.Connect).HasDefaultValueSql("''");

                entity.Property(e => e.DeleteIp).HasDefaultValueSql("''");

                entity.Property(e => e.Deleter).HasDefaultValueSql("''");

                entity.Property(e => e.Desc).HasDefaultValueSql("''");

                entity.Property(e => e.State).HasDefaultValueSql("'1'");
            });
        }
    }
}
