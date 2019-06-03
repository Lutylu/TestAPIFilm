using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IServiceTheme
    {
        bool Add(Theme theme);
        bool Update(Theme theme);
        bool Delete(int id);
        List<Theme> FindAll();
        Theme FindById(int id);
    }
}
