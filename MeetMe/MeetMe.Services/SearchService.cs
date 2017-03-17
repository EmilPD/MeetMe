﻿using MeetMe.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using MeetMe.Web.Models.Search;
using MeetMe.Data.Contracts;
using MeetMe.Data.Models;
using Bytes2you.Validation;

namespace MeetMe.Services
{
    public class SearchService : ISearchService
    {
        private readonly IEFRepository<CustomUser> userRepository;
        private readonly IMapperService mapperService;
        private readonly IImageService imageService;
        private readonly IUserService userService;
        private readonly IFriendService friendService;

        public SearchService(
            IEFRepository<CustomUser> userRepository,
            IMapperService mapperService,
            IImageService imageService,
            IUserService userService,
            IFriendService friendService)
        {
            Guard.WhenArgument(userRepository, "UserRepository").IsNull().Throw();
            Guard.WhenArgument(mapperService, "MapperService").IsNull().Throw();
            Guard.WhenArgument(imageService, "ImageService").IsNull().Throw();
            Guard.WhenArgument(userService, "UserService").IsNull().Throw();
            Guard.WhenArgument(friendService, "FriendService").IsNull().Throw();

            this.userRepository = userRepository;
            this.mapperService = mapperService;
            this.imageService = imageService;
            this.userService = userService;
            this.friendService = friendService;
        }

        public IEnumerable<SearchUserViewModel> SearchedUsers(string pattern, int skip, int count, string userId)
        {
            var users = this.userRepository.All
                .Where(x => x.FullName.Contains(pattern))
                .OrderBy(x => x.Id)
                .Skip(skip)
                .Take(count)
                .ToList();

            var result = users.Select(x => this.mapperService.MapObject<SearchUserViewModel>(x)).ToList();
            var currentUser = this.userService.GetByIndentityId(userId);
            var currentUserFriendsIds = this.friendService.GetAllUserFriendsIds(currentUser.Id);
            for (int i = 0; i < users.Count; i++)
            {
                bool isFriend = currentUserFriendsIds.Contains(users[i].Id);
                result[i].IsFriend = isFriend;
                result[i].ImageUrl = this.imageService.ByteArrayToImageUrl(users[i].ProfileImage.Content);
            }

            return result;
        }
    }
}
