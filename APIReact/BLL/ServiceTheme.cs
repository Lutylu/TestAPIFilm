using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ServiceTheme : IServiceTheme
    {
        #region Constructeur
        private readonly APIReactContext _manager;
        public ServiceTheme(APIReactContext manager)
        {
            _manager = manager;
        }
        #endregion

        #region Add
        public bool Add(Theme theme)
        {
            try
            {
                _manager.Themes.Add(theme);
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
                Theme theme = _manager.Themes.Find(id);
                _manager.Themes.Remove(theme);
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
        public List<Theme> FindAll()
        {
            try
            {
                return _manager.Themes.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion

        #region FindById
        public Theme FindById(int id)
        {
            try
            {
                return _manager.Themes.Include(x => x.Films).FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion

        #region Update
        public bool Update(Theme theme)
        {
            try
            {
                Theme themeToUpdate = _manager.Themes.Find(theme.Id);
                _manager.Entry(themeToUpdate).CurrentValues.SetValues(theme);
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
