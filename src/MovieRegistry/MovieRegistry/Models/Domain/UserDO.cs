using MovieRegistry.Models.Entities;
using MovieRegistry.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.Models.Domain
{
    public class UserDO
    {
        public static bool CreateOrSetUser(string name)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                user = uow.Users.Where(u => u.Name == name).FirstOrDefault();
                if (user == null)
                {
                    user = new WindowsUser { Name = name };
                    uow.Users.Add(user);
                    uow.Save();

                    return true;
                }
            }
            return false;
        }

        public static WindowsUser GetUser()
        {
            return user;
        }

        private static WindowsUser user;
    }
}
