using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Core.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Edit(Student student);
        void Delete(int studentID);
        PagedList<StudentListItem> GetStudentPagedList(int currentPage, int pageSize);
        List<StudentListItem> GetStudentSelectListItem();
        Student GetStudentDetails(int studentID);
        bool HasRelationship(int studentID);
        PagedList<StudentListReport> GetStudentReport(int currentPage, int pageSize);
    }
}
