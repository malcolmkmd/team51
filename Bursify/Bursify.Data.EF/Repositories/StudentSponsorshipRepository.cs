﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Bursify.Data.EF.SponsorUser;
using Bursify.Data.EF.StudentUser;
using Bursify.Data.EF.Uow;
using Bursify.Data.EF.User;

namespace Bursify.Data.EF.Repositories
{
    public class StudentSponsorshipRepository : BridgeRepository<StudentSponsorship>
    {
        private readonly DataSession _dataSession;

        public StudentSponsorshipRepository(DataSession dataSession) : base(dataSession)
        {
            _dataSession = dataSession;
        }

        public void ApplyForSponsorship(int userId, int sponsorshipId)
        {
            var newApplication = new StudentSponsorship()
            {
                StudentId = userId,
                SponsorshipId = sponsorshipId,
                ApplicationDate = DateTime.UtcNow,
                SponsorshipConfirmed = "No"
            };

            Save(newApplication);
        }

        public bool ConfirmSponsorship(int userId, int sponsorshipId, string confirmationMessage)
        {
            var application = LoadByIds(userId, sponsorshipId);

            if (application == null) { return false; }

            application.SponsorshipConfirmed = confirmationMessage;

            Save(application);

            return true;
        }

        public List<StudentSponsorship> GetStudentsApplications(int userId)
        {
            return FindMany(application => application.leftId == userId).Where(x => x.SponsorshipConfirmed == "No").ToList();
        }

        public List<StudentSponsorship> GetSponsorApplicants(int sponsorshipId)
        {
            return FindMany(applicant => applicant.rightId == sponsorshipId).Where(x => x.SponsorshipConfirmed == "No").ToList();
        }

        public List<Sponsorship> GetStudentsAppliedSponsorships(int userId)
        {
            var sponsorships =
                FindMany(x => x.leftId == userId)
                    .Where(x => x.SponsorshipConfirmed == "No")
                    .Select(s => s.Sponsorship)
                    .ToList();
            return sponsorships;
        }

        public List<Student> GetApplyingStudents(int sponsorshipId)
        {
            var students =
                FindMany(x => x.SponsorshipId == sponsorshipId)
                    .Where(x => x.SponsorshipConfirmed == "No")
                    .Select(s => s.Student)
                    .ToList();
            return students;
        }

        public List<Student> GetStudentsEndorsed(int sponsorshipId)
        {
            //var students = (from s in DbContext.Set<StudentSponsorship>() where s.SponsorshipId == sponsorshipId select s.Student).ToList()
            //this query thanx to resharper!
            var students =
                FindMany(x => x.SponsorshipId == sponsorshipId)
                    .Where(x => x.SponsorshipConfirmed != "No")
                    .Select(s => s.Student)
                    .ToList();
            return students;
        }



        //done in api using generic repo
        //public Student GetApplicant(int userId)
        //{
        //    var student = new StudentRepository(_dataSession);

        //    return student.GetStudent(userId);
        //}
    }
}