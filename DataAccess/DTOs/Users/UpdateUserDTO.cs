﻿namespace DataAccess.DTOs.Users
{
    public class UpdateUserDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
