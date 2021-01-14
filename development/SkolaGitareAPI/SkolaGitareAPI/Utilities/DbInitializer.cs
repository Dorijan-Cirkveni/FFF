using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkolaGitareAPI.Data;
using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    using (var userManager = serviceScope.ServiceProvider.GetService<UserManager<Person>>())
                    {
                        var passwordHasher = new PasswordHasher<Person>();

                        if (!context.Roles.Any())
                        {
                            var role1 = new IdentityRole
                            {
                                Name = "Admin",
                                NormalizedName = "ADMIN"
                            };

                            var role2 = new IdentityRole
                            {
                                Name = "Student",
                                NormalizedName = "STUDENT"
                            };

                            var role3 = new IdentityRole
                            {
                                Name = "Teacher",
                                NormalizedName = "TEACHER"
                            };

                            context.Roles.Add(role1);
                            context.Roles.Add(role2);
                            context.Roles.Add(role3);

                            context.SaveChanges();
                        }

                        //add admin user
                        if (!context.Users.Any())
                        {
                            var adminUser = new Person
                            {
                                EmailConfirmed = true,
                                Email = "admin@admin.com",
                                Name = "Admin",
                                Surname = "Admin",
                                UserName = "admin@admin.com",
                                Verified = true,
                                SecurityStamp = Guid.NewGuid().ToString(),
                            };

                            var teacherUser = new Person
                            {
                                EmailConfirmed = true,
                                Email = "teacher@teacher.com",
                                Name = "Teacher",
                                Surname = "Teacher",
                                UserName = "teacher@teacher.com",
                                Verified = true,
                                SecurityStamp = Guid.NewGuid().ToString(),
                            };

                            var studentUser = new Person
                            {
                                EmailConfirmed = true,
                                Email = "student@student.com",
                                Name = "Student",
                                Surname = "Student",
                                UserName = "student@student.com",
                                Verified = true,
                                SecurityStamp = Guid.NewGuid().ToString(),
                            };

                            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "$Admin1$");

                            teacherUser.PasswordHash = passwordHasher.HashPassword(adminUser, "$Teacher1$");

                            studentUser.PasswordHash = passwordHasher.HashPassword(adminUser, "$Student1$");

                            context.Users.Add(adminUser);

                            context.Users.Add(teacherUser);

                            context.Users.Add(studentUser);

                            context.SaveChanges();

                            userManager.AddToRoleAsync(adminUser, "Admin").Wait();

                            userManager.AddToRoleAsync(teacherUser, "Teacher").Wait();

                            userManager.AddToRoleAsync(studentUser, "Student").Wait();

                            context.SaveChanges();
                        }

                        if (!context.MembershipTypes.Any())
                        {
                            var memberyshipType1 = new MembershipType { Id = new Guid(), Name = "Group", Price = 400 };

                            var memberyshipType2 = new MembershipType { Id = new Guid(), Name = "Individual", Price = 600 };

                            context.MembershipTypes.Add(memberyshipType1);

                            context.MembershipTypes.Add(memberyshipType2);

                            context.SaveChanges();
                        }
                        
                    }
                }
            }
        }
    }
}
