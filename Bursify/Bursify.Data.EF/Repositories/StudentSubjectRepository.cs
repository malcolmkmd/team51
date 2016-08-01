﻿using System.Collections.Generic;
using Bursify.Data.EF.StudentUser;
using Bursify.Data.EF.Uow;

namespace Bursify.Data.EF.Repositories
{
    public class StudentSubjectRepository : Repository<StudentSubject>
    {
        public StudentSubjectRepository(DataSession dataSession) : base(dataSession)
        {
        }

        public List<StudentSubject> GetStudentSubjects(int studentId)
        {
            return FindMany(subjects => subjects.StudentId == studentId);
        }
    }
}