﻿using System.Collections.Generic;
using System.Linq;
using Bursify.Data.EF.Entities.User;

namespace Bursify.Web.Models
{
    public class SubjectViewModel
    {

        public int ID { get; set; }
        public int StudentReportID { get; set; }
        public string Name { get; set; }
        public int MarkAcquired { get; set; }
        public static Subject MapFromStudentSubject(StudentSubjectViewModel studentSubject)
        {
            return new Subject()
            {
                ID = studentSubject.SubjectId,
                Name = studentSubject.SubjectName,
                Period = studentSubject.Period
            };
        }


        public SubjectViewModel MapSingleSubject(Subject subject)
        {
            ID = subject.ID;
            StudentReportID = subject.RequirementId;
            Name = subject.Name;
            MarkAcquired = subject.MarkAcquired;

            return this;
        }

        public Subject ReverseMap()
        {
            return new Subject()
            {
                ID = this.ID,
                RequirementId = this.StudentReportID,
                Name = this.Name,
                MarkAcquired = this.MarkAcquired
            };
        }

        public static List<SubjectViewModel> MapMultipleSubjects(List<Subject> reportViewModels)
        {
            var subjectViewModel = new SubjectViewModel();
            return reportViewModels.Select(subjectViewModel.MapSingleSubject).ToList();
        }
    }
}