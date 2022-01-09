using CollegeManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private CollegeManagementContext context;
        public UnitOfWork()
        {
            context = new CollegeManagementContext();
        }

        private ICourseRepository _courseRepository;
        private ISubjectRepository _subjectRepository;
        private ITeacherRepository _teacherRepository;
        private IStudentRepository _studentRepository;
        private IEnrollmentRepository _enrollmentRepository;


        public ICourseRepository CourseRepository
        {
            get
            {
                if (this._courseRepository == null)
                {
                    this._courseRepository = new CourseRepository(context);
                }
                return this._courseRepository;
            }
        }

        public ISubjectRepository SubjectRepository
        {
            get
            {
                if (this._subjectRepository == null)
                {
                    this._subjectRepository = new SubjectRepository(context);
                }
                return this._subjectRepository;
            }
        }
        public ITeacherRepository TeacherRepository
        {
            get
            {
                if (this._teacherRepository == null)
                {
                    this._teacherRepository = new TeacherRepository(context);
                }
                return this._teacherRepository;
            }
        }
        public IStudentRepository StudentRepository
        {
            get
            {
                if (this._studentRepository == null)
                {
                    this._studentRepository = new StudentRepository(context);
                }
                return this._studentRepository;
            }
        }
        public IEnrollmentRepository EnrollmentRepository
        {
            get
            {
                if (this._enrollmentRepository == null)
                {
                    this._enrollmentRepository = new EnrollmentRepository(context);
                }
                return this._enrollmentRepository;
            }
        }
        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
