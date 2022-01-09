using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Core.Interfaces
{
    public interface ICourseRepository
    {
        void Add(Course course);
        void Edit(Course course);
        void Delete(int courseID);
        PagedList<CourseListItem> GetCoursePagedList(int currentPage, int pageSize);
        List<CourseListItem> GetCourseSelectListItem();
        Course GetCourseDetails(int courseID);
        bool HasRelationship(int courseID);
        PagedList<CourseListReport> GetCourseReport(int currentPage, int pageSize);
    }
}
