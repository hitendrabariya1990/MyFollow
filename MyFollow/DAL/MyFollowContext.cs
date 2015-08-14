using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyFollow.Migrations;
using MyFollow.Models;
using System.Threading.Tasks;

namespace MyFollow.DAL
{
    public class MyFollowUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MyFollowUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class MyFollowContext : IdentityDbContext<MyFollowUser>
    {
        public MyFollowContext()
            : base("MyFollowContext", throwIfV1Schema: false)
        {
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyFollowContext, SchoolDataLayer.Migrations.Configuration>("MyFol"));
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyFollowContext, Configuration>("MyFollowContext"));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }



        public DbSet<LoginMaster> LoginMasters { get; set; }

        public DbSet<ProductOwner> ProductOwners { get; set; }

        public DbSet<Products> Productses { get; set; }

        public DbSet<UploadImages> UploadImageses { get; set; }
        public static MyFollowContext Create()
        {
            return new MyFollowContext();
        }
    }
    public class ApplicationUser :MyFollowUser
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

    }
    public class IdentityManager
    {
        public bool RoleExists(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new MyFollowContext()));
            return rm.RoleExists(name);
        }


        public bool CreateRole(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new MyFollowContext()));
            var idResult = rm.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }


        public bool CreateUser(MyFollowUser user, string password)
        {
            var um = new UserManager<MyFollowUser>(
                new UserStore<MyFollowUser>(new MyFollowContext()));
            var idResult = um.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var um = new UserManager<MyFollowUser>(
                new UserStore<MyFollowUser>(new MyFollowContext()));
            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

    }
  
}