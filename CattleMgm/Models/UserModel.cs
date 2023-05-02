﻿using CattleMgm.Helpers;

namespace CattleMgm.Models
{
    public class UserModel
    {
        public string Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public LanguageEnum Language { get; set; }


        public bool Notification { get; set; }

        public bool AdminNotification { get; set; }

        public string? ImageProfile { get; set; }
    }

    public class UserReportModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
