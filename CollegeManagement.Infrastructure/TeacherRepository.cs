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
    public class TeacherRepository : ITeacherRepository
    {
        private CollegeManagementContext _context;
        public TeacherRepository(CollegeManagementContext context)
        {
            _context = context;
        }
        public void Add(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
        }
        public void Edit(Teacher teacher)
        {
            _context.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(int teacherID)
        {
            Teacher teacher = _context.Teachers.Find(teacherID);
            _context.Teachers.Remove(teacher);
        }
        public PagedList<TeacherListItem> GetTeacherPagedList(int currentPage, int pageSize)
        {
            var query = from s in _context.Subjects.AsNoTracking()
                        join t in _context.Teachers
                            on s.SubjectID equals t.SubjectID
                        select new TeacherListItem
                        {
                            SubjectID = s.SubjectID,
                            SubjectName = s.Name,
                            TeacherName = t.Name,
                            BirthDate = t.BirthDate,
                            Salary = t.Salary
                        };

            var pagedList = new PagedList<TeacherListItem>(currentPage, pageSize, query.Count());
            query = query.OrderByDescending(s => s.SubjectID);

            var skip = (pagedList.CurrentPage - 1) * pagedList.PageSize;

            var teacherListItem = query.Skip(() => skip).Take(() => pagedList.PageSize).ToList().Select(s =>
                        new TeacherListItem
                        {
                            SubjectID = s.SubjectID,
                            SubjectName = s.SubjectName,
                            TeacherName = s.TeacherName,
                            BirthDate = s.BirthDate,
                            Salary = s.Salary
                        });

            pagedList.AddRange(teacherListItem);
            return pagedList;
        }
        public Teacher GetTeacherDetails(int subjectID)
        {
            var query = from t in _context.Teachers.AsNoTracking()
                        where t.SubjectID == subjectID
                        select new
                        {
                            SubjectID = t.SubjectID,
                            Name = t.Name,
                            BirthDate = t.BirthDate,
                            Salary = t.Salary
                        };
            var teacher = query.SingleOrDefault();
            return teacher == null ? null : new Teacher
            {
                SubjectID = teacher.SubjectID,
                Name = teacher.Name,
                BirthDate = teacher.BirthDate,
                Salary = teacher.Salary
            };
        }
    }
}
