using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BugTracker.Models;
namespace BugTracker.Data{
    public class BugTrackerContext:IdentityDbContext<ApplicationUser>{
        public BugTrackerContext (DbContextOptions<BugTrackerContext> options):base(options){}

        public DbSet<Project> Projects{get;set;}
        public DbSet<Task> Tasks{get;set;}
        public DbSet<ProjectUser> projectUsers{get;set;}

        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<ProjectUser>().HasKey(t=> new {t.ProjectId,t.UserId});

            builder.Entity<ProjectUser>()
                .HasOne(pu=> pu.Project)
                .WithMany(p=> p.ProjectUsers)
                .HasForeignKey(pu=> pu.ProjectId);
            builder.Entity<ProjectUser>()
                .HasOne(pu=> pu.User)
                .WithMany(u=> u.ProjectUsers)
                .HasForeignKey(pu=> pu.UserId);
            builder.Entity<Task>()
                .HasOne(T=> T.User)
                .WithMany(U => U.Tasks)
                .HasForeignKey(T=> T.UserId);
            builder.Entity<Task>()
                .HasOne(T=> T.Project)
                .WithMany(P => P.Tasks)
                .HasForeignKey(T=> T.ProjectId);
            base.OnModelCreating(builder);
        }
    }
}