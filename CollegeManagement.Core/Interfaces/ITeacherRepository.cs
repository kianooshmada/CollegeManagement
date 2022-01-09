using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Core.Interfaces
{
    public interface ITeacherRepository
    {
        void Add(Teacher teacher);
        void Edit(Teacher teacher);
        void Delete(int teacherID);
        PagedList<TeacherListItem> GetTeacherPagedList(int currentPage, int pageSize);
        Teacher GetTeacherDetails(int teacherID);
    }
}
