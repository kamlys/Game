﻿using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IUserService
    {
        int[] RegisterUser(UserDto user);
        int LoginUser(UserDto user);
        List<UserDto> GetUser();
        bool Add(UserDto user);
        bool Update(UserDto user);
        bool Delete(int id);
        DolarDto UserDolar();
        UserDto Profil(string User);
        bool ChangePass(UserDto user, string User);
        bool ChangeEmail(UserDto user, string User);
        void AddFriend(FriendDto friend);
        List<FriendDto> GetFriendList(string User);
        void AcceptFriend(int id);
        void DontAcceptFriend(int id);
        void DeleteFriend(string userLogin, string friendLogin);
        int ifFriend(string user, string friend);
        bool Ignored(string user_login, string ignored_login);
        void AddIgnore(string userlogin, string ignorelogin);
        void DeleteIgnore(string userlogin, string ignorelogin);
        List<string> GetIgnored(string user);
        bool ForgetPassword(string email);
        int RecoveryPass(UserDto user);
        List<FriendDto> GetFriends();
        bool UpdateFriendAdmin(FriendDto friendDto);
        bool DeleteFriendAdmin(int id);
        bool AddFriendAdmin(FriendDto friendDto);
        List<IgnoredDto> GetIgnored();
        bool UpdateIgnoredAdmin(IgnoredDto friendDto);
        bool DeleteIgnoredAdmin(int id);
        bool AddIgnoredAdmin(IgnoredDto friendDto);
        void HelpMessage(string email, string content, string theme);
    }
}
