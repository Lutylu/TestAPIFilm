using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIReact.Helpers;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIReact.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // on autorise seulement les gens qui ont un token
    public class UserController : BaseController
    {
        #region Constructeur
        private readonly IServiceUser _serviceUser;
        public UserController(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }
        #endregion

        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<User> List = _serviceUser.FindAll();
                return BuildJsonResponse(200, "Succès", List);
            }
            catch (Exception e)
            {

                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
        #endregion

        #region Save
        [HttpPost] // On ne peut pas exécuter cette méthode à partir du navigateur
        [ValidateModel]
        public IActionResult Save([FromBody] User user)
        {
            try
            {
                if (_serviceUser.Add(user))
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succès", user);
                else
                    return BuildJsonResponse(400, "Erreur d'enregistrement");
            }
            catch (Exception e)
            {

                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
        #endregion

        #region Update
        [HttpPut] // Par convention 
        [ValidateModel] // pour vérifier les champs obligatoires d'un model
        public IActionResult Update([FromBody] User user) // Paramètre dans le Body de la méthode
        {
            try
            {
                if (_serviceUser.Update(user))
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succès", user);
                else
                    return BuildJsonResponse(400, "Erreur d'enregistrement");
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")] // Paramètre dans l'URL de la méthode
        public IActionResult Delete(int id)
        {
            try
            {
                if (_serviceUser.Delete(id))
                    return BuildJsonResponse(201, "Utilisateur supprimé avec succès");
                else
                    return BuildJsonResponse(400, "Erreur de suppression");
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
        #endregion

        #region GetById
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                User user = _serviceUser.FindById(id);
                return BuildJsonResponse(200, "Succès", user);
            }
            catch (Exception e)
            {

                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
        #endregion
    }
}