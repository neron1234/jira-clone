using JiraCloneBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiraCloneBackend.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            if (users == null)
            {
                return null;
            }

            foreach(User u in users)
            {
                u.WithoutPassword();
            }

            return users;
        }

        public static User WithoutPassword(this User user)
        {
            if (user == null)
            {
                return null;
            }

            user.NewPasswordSalt = null;
            user.PasswordHash = null;
            //user.PasswordSalt = null;
            return user;
        }
    }
}
