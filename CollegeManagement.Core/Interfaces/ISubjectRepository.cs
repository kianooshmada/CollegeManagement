using CollegeManagement.Core.Models;
using CollegeManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Core.Interfaces
{
    public interface ISubjectRepository
    {
        void Add(Subject subject);
        void Edit(Subject subject);
        void Delete(int subjectID);
        PagedList<SubjectListItem> GetSubjectPagedList(int currentPage, int pageSize);
        List<SubjectListItem> GetSubjectSelectListItem();
        Subject GetSubjectDetails(int subjectID);
        bool HasRelationship(int subjectID);
        PagedList<SubjectListReport> GetSubjectReport(int currentPage, int pageSize);
    }
}
