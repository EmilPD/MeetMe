﻿using System.Collections.Generic;
using System.Linq;
using MeetMe.Data.Contracts;
using MeetMe.Data.Models;
using MeetMe.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace MeetMe.Services.Tests.StatisticServiceTests
{
    [TestFixture]
    public class AddNotificationStatistic_Should
    {
        [Test]
        public void CallUserService_GetByIndentityIdOnce()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var user = new CustomUser() { Id = 1 };
            mockedUserService.Setup(x => x.GetByIndentityId(It.IsAny<string>())).Returns(user);
            var mockedStatisticRepository = new Mock<IEFRepository<Statistic>>();
            var statistics = new List<Statistic>() { new Statistic() { CustomUserId = 1 } }.AsQueryable();
            mockedStatisticRepository.Setup(x => x.All).Returns(statistics);
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedStatisticFactory = new Mock<IStatisticFactory>();

            var statisticService = new StatisticService(
                mockedUserService.Object,
                mockedStatisticRepository.Object,
                mockedUnitOfWork.Object,
                mockedStatisticFactory.Object);
            string userId = "some-id";

            // Act
            statisticService.AddNotificationStatistic(userId);

            // Assert
            mockedUserService.Verify(x => x.GetByIndentityId(It.Is<string>(i => i == userId)), Times.Once);
        }

        [Test]
        public void IncreasedStatistic_NotificationCountWithOne_WhenCalledWithStringId()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var user = new CustomUser() { Id = 1 };
            mockedUserService.Setup(x => x.GetByIndentityId(It.IsAny<string>())).Returns(user);
            var mockedStatisticRepository = new Mock<IEFRepository<Statistic>>();
            var statistic = new Statistic() { CustomUserId = 1 };
            var statistics = new List<Statistic>() { statistic }.AsQueryable();
            mockedStatisticRepository.Setup(x => x.All).Returns(statistics);
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedStatisticFactory = new Mock<IStatisticFactory>();

            var statisticService = new StatisticService(
                mockedUserService.Object,
                mockedStatisticRepository.Object,
                mockedUnitOfWork.Object,
                mockedStatisticFactory.Object);
            string userId = "some-id";

            // Act
            statisticService.AddNotificationStatistic(userId);

            // Assert
            Assert.AreEqual(statistic.NotificationsCount, 1);
        }

        [Test]
        public void IncreasedStatistic_NotificationCountWithOne_WhenCalledWithIntId()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var user = new CustomUser() { Id = 1 };
            mockedUserService.Setup(x => x.GetByIndentityId(It.IsAny<string>())).Returns(user);
            var mockedStatisticRepository = new Mock<IEFRepository<Statistic>>();
            var statistic = new Statistic() { CustomUserId = 1 };
            var statistics = new List<Statistic>() { statistic }.AsQueryable();
            mockedStatisticRepository.Setup(x => x.All).Returns(statistics);
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedStatisticFactory = new Mock<IStatisticFactory>();

            var statisticService = new StatisticService(
                mockedUserService.Object,
                mockedStatisticRepository.Object,
                mockedUnitOfWork.Object,
                mockedStatisticFactory.Object);
            int userId = 1;

            // Act
            statisticService.AddNotificationStatistic(userId);

            // Assert
            Assert.AreEqual(statistic.NotificationsCount, 1);
        }

        [Test]
        public void CallStatisticRepository_UpdateOnce_WhenCalledWithStringId()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var user = new CustomUser() { Id = 1 };
            mockedUserService.Setup(x => x.GetByIndentityId(It.IsAny<string>())).Returns(user);
            var mockedStatisticRepository = new Mock<IEFRepository<Statistic>>();
            var statistic = new Statistic() { CustomUserId = 1 };
            var statistics = new List<Statistic>() { statistic }.AsQueryable();
            mockedStatisticRepository.Setup(x => x.All).Returns(statistics);
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedStatisticFactory = new Mock<IStatisticFactory>();

            var statisticService = new StatisticService(
                mockedUserService.Object,
                mockedStatisticRepository.Object,
                mockedUnitOfWork.Object,
                mockedStatisticFactory.Object);
            string userId = "some-id";

            // Act
            statisticService.AddNotificationStatistic(userId);

            // Assert
            mockedStatisticRepository.Verify(x => x.Update(It.Is<Statistic>(s => s == statistic)), Times.Once);
        }

        [Test]
        public void CallStatisticRepository_UpdateOnce_WhenCalledWithIntId()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var user = new CustomUser() { Id = 1 };
            mockedUserService.Setup(x => x.GetByIndentityId(It.IsAny<string>())).Returns(user);
            var mockedStatisticRepository = new Mock<IEFRepository<Statistic>>();
            var statistic = new Statistic() { CustomUserId = 1 };
            var statistics = new List<Statistic>() { statistic }.AsQueryable();
            mockedStatisticRepository.Setup(x => x.All).Returns(statistics);
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedStatisticFactory = new Mock<IStatisticFactory>();

            var statisticService = new StatisticService(
                mockedUserService.Object,
                mockedStatisticRepository.Object,
                mockedUnitOfWork.Object,
                mockedStatisticFactory.Object);
            int userId = 1;

            // Act
            statisticService.AddNotificationStatistic(userId);

            // Assert
            mockedStatisticRepository.Verify(x => x.Update(It.Is<Statistic>(s => s == statistic)), Times.Once);
        }

        [Test]
        public void CallUnitOfWork_CommitOnce_WhenCalledWithStringId()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var user = new CustomUser() { Id = 1 };
            mockedUserService.Setup(x => x.GetByIndentityId(It.IsAny<string>())).Returns(user);
            var mockedStatisticRepository = new Mock<IEFRepository<Statistic>>();
            var statistic = new Statistic() { CustomUserId = 1 };
            var statistics = new List<Statistic>() { statistic }.AsQueryable();
            mockedStatisticRepository.Setup(x => x.All).Returns(statistics);
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedStatisticFactory = new Mock<IStatisticFactory>();

            var statisticService = new StatisticService(
                mockedUserService.Object,
                mockedStatisticRepository.Object,
                mockedUnitOfWork.Object,
                mockedStatisticFactory.Object);
            string userId = "some-id";

            // Act
            statisticService.AddNotificationStatistic(userId);

            // Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void CallUnitOfWork_CommitOnce_WhenCalledWithIntId()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var user = new CustomUser() { Id = 1 };
            mockedUserService.Setup(x => x.GetByIndentityId(It.IsAny<string>())).Returns(user);
            var mockedStatisticRepository = new Mock<IEFRepository<Statistic>>();
            var statistic = new Statistic() { CustomUserId = 1 };
            var statistics = new List<Statistic>() { statistic }.AsQueryable();
            mockedStatisticRepository.Setup(x => x.All).Returns(statistics);
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedStatisticFactory = new Mock<IStatisticFactory>();

            var statisticService = new StatisticService(
                mockedUserService.Object,
                mockedStatisticRepository.Object,
                mockedUnitOfWork.Object,
                mockedStatisticFactory.Object);
            int userId = 1;

            // Act
            statisticService.AddNotificationStatistic(userId);

            // Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}
