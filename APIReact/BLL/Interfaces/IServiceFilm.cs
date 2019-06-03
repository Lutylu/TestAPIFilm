using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IServiceFilm
    {
        bool Add(Film film);
        bool Update(Film film);
        bool Delete(int id);
        List<Film> FindAll();
        Film FindById(int id);
    }
}
