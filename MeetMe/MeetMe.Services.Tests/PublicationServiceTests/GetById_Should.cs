﻿using NUnit.Framework;
using Moq;
using MeetMe.Data.Contracts;
using MeetMe.Data.Models;
using MeetMe.Services.Contracts;

namespace MeetMe.Services.Tests.PublicationServiceTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void CallPublicationRepository_GetByIdOnce()
        {
            // Arrange
            var mockedPublicationRepository = new Mock<IEFRepository<Publication>>();
            var mockedCommentRepository = new Mock<IEFRepository<Comment>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPublicationFactory = new Mock<IPublicationFactory>();
            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedPublicationImageFactory = new Mock<IPublicationImageFactory>();
            var mockedCommentFactory = new Mock<ICommentFactory>();

            var publicationService = new PublicationService(
                mockedPublicationRepository.Object,
                mockedCommentRepository.Object,
                mockedUserService.Object,
                mockedUnitOfWork.Object,
                mockedPublicationFactory.Object,
                mockedDateTimeService.Object,
                mockedPublicationImageFactory.Object,
                mockedCommentFactory.Object);
            int publicationId = 12;

            // Act
            publicationService.GetById(publicationId);

            // Assert
            mockedPublicationRepository.Verify(x => x.GetById(It.Is<int>(i => i == publicationId)), Times.Once);
        }

        [Test]
        public void ReturnCorrectPublication_WhenFound()
        {
            // Arrange
            var mockedPublicationRepository = new Mock<IEFRepository<Publication>>();
            var publication = new Publication();
            mockedPublicationRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(publication);
            var mockedCommentRepository = new Mock<IEFRepository<Comment>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPublicationFactory = new Mock<IPublicationFactory>();
            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedPublicationImageFactory = new Mock<IPublicationImageFactory>();
            var mockedCommentFactory = new Mock<ICommentFactory>();

            var publicationService = new PublicationService(
                mockedPublicationRepository.Object,
                mockedCommentRepository.Object,
                mockedUserService.Object,
                mockedUnitOfWork.Object,
                mockedPublicationFactory.Object,
                mockedDateTimeService.Object,
                mockedPublicationImageFactory.Object,
                mockedCommentFactory.Object);
            int publicationId = 12;

            // Act
            var result = publicationService.GetById(publicationId);

            // Assert
            Assert.AreEqual(result, publication);
        }

        [Test]
        public void ReturnNull_WhenPublicationNotFound()
        {
            // Arrange
            var mockedPublicationRepository = new Mock<IEFRepository<Publication>>();
            var mockedCommentRepository = new Mock<IEFRepository<Comment>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPublicationFactory = new Mock<IPublicationFactory>();
            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedPublicationImageFactory = new Mock<IPublicationImageFactory>();
            var mockedCommentFactory = new Mock<ICommentFactory>();

            var publicationService = new PublicationService(
                mockedPublicationRepository.Object,
                mockedCommentRepository.Object,
                mockedUserService.Object,
                mockedUnitOfWork.Object,
                mockedPublicationFactory.Object,
                mockedDateTimeService.Object,
                mockedPublicationImageFactory.Object,
                mockedCommentFactory.Object);
            int publicationId = 12;

            // Act
            var result = publicationService.GetById(publicationId);

            // Assert
            Assert.IsNull(result);
        }
    }
}