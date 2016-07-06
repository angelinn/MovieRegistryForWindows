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
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Registry.Instance.User = uow.Users.Where(u => u.Name == name).FirstOrDefault();
                    if (Registry.Instance.User == null)
                    {
                        Registry.Instance.User = new WindowsUser { Name = name };
                        uow.Users.Add(Registry.Instance.User);
                        uow.Save();
                        
                        return true;
                    }
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
