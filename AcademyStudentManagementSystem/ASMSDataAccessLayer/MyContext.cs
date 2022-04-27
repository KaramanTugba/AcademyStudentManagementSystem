using ASMSEntityLayer.IdentityModels;
using ASMSEntityLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSDataAccessLayer
{
    public class MyContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Neighbourhood> Neighbourhoods { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseGroup> CourseGroups { get; set; }
        public virtual DbSet<StudentsCourseGroup> StudentsCourseGroups { get; set; }
        public virtual DbSet<StudentAttendance> StudentAttendances { get; set; }
        public virtual DbSet<UsersAddress> UsersAddresses { get; set; }

        //override
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseGroup>().HasIndex(cg => new { cg.PortalCode }).IsUnique(true);
            base.OnModelCreating(builder);

            ////ilişki burada kurulur
            //builder.Entity<District>().HasOne(d => d.City).WithMany(c => c.Districts).HasForeignKey(d => d.CityId).OnDelete(DeleteBehavior.NoAction);//ilçe -- bire /çok ilişki /ne üzerinden / hangi davranışla (ilçe silinemez)
            //builder.Entity<Neighbourhood>().HasOne(n => n.District).WithMany(n => n.Neighbourhoods).HasForeignKey(n => n.DistrictId).OnDelete(DeleteBehavior.NoAction);
            
        
        }

    }
}
