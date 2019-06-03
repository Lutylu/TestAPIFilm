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
    public class ServiceFilm : IServiceFilm
    {
        #region Constructeur
        private readonly APIReactContext _manager;
        public ServiceFilm(APIReactContext manager)
        {
            _manager = manager;
        }
        #endregion

        #region Add
        public bool Add(Film film)
        {
            try
            {
                _manager.Films.Add(film);
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
                Film film = _manager.Films.Find(id);
                _manager.Films.Remove(film);
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
        public List<Film> FindAll()
        {
            try
            {
                return _manager.Films.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion

        #region FindById
        public Film FindById(int id)
        {
            try
            {
                return _manager.Films.Include(x=>x.Theme).FirstOrDefault(x=>x.Id == id);
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion

        #region Update
        public bool Update(Film film)
        {
            try
            {
                Film filmToUpdate = _manager.Films.Find(film.Id);
                _manager.Entry(filmToUpdate).CurrentValues.SetValues(film);
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
