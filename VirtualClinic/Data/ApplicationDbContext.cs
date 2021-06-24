using VirtualClinic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VirtualClinic.Models.Identity;
using VirtualClinic.Models.Patient_ns;
using WebApplication1.Data.AdminUserConfig;
using Task = VirtualClinic.Models.Task;

namespace VirtualClinic.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        { }


        public DbSet<ApplicationUser>  AppUsers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<MedicalFile> MedicalFiles { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("VirtualClinic");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
                entity.HasKey(u => u.Id);
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<Task>(entity =>
            {
                entity.ToTable("Tasks").HasKey(i => i.TaskId);
            });
            builder.Entity<MedicalFile>(entity =>
            {
                entity.ToTable("MedicalFiles").HasKey(i => i.Id);
            });

            builder.Entity<Rating>(entity =>
            {
                entity.ToTable("Ratings").HasKey(i => i.Id);
            });

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new AdminConfiguration());
            builder.ApplyConfiguration(new UsersWithRolesConfig());

          
        }
    }
}
