#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2016 Tosho Toshev
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// GitHub repository: https://github.com/cracker4o/dev-marketplace
// e-mail: cracker4o@gmail.com
#endregion
using DataAccess.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    /// <summary>
    /// The main DataContext for the developer marketplace
    /// </summary>
    public sealed class DevMarketplaceDataContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public DbSet<Country> Country { get; set; }

        public DbSet<Company> Company { get; set; }

        public DbSet<CompanyAdmin> CompanyAdmin { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<ProjectOwner> ProjectOwner { get; set; }

        public DbSet<ProjectStatus> ProjectStatus { get; set; }

        public DbSet<Card> Card { get; set; }

        public DbSet<CardBidder> CardBidder { get; set; }

        public DbSet<CardDeveloper> CardDeveloper { get; set; }

        public DbSet<CardStatus> CardStatus { get; set; }

        public DbSet<CardTask> CardTask { get; set; }

        public DevMarketplaceDataContext()
        {
            Database.EnsureCreated();
        }

        public DevMarketplaceDataContext(DbContextOptions options) : base(options)
        {

        }

        private void SetupProject(ModelBuilder builder)
        {
            builder.Entity<Project>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Entity<Project>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<Project>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<ProjectOwner>()
                .HasKey(c => new { c.ProjectId, c.UserId });
        }

        private void SetupCard(ModelBuilder builder)
        {
            builder.Entity<Card>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Entity<Card>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<Card>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<CardTask>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Entity<CardTask>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<CardTask>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<CardBidder>()
                .HasKey(c => new { c.CardId, c.UserId });

            builder.Entity<CardBidder>()
                .HasOne(f => f.Card)
                .WithMany(f => f.CardBidders)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            builder.Entity<CardDeveloper>()
                .HasKey(c => new { c.CardId, c.UserId });
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Company>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Entity<CompanyAdmin>()
                .HasKey(c => new { c.CompanyId, c.UserId });

            SetupProject(builder);

            SetupCard(builder);
        }

        void IDataContext.SaveChanges()
        {
            SaveChanges();
        }
    }
}