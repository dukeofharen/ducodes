using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Business
{
   internal class UserManager : IUserManager
   {
      private readonly List<User> _users = new List<User>
      {
         new User
         {
            Id = 1,
            FirstName = "Duco",
            LastName = "Winterwerp"
         },
         new User
         {
            Id = 2,
            FirstName = "Sinter",
            LastName = "Klaas"
         }
      };

      public User GetUser(int id)
      {
         return _users.FirstOrDefault(u => u.Id == id);
      }

      public IEnumerable<User> GetUsers()
      {
         return _users;
      }
   }
}