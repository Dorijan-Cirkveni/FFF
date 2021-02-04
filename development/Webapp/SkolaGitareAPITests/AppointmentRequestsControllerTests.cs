using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using SkolaGitareAPI.Controllers;
using SkolaGitareAPI.Data.Entities;
using SkolaGitareAPI.Data.Repositories.Interfaces;

namespace SkolaGitareAPITests
{
    public class AppointmentRequestsControllerTests
    {
        private AppointmentRequestsController controller;

        private readonly Mock<IPersonRepository> personRepository;
        private readonly Mock<IAppointmentRepository> appointmentRepository;
        private readonly Mock<IAppointmentRequestRepository> repository;
        private readonly Mock<UserManager<Person>> userManager;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}