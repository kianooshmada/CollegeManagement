using CollegeManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Infrastructure.Migrations
{
    internal sealed class Configuration :
    DbMigrationsConfiguration<CollegeManagement.Infrastructure.CollegeManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CollegeManagement.Infrastructure.CollegeManagementContext context)
        {
            var course = new List<Course>
            {
                new Course { Name = "First" },
                new Course { Name = "Second" }
            };
            course.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();
        }
    }
}
