﻿using System;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.UserAggregate
{
    public class UserLogin:IdentityUserLogin<int>,IEntity
    {
        public UserLogin()
        {
            LoggedOn=DateTime.Now;
        }

        public User User { get; set; }
        public DateTime LoggedOn { get; set; }
    }

}
