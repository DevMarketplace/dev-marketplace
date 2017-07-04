using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataAccess;

namespace DataAccess.Migrations
{
    [DbContext(typeof(DevMarketplaceDataContext))]
    [Migration("20170704030250_ProjectCapabilities")]
    partial class ProjectCapabilities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<Guid>("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DataAccess.Entity.Card", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Assignee")
                        .IsRequired();

                    b.Property<string>("CardProperties");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description");

                    b.Property<string>("Estimate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<Guid>("ProjectId");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Assignee");

                    b.HasIndex("ProjectId");

                    b.HasIndex("Status");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("DataAccess.Entity.CardBidder", b =>
                {
                    b.Property<Guid?>("CardId");

                    b.Property<string>("UserId");

                    b.HasKey("CardId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CardBidder");
                });

            modelBuilder.Entity("DataAccess.Entity.CardDeveloper", b =>
                {
                    b.Property<Guid?>("CardId");

                    b.Property<string>("UserId");

                    b.HasKey("CardId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CardDeveloper");
                });

            modelBuilder.Entity("DataAccess.Entity.CardStatus", b =>
                {
                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Status");

                    b.ToTable("CardStatus");
                });

            modelBuilder.Entity("DataAccess.Entity.CardTask", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("CardId");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("TaskProperties");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("CardTask");
                });

            modelBuilder.Entity("DataAccess.Entity.Company", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("IsoCountryCode")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<string>("Location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("IsoCountryCode");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("DataAccess.Entity.CompanyAdmin", b =>
                {
                    b.Property<Guid?>("CompanyId");

                    b.Property<string>("UserId");

                    b.HasKey("CompanyId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CompanyAdmin");
                });

            modelBuilder.Entity("DataAccess.Entity.Country", b =>
                {
                    b.Property<string>("IsoCountryCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("IsoCountryCode");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("DataAccess.Entity.Project", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("MainImageUrl");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Summary")
                        .IsRequired();

                    b.Property<string>("TileImageUrl")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("VanityImageUrl");

                    b.HasKey("Id");

                    b.HasIndex("Status");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("DataAccess.Entity.ProjectOwner", b =>
                {
                    b.Property<Guid?>("ProjectId");

                    b.Property<string>("UserId");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectOwner");
                });

            modelBuilder.Entity("DataAccess.Entity.ProjectStatus", b =>
                {
                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Status");

                    b.ToTable("ProjectStatus");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DataAccess.ApplicationUser", b =>
                {
                    b.HasOne("DataAccess.Entity.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entity.Card", b =>
                {
                    b.HasOne("DataAccess.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("Assignee")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.Entity.Project", "Project")
                        .WithMany("ProjectCards")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.Entity.CardStatus", "CardStatus")
                        .WithMany()
                        .HasForeignKey("Status")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entity.CardBidder", b =>
                {
                    b.HasOne("DataAccess.Entity.Card", "Card")
                        .WithMany("CardBidders")
                        .HasForeignKey("CardId");

                    b.HasOne("DataAccess.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entity.CardDeveloper", b =>
                {
                    b.HasOne("DataAccess.Entity.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entity.CardTask", b =>
                {
                    b.HasOne("DataAccess.Entity.Card", "Card")
                        .WithMany("Tasks")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entity.Company", b =>
                {
                    b.HasOne("DataAccess.Entity.Country", "Country")
                        .WithMany()
                        .HasForeignKey("IsoCountryCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entity.CompanyAdmin", b =>
                {
                    b.HasOne("DataAccess.Entity.Company", "Company")
                        .WithMany("CompanyAdmins")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.ApplicationUser", "User")
                        .WithMany("CompanyAdmins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entity.Project", b =>
                {
                    b.HasOne("DataAccess.Entity.ProjectStatus", "ProjectStatus")
                        .WithMany()
                        .HasForeignKey("Status")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entity.ProjectOwner", b =>
                {
                    b.HasOne("DataAccess.Entity.Project", "Project")
                        .WithMany("ProjectOwners")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DataAccess.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DataAccess.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
