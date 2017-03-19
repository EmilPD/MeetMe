﻿using System.Collections.Generic;
using MeetMe.Data.Models;

namespace MeetMe.Services.Contracts
{
    public interface IUserService
    {
        CustomUser GetById(int id);

        CustomUser GetByIndentityId(string id);

        IEnumerable<string> GetUsernames();

        void AddFriend(string userId, int friendId);
    }
}
