﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using MeetMe.Data.Contracts;
using MeetMe.Data.Models;
using MeetMe.Services.Contracts;

namespace MeetMe.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class GetUsernames_Should
    {
        [Test]
        public void ReturnAllFullnames()
        {
            // Arrange
            var mockedUserRepository = new Mock<IEFRepository<CustomUser>>();
            var users = new List<CustomUser>()
            {
                new CustomUser() { FullName = "Some name" },
                new CustomUser() { FullName = "Other name" },
                new CustomUser() { FullName = "Some othername" }
            }.AsQueryable();
            mockedUserRepository.Setup(x => x.All).Returns(users);
            var mockedFriendService = new Mock<IFriendService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(
                mockedUserRepository.Object,
                mockedFriendService.Object,
                mockedUnitOfWork.Object);
            var expectedUsers = new List<string>() { "Some name", "Other name", "Some othername" };

            // Act
            var result = userService.GetUsernames();

            // Assert
            CollectionAssert.AreEqual(result, expectedUsers);
        }

        [Test]
        public void ReturnEmptyCollection_WhenNoUsers()
        {
            // Arrange
            var mockedUserRepository = new Mock<IEFRepository<CustomUser>>();
            var users = new List<CustomUser>().AsQueryable();
            mockedUserRepository.Setup(x => x.All).Returns(users);
            var mockedFriendService = new Mock<IFriendService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(
                mockedUserRepository.Object,
                mockedFriendService.Object,
                mockedUnitOfWork.Object);
            var expectedUsers = new List<string>();

            // Act
            var result = userService.GetUsernames();

            // Assert
            CollectionAssert.AreEqual(result, expectedUsers);
        }
    }
}