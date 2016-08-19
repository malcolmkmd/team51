﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bursify.Api.Students;
using Bursify.Data.EF.Entities.StudentUser;
using Bursify.Data.EF.Entities.User;
using Bursify.Data.EF.Entities.Bridge;
using Bursify.Web.Models;

namespace Bursify.Web.Controllers
{
    [System.Web.Mvc.RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        private readonly StudentApi _studentApi;

        public StudentController(StudentApi studentApi)
        {
            _studentApi = studentApi;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetAllStudents")]
        public HttpResponseMessage GetAllStudents(HttpRequestMessage request)
        {
            var students = _studentApi.GetAllStudents();

            var studentsVm = StudentViewModel.MapMultipleStudents(students);

            var response = request.CreateResponse(HttpStatusCode.OK, studentsVm);

            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetStudent")]
        public HttpResponseMessage GetStudent(HttpRequestMessage request, int studentId)
        {
            var student = _studentApi.GetStudent(studentId);

            var model = new StudentViewModel(student);

            var response = request.CreateResponse(HttpStatusCode.OK, model);

            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetAddresses")]
        public HttpResponseMessage GetAddresses(HttpRequestMessage request, int userId)
        {
            var addresses = _studentApi.GetAddressofUser(userId);

            var response = request.CreateResponse(HttpStatusCode.OK, addresses);

            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("SaveStudent")]
        public HttpResponseMessage SaveStudent(HttpRequestMessage request, StudentViewModel student)
        {
            var newStudent = student.ReverseMap();

            _studentApi.SaveStudent(newStudent);

            var model = new StudentViewModel();

            var studentVm = model.MapSingleStudent(newStudent);

            var response = request.CreateResponse(HttpStatusCode.Created, studentVm);

            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("SaveDetails")]
        public HttpResponseMessage SaveDetails(HttpRequestMessage request, PesonalDetailsViewModel details)
        {
            var user = _studentApi.GetUserInfo(details.ID);
            user.Biography = details.Bio;

            _studentApi.UpdateUser(user);

            var response = request.CreateResponse(HttpStatusCode.Accepted);

            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("SaveContactDetails")]
        public HttpResponseMessage SaveContactDetails(HttpRequestMessage request, ContactDetailsViewModel details, UserAddressViewModel address)
        {
            var user = _studentApi.GetUserInfo(details.ID);
            user.CellphoneNumber = details.GPhone;
            if (_studentApi.ValidateEmail(details.email))
            {
                user.Email = details.email;
            }
            _studentApi.UpdateUser(user);
            var student = _studentApi.GetStudent(details.ID);

            student.GuardianEmail = details.GEmail;
            student.GuardianRelationship = details.GRelationship;
            student.GuardianPhone = details.GPhone;

            _studentApi.SaveStudent(student);

            _studentApi.SaveAddress(address.ReverseMap());

            var response = request.CreateResponse(HttpStatusCode.OK);

            return response;
        }

            var subjects = new List<Subject>();
                subjects.Add(SubjectViewModel.MapFromStudentSubject(r));
                
            _studentApi.AddSubjects(subjects);
        
        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("ApplyForSponsorship")]
        public HttpResponseMessage ApplyForSponsorship(HttpRequestMessage request,
            StudentSponsorshipViewModel application)
        {
            var newApplication = new StudentSponsorship()
            {
                StudentId = application.StudentId,
                SponsorshipId = application.SponsorshipId,
                ApplicationDate = application.ApplicationDate,
                SponsorshipOffered = application.SponsorshipOffered,
                Status = application.Status
            };

            _studentApi.ApplyForSponsorship(newApplication);

            var model = new StudentSponsorshipViewModel();

            var applicationVm = model.MapSIngleStudentSponsorship(newApplication);

            var response = request.CreateResponse(HttpStatusCode.Created, applicationVm);

            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("ConfirmSponsorship")]
        public HttpResponseMessage ConfirmSponsorship(HttpRequestMessage request, int studentId, int sponsorshipId,
            string confirmationMessage)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                var confirmed = _studentApi.ConfirmSponsorship(studentId, sponsorshipId, confirmationMessage);

                if (confirmed)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, new {success = true});
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                }
            }

            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetSponsorshipSuggestions")]
        public HttpResponseMessage GetSponsorshipSuggestions(HttpRequestMessage request, int studentId)
        {
            var suggestions = _studentApi.LoadSponsorshipSuggestions(studentId);

            var sponsorshipsVm = SponsorshipViewModel.MultipleSponsorshipsMap(suggestions);

            var response = request.CreateResponse(HttpStatusCode.OK, sponsorshipsVm);

            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetInstitution")]
        public HttpResponseMessage GetInstitution(HttpRequestMessage request, int userId)
        {
            var institution = _studentApi.GetInstitution(userId);

            var model = new InstitutionViewModel();

            var institutionVm = model.MapSingleInstitution(institution);

            var response = request.CreateResponse(HttpStatusCode.OK, institutionVm);

            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("SaveInstitution")]
        public HttpResponseMessage SaveInstitution(HttpRequestMessage request, InstitutionViewModel institution)
        {
            var newInstitution = new Institution()
            {
                ID = institution.ID,
                Name = institution.Name,
                Type = institution.Type,
                Website = institution.Website
            };

            _studentApi.SaveInstitution(newInstitution);

            var model = new InstitutionViewModel();

            var institutionVm = model.MapSingleInstitution(newInstitution);

            var response = request.CreateResponse(HttpStatusCode.Created, institutionVm);

            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetNumberSupportedCampaign")]
        public HttpResponseMessage GetNumberSupportedCampaign(HttpRequestMessage request, int campaignId)
        {
            int number = _studentApi.GetNumberOfCampaignSupporters(campaignId);

            var response = request.CreateResponse(HttpStatusCode.OK, number);

            return response;
        }
    }
}
