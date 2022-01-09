using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Core.Interfaces
{
    public interface IEnrollmentRepository
    {
        void Add(Enrollment enrollment);
        void Edit(Enrollment enrollment);
        void Delete(int enrollmentID);
        PagedList<EnrollmentListItem> GetEnrollmentPagedList(int currentPage, int pageSize);
        Enrollment GetEnrollmentDetails(int enrollmentID);
    }
}
