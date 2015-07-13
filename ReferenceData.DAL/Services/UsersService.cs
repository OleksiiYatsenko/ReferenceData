﻿using ReferenceData.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.DAL.Services
{
    public class UsersService
    {
        public User AddOrUpdate(User user)
        {
            using (var connection = new ReferenceDataEntities())
            {
                User oldValue = connection.Users.FirstOrDefault(x => x.Id == user.Id);
                if (oldValue != null)
                {
                    connection.Entry(oldValue).CurrentValues.SetValues(user);
                    connection.SaveChanges();
                    return null;
                }
                else
                {
                    connection.Users.Add(user);
                    connection.SaveChanges();
                    return user;
                }
            }
        }

        public IEnumerable<User> GetItems()
        {
            List<User> users;
            using (var connection = new ReferenceDataEntities())
            {
                users = connection.Users.ToList();
            }

            return users;
        }

        public User GetItem(int id)
        {
            User user;
            using (var connection = new ReferenceDataEntities())
            {
                user = connection.Users.FirstOrDefault(x => x.Id == id);
            }

            return user;
        }
    }
}
