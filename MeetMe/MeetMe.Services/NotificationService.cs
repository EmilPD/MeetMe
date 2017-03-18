﻿using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;
using MeetMe.Data.Contracts;
using MeetMe.Data.Models;
using MeetMe.Services.Contracts;
using MeetMe.Web.Models.Notifications;

namespace MeetMe.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEFRepository<Notification> notificationRepository;
        private readonly IDateTimeService dateTimeService;
        private readonly INotificationFactory notificationFactory;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapperService mapperService;
        private readonly IImageService imageService;
        private readonly IUserService userService;

        public NotificationService(
            IEFRepository<Notification> notificationRepository,
            IDateTimeService dateTimeService,
            INotificationFactory notificationFactory,
            IUnitOfWork unitOfWork,
            IMapperService mapperService,
            IImageService imageService,
            IUserService userService)
        {
            Guard.WhenArgument(notificationRepository, "NotificationRepository").IsNull().Throw();
            Guard.WhenArgument(dateTimeService, "DateTimeService").IsNull().Throw();
            Guard.WhenArgument(notificationFactory, "NotificationFactory").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "UnitOfWork").IsNull().Throw();
            Guard.WhenArgument(mapperService, "MapperService").IsNull().Throw();
            Guard.WhenArgument(imageService, "ImageService").IsNull().Throw();
            Guard.WhenArgument(userService, "UserService").IsNull().Throw();

            this.notificationRepository = notificationRepository;
            this.dateTimeService = dateTimeService;
            this.notificationFactory = notificationFactory;
            this.unitOfWork = unitOfWork;
            this.mapperService = mapperService;
            this.imageService = imageService;
            this.userService = userService;
        }

        public void CreateNotification(int userId, string content, bool isFriendship, int targetId)
        {
            var date = this.dateTimeService.GetCurrentDate();
            var notification = this.notificationFactory.CreateNotification(userId, content, date, isFriendship, targetId);
            
            this.notificationRepository.Add(notification);
            this.unitOfWork.Commit();
        }

        public IEnumerable<NotificationUserViewModel> UserNotifications(int skip, int count, string userId)
        {
            var user = this.userService.GetByIndentityId(userId);

            var notifications = this.notificationRepository.All
                .Where(x => x.TargetUserId == user.Id && x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(count)
                .ToList();

            var userImageContents = notifications.Select(x => x.User.ProfileImage.Content).ToList();
            var mappedNotifications = notifications.Select(x => this.mapperService.MapObject<NotificationUserViewModel>(x)).ToList();
            for (int i = 0; i < mappedNotifications.Count; i++)
            {
                var notificationImageUrl = this.imageService.ByteArrayToImageUrl(userImageContents[i]);
                mappedNotifications[i].AuthorImageUrl = notificationImageUrl;
            }

            return mappedNotifications;
        }

        public void RemoveNotification(int id)
        {
            var notification = this.notificationRepository.GetById(id);
            notification.IsDeleted = true;
            this.notificationRepository.Update(notification);
            this.unitOfWork.Commit();
        }

        public void RemoveAllNotifications(string userId)
        {
            var notifications = this.notificationRepository.All
                .Where(x => x.User.AspIdentityUserId == userId && x.IsDeleted == false);

            foreach (var notification in notifications)
            {
                notification.IsDeleted = true;
                this.notificationRepository.Update(notification);
            }

            this.unitOfWork.Commit();
        }
    }
}
