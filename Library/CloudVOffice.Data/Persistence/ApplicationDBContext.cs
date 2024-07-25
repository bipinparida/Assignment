using CloudVOffice.Core.Domain.Company;
using CloudVOffice.Core.Domain.Comunication;
using CloudVOffice.Core.Domain.EmailTemplates;
using CloudVOffice.Core.Domain.Logging;
using CloudVOffice.Core.Domain.Pemission;
using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Data.Seeding;
using Microsoft.EntityFrameworkCore;


namespace CloudVOffice.Data.Persistence
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
         : base(options)
        {

        }

        #region Base
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ActivityLogType> ActivityLogTypes { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<RoleAndApplicationWisePermission> RoleAndApplicationWisePermissions { get; set; }
        public virtual DbSet<UserWiseViewMapper> UserWiseViewMappers { get; set; }
        public virtual DbSet<InstalledApplication> InstalledApplications { get; set; }
        public virtual DbSet<EmailDomain> EmailDomains { get; set; }
        public virtual DbSet<EmailAccount> EmailAccounts { get; set; }
        public virtual DbSet<LetterHead> LetterHeads { get; set; }
        public virtual DbSet<CompanyDetails> CompanyDetails { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
		#endregion

		

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Base

            modelBuilder.Entity<RefreshToken>()
       .Property(s => s.CreatedDate)
       .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<RefreshToken>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();



            modelBuilder.Entity<Role>()
        .Property(s => s.CreatedDate)
        .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Role>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();


            modelBuilder.Entity<User>()
        .Property(s => s.CreatedDate)
        .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<User>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();



            modelBuilder.Entity<Application>()
        .Property(s => s.CreatedDate)
        .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Application>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();


            modelBuilder.Entity<RoleAndApplicationWisePermission>()
      .Property(s => s.CreatedDate)
      .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<RoleAndApplicationWisePermission>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();




            modelBuilder.Entity<UserWiseViewMapper>()
      .Property(s => s.CreatedDate)
      .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<UserWiseViewMapper>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();



            modelBuilder.Entity<InstalledApplication>()
      .Property(s => s.CreatedDate)
      .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<InstalledApplication>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();



            modelBuilder.Entity<EmailDomain>()
      .Property(s => s.CreatedDate)
      .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<EmailDomain>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();



            modelBuilder.Entity<EmailAccount>()
.Property(s => s.CreatedDate)
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<EmailAccount>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();





            modelBuilder.Entity<LetterHead>()
.Property(s => s.CreatedDate)
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<LetterHead>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();



            modelBuilder.Entity<CompanyDetails>()
.Property(s => s.CreatedDate)
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<CompanyDetails>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();



            modelBuilder.Entity<EmailTemplate>()
.Property(s => s.CreatedDate)
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<EmailTemplate>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();


            modelBuilder.Entity<UserWiseViewMapper>()
.Property(s => s.CreatedDate)
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<UserWiseViewMapper>()
             .Property(s => s.Deleted)
             .HasDefaultValue(false)
             .ValueGeneratedNever();



			#endregion

			

            modelBuilder.Seed();
        }
    }
}
