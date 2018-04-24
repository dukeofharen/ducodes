﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Business
{
   public interface IUserManager
   {
      User GetUser(int id);

      IEnumerable<User> GetUsers();
   }
}