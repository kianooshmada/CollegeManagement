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
    public class StudentRepository : IStudentRepository
    {
        private CollegeManagementContext _context;
        public StudentRepository(CollegeManagementContext context)
        {
            _context = context;
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
        }
        public void Edit(Student student)
        {
            _context.Entry(student).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(int studentID)
        {
            Student student = _context.Students.Find(studentID);
            _context.Students.Remove(student);
        }
        public PagedList<StudentListItem> GetStudentPagedList(int currentPage, int pageSize)
        {
            var query = from c in _context.Students.AsNoTracking()
                        select new StudentListItem
                        {
                            StudentID = c.StudentID,
                            Name = c.Name,
                            BirthDate = c.BirthDate
                        };

            var pagedList = new PagedList<StudentListItem>(currentPage, pageSize, query.Count());
            query = query.OrderByDescending(c => c.StudentID);

            var skip = (pagedList.CurrentPage - 1) * pagedList.PageSize;

            var subjectListItem = query.Skip(() => skip).Take(() => pagedList.PageSize).ToList().Select(c =>
                        new StudentListItem
                        {
                            StudentID = c.StudentID,
                            Name = c.Name,
                            BirthDate = c.BirthDate
                        });

            pagedList.AddRange(subjectListItem);
            return pagedList;
        }
        public List<StudentListItem> GetStudentSelectListItem()
        {
            var query = from s in _context.Students.AsNoTracking()
                        select new StudentListItem
                        {
                            StudentID = s.StudentID,
                            Name = s.Name
                        };
            return query.ToList();
        }
        public Student GetStudentDetails(int studentID)
        {
            var query = from c in _context.Students.AsNoTracking()
                        where c.StudentID == studentID
                        select new
                        {
                            StudentID = c.StudentID,
                            Name = c.Name,
                            BirthDate = c.BirthDate
                        };
            var student = query.SingleOrDefault();
            return student == null ? null : new Student
            {
                StudentID = student.StudentID,
                Name = student.Name,
                BirthDate = student.BirthDate
            };
        }
        public bool HasRelationship(int studentID)
        {
            var query = from s in _context.Enrollments.AsNoTracking()
                        where s.StudentID == studentID
                        select 1;
            return query.Any();
        }
        public PagedList<StudentListReport> GetStudentReport(int currentPage, int pageSize)
        {
            var query = from st in _context.Students
                        join e in _context.Enrollments
                            on st.StudentID equals e.StudentID

                        select new StudentListReport
                        {
                            SubjectName = e.Subject.Name,
                            StudentName = st.Name,
                            RegistrationNumber = e.RegistrationNumber,
                            RegistrationDate = e.RegistrationDate,
                            Grade = e.Grade
                        };

            var pagedList = new PagedList<StudentListReport>(currentPage, pageSize, query.Count());
            query = query.OrderByDescending(c => c.SubjectName);

            var skip = (pagedList.CurrentPage - 1) * pagedList.PageSize;

            var subjectListItem = query.Skip(() => skip).Take(() => pagedList.PageSize).ToList().Select(c =>
                        new StudentListReport
                        {
                            SubjectName = c.SubjectName,
                            StudentName = c.StudentName,
                            RegistrationNumber = c.RegistrationNumber,
                            RegistrationDate = c.RegistrationDate,
                            Grade = c.Grade
                        });

            pagedList.AddRange(subjectListItem);
            return pagedList;
        }
    }
}
