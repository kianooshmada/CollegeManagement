using CollegeManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Infrastructure
{
    public class CollegeManagementContext:DbContext
    {
        public CollegeManagementContext():base("CollegeManagementContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(CollegeManagementContext)));
        }
        public class BaseEntityTypeConfiguration<TEntity> : EntityTypeConfiguration<TEntity>
            where TEntity : class, IEntity
        {
            public BaseEntityTypeConfiguration()
            {

            }
        }

        public class CourseMap : BaseEntityTypeConfiguration<Course>
        {
            public CourseMap()
            {
                //Key
                HasKey(c => c.CourseID);

                //Fields
                Property(c => c.CourseID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                Property(c => c.Name).HasColumnType("varchar").HasMaxLength(16).IsRequired();

                //Table  
                ToTable("Courses");
            }
        }
        public class SubjectMap : BaseEntityTypeConfiguration<Subject>
        {
            public SubjectMap()
            {
                //Key
                HasKey(s => s.SubjectID);

                //Fields
                Property(s => s.SubjectID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                Property(s => s.CourseID).HasColumnType("int").IsRequired();
                Property(s => s.Name).HasColumnType("varchar").HasMaxLength(16).IsRequired();

                //Relationships
                HasRequired(c => c.Course).WithMany(s => s.Subjects).HasForeignKey(c => c.CourseID).WillCascadeOnDelete(true);
                HasOptional(s => s.Teacher).WithRequired().WillCascadeOnDelete(true);

                //Table  
                ToTable("Subjects");
            }
        }

        public class TeacherMap : BaseEntityTypeConfiguration<Teacher>
        {
            public TeacherMap()
            {
                //Key
                HasKey(t => t.SubjectID);

                //Fields
                Property(t => t.SubjectID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                Property(t => t.SubjectID).HasColumnType("int").IsRequired();
                Property(t => t.Name).HasColumnType("varchar").HasMaxLength(16).IsRequired();
                                
                //Table  
                ToTable("Teachers");
            }
        }
        public class StudentMap : BaseEntityTypeConfiguration<Student>
        {
            public StudentMap()
            {
                //Key
                HasKey(s => s.StudentID);

                //Fields
                Property(s => s.StudentID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                Property(s => s.Name).HasColumnType("varchar").HasMaxLength(16).IsRequired();
                Property(s => s.BirthDate).HasColumnType("datetime2").HasPrecision(0).IsRequired();

                //Table  
                ToTable("Students");
            }
        }
        public class EnrollmentMap : BaseEntityTypeConfiguration<Enrollment>
        {
            public EnrollmentMap()
            {
                //Key
                HasKey(e => e.EnrollmentID);

                //Fields
                Property(e => e.EnrollmentID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                Property(e => e.RegistrationNumber).HasColumnType("int").IsRequired();
                Property(e => e.RegistrationDate).HasColumnType("datetime2").HasPrecision(0).IsRequired();
                Property(e => e.Grade).HasColumnType("float").IsOptional();

                //Relationships
                HasRequired(c => c.Subject).WithMany(s => s.Enrollments).HasForeignKey(c => c.SubjectID).WillCascadeOnDelete(true);
                HasRequired(c => c.Student).WithMany(s => s.Enrollments).HasForeignKey(c => c.StudentID).WillCascadeOnDelete(true);

                //Table  
                ToTable("Enrollmens");
            }
        }
    }
}
