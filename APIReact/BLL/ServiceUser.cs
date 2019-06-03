using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ServiceUser : IServiceUser
    {
        #region Constructeur
        private readonly APIReactContext _manager;
        public ServiceUser(APIReactContext manager) 
        {
            _manager = manager;  
        }
        #endregion

        #region Add
        public bool Add(User user)
        {
            try
            {
                _manager.Users.Add(user);
                _manager.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int id)
        {
            try
            {
                User user = _manager.Users.Find(id);
                _manager.Users.Remove(user);
                _manager.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region FindAll
        public List<User> FindAll()
        {
            try
            {
                return _manager.Users.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion

        #region FindById
        public User FindById(int id)
        {
            try
            {
                return _manager.Users.Find(id);
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion

        #region Login
        public User Login(string login, string password)
        {
            try
            {
                return _manager.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            }
            catch (Exception)
            {

                return null;
            }
        } 
        #endregion

        #region Update
        public bool Update(User user)
        {
            try
            {
                User userToUpdate = _manager.Users.Find(user.Id);
                _manager.Entry(userToUpdate).CurrentValues.SetValues(user);
                _manager.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        } 
        #endregion
    }
}
