using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IServiceUser
    {
        bool Add(User user);
        bool Update(User user);
        bool Delete(int id);
        List<User> FindAll();
        User FindById(int id);
        User Login(string login, string password);
    }
}
