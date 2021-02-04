using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>().HasOne(x => x.Teacher);

            modelBuilder.Entity<Appointment>().HasMany(x => x.Students).WithMany(x => x.Appointments).UsingEntity(j => j.ToTable("StudentAppointment"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Appointment> Appointments { get; set; }

        public virtual DbSet<AppointmentRequest> AppointmentRequests { get; set; }

        public virtual DbSet<Lesson> Lessons { get; set; }

        public virtual DbSet<MembershipType> MembershipTypes { get; set; }

        public virtual DbSet<Membership> Memberships { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

    }
}
