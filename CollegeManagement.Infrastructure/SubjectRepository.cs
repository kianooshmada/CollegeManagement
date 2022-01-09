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
    public class SubjectRepository : ISubjectRepository
    {
        private CollegeManagementContext _context;
        public SubjectRepository(CollegeManagementContext context)
        {
            _context = context;
        }
        public void Add(Subject subject)
        {
            _context.Subjects.Add(subject);
        }
        public void Edit(Subject subject)
        {
            _context.Entry(subject).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(int subjectID)
        {
            Subject subject = _context.Subjects.Find(subjectID);
            _context.Subjects.Remove(subject);
        }
        public PagedList<SubjectListItem> GetSubjectPagedList(int currentPage, int pageSize)
        {
            var query = from c in _context.Subjects.AsNoTracking()
                        select new SubjectListItem
                        {
                            SubjectID = c.SubjectID,
                            SubjectName = c.Name,
                            CourseID = c.CourseID,
                            CourseName = c.Course.Name
                        };

            var pagedList = new PagedList<SubjectListItem>(currentPage, pageSize, query.Count());
            query = query.OrderByDescending(c => c.CourseID);

            var skip = (pagedList.CurrentPage - 1) * pagedList.PageSize;

            var subjectListItem = query.Skip(() => skip).Take(() => pagedList.PageSize).ToList().Select(c =>
                        new SubjectListItem
                        {
                            SubjectID = c.SubjectID,
                            SubjectName = c.SubjectName,
                            CourseID = c.CourseID,
                            CourseName = c.CourseName
                        });

            pagedList.AddRange(subjectListItem);
            return pagedList;
        }
        public List<SubjectListItem> GetSubjectSelectListItem()
        {
            var query = from s in _context.Subjects.AsNoTracking()
                        select new SubjectListItem
                        {
                            SubjectID = s.SubjectID,
                            SubjectName = s.Name
                        };
            return query.ToList();
        }
        public Subject GetSubjectDetails(int subjectID)
        {
            var query = from c in _context.Subjects.AsNoTracking()
                        where c.SubjectID == subjectID
                        select new
                        {
                            SubjectID = c.SubjectID,
                            CourseID = c.CourseID,
                            Name = c.Name
                        };
            var subject = query.SingleOrDefault();
            return subject == null ? null : new Subject
            {
                SubjectID = subject.SubjectID,
                CourseID = subject.CourseID,
                Name = subject.Name
            };
        }
        public bool HasRelationship(int subjectID)
        {
            var query = from s in _context.Enrollments.AsNoTracking()
                        where s.SubjectID == subjectID
                        select 1;
            return query.Any();
        }
        public PagedList<SubjectListReport> GetSubjectReport(int currentPage, int pageSize)
        {
            var query = from s in _context.Subjects.DefaultIfEmpty()
                        join e in _context.Enrollments
                            on s.SubjectID equals e.SubjectID
                        join st in _context.Students
                            on e.StudentID equals st.StudentID

                        select new SubjectListReport
                        {
                            SubjectName = s.Name,
                            TeacherName = s.Teacher.Name,
                            BirthDate = s.Teacher.BirthDate,
                            Salary = s.Teacher.Salary,
                            StudentsCount = s.Enrollments.Count,
                            GradeAvg = s.Enrollments.Average(ee=>ee.Grade)
                        };

            var pagedList = new PagedList<SubjectListReport>(currentPage, pageSize, query.Count());
            query = query.OrderByDescending(c => c.SubjectName);

            var skip = (pagedList.CurrentPage - 1) * pagedList.PageSize;

            var subjectListItem = query.Skip(() => skip).Take(() => pagedList.PageSize).ToList().Select(c =>
                        new SubjectListReport
                        {
                            SubjectName = c.SubjectName,
                            TeacherName = c.TeacherName,
                            BirthDate = c.BirthDate,
                            Salary = c.Salary,
                            StudentsCount = c.StudentsCount,
                            GradeAvg = c.GradeAvg
                        });

            pagedList.AddRange(subjectListItem);
            return pagedList;
        }
    }
}
