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
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private CollegeManagementContext _context;
        public EnrollmentRepository(CollegeManagementContext context)
        {
            _context = context;
        }
        public void Add(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
        }
        public void Edit(Enrollment enrollment)
        {
            _context.Entry(enrollment).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(int enrollmentID)
        {
            Enrollment enrollment = _context.Enrollments.Find(enrollmentID);
            _context.Enrollments.Remove(enrollment);
        }
        public PagedList<EnrollmentListItem> GetEnrollmentPagedList(int currentPage, int pageSize)
        {
            var query = from e in _context.Enrollments.AsNoTracking()
                        select new EnrollmentListItem
                        {
                            EnrollmentID = e.EnrollmentID,
                            Grade = e.Grade,
                            RegistrationDate = e.RegistrationDate,
                            RegistrationNumber = e.RegistrationNumber,
                            SubjectName = e.Subject.Name,
                            StudentName = e.Student.Name
                        };

            var pagedList = new PagedList<EnrollmentListItem>(currentPage, pageSize, query.Count());
            query = query.OrderByDescending(e => e.EnrollmentID);

            var skip = (pagedList.CurrentPage - 1) * pagedList.PageSize;

            var enrollmentListItem = query.Skip(() => skip).Take(() => pagedList.PageSize).ToList().Select(e =>
                        new EnrollmentListItem
                        {
                            EnrollmentID = e.EnrollmentID,
                            Grade = e.Grade,
                            RegistrationDate = e.RegistrationDate,
                            RegistrationNumber = e.RegistrationNumber,
                            SubjectName = e.SubjectName,
                            StudentName = e.StudentName,
                        });

            pagedList.AddRange(enrollmentListItem);
            return pagedList;
        }
        public Enrollment GetEnrollmentDetails(int enrollmentID)
        {
            var query = from e in _context.Enrollments.AsNoTracking()
                        where e.EnrollmentID == enrollmentID
                        select new
                        {
                            EnrollmentID = e.EnrollmentID,
                            Grade = e.Grade,
                            RegistrationDate = e.RegistrationDate,
                            RegistrationNumber = e.RegistrationNumber,
                            StudentID = e.StudentID,
                            SubjectID = e.SubjectID
                        };
            var enrollment = query.SingleOrDefault();
            return enrollment == null ? null : new Enrollment
            {
                EnrollmentID = enrollment.EnrollmentID,
                Grade = enrollment.Grade,
                RegistrationDate = enrollment.RegistrationDate,
                RegistrationNumber = enrollment.RegistrationNumber,
                StudentID = enrollment.StudentID,
                SubjectID = enrollment.SubjectID
            };
        }
    }
}
