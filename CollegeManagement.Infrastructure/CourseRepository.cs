using CollegeManagement.Core.Interfaces;
using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CollegeManagement.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        private CollegeManagementContext _context;
        public CourseRepository(CollegeManagementContext context)
        {
            _context = context;
        }
        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }
        public void Edit(Course course)
        {
            _context.Entry(course).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(int courseID)
        {
            Course course = _context.Courses.Find(courseID);
            _context.Courses.Remove(course);
        }
        public PagedList<CourseListItem> GetCoursePagedList(int currentPage, int pageSize)
        {
            var query = from c in _context.Courses.AsNoTracking()
                        select new CourseListItem
                        {
                            CourseID = c.CourseID,
                            Name = c.Name
                        };

            var pagedList = new PagedList<CourseListItem>(currentPage, pageSize, query.Count());
            query = query.OrderByDescending(c => c.CourseID);

            var skip = (pagedList.CurrentPage - 1) * pagedList.PageSize;

            var courseListItem = query.Skip(() => skip).Take(() => pagedList.PageSize).ToList().Select(c =>
                        new CourseListItem
                        {
                            CourseID = c.CourseID,
                            Name = c.Name
                        });

            pagedList.AddRange(courseListItem);
            return pagedList;
        }
        public List<CourseListItem> GetCourseSelectListItem()
        {
            var query = from c in _context.Courses.AsNoTracking()
                        select new CourseListItem
                        {
                            CourseID = c.CourseID,
                            Name = c.Name
                        };
            return query.ToList();
        }
        public Course GetCourseDetails(int courseID)
        {
            var query = from c in _context.Courses.AsNoTracking()
                        where c.CourseID == courseID
                        select new
                        {
                            CourseID = c.CourseID,
                            Name = c.Name
                        };
            var course = query.SingleOrDefault();
            return course == null ? null : new Course
            {
                CourseID = course.CourseID,
                Name = course.Name
            };
        }
        public bool HasRelationship(int courseID)
        {
            var query = from s in _context.Subjects.AsNoTracking()
                        where s.CourseID == courseID
                        select 1;
            return query.Any();
        }

        public PagedList<CourseListReport> GetCourseReport(int currentPage, int pageSize)
        {
            var query = from c in _context.Courses.DefaultIfEmpty()
                        join s in _context.Subjects.DefaultIfEmpty()
                            on c.CourseID equals s.CourseID                       
                        join e in _context.Enrollments
                            on s.SubjectID equals e.SubjectID
                        //join st in _context.Students
                          //  on e.StudentID equals st.StudentID

                        select new CourseListReport
                        {
                            Name = c.Name,
                            TeachersCount = c.Subjects.Select(d=>d.Teacher).Count(),
                            StudentsCount = s.Enrollments.Select(st=>st.Student).Count(),
                            GradeAvg = s.Enrollments.Average(ee => ee.Grade)
                        };

            var pagedList = new PagedList<CourseListReport>(currentPage, pageSize, query.Count());
            query = query.OrderByDescending(c => c.Name);

            var skip = (pagedList.CurrentPage - 1) * pagedList.PageSize;

            var courseListItem = query.Skip(() => skip).Take(() => pagedList.PageSize).ToList().Select(c =>
                        new CourseListReport
                        {
                            Name = c.Name,
                            TeachersCount = c.TeachersCount,
                            StudentsCount = c.StudentsCount,
                            GradeAvg = c.GradeAvg
                        }).Distinct();

            pagedList.AddRange(courseListItem);
            return pagedList;
        }
    }
}
