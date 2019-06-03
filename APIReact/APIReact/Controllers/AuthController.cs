using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APIReact.Helpers;
using BLL.Interfaces;
using DAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APIReact.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        #region Injestion dépendance
        private readonly IServiceUser _serviceUser;
        private readonly IConfiguration _configuration;
        public AuthController(IServiceUser iserviceUser, IConfiguration configuration)
        {
            _serviceUser = iserviceUser;
            _configuration = configuration;
        }
        #endregion

        #region Login
        [HttpPost]
        [ValidateModel]
        public IActionResult Login([FromBody] ViewAuth viewAuth)
        {
            try
            {
                var user = _serviceUser.Login(viewAuth.Login, viewAuth.Password);
                if (user == null)
                {
                    return BuildJsonResponse(404, "Utilisateur n'existe pas", null, "Login ou mot de passe incorrect");
                }

                var Claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                };
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityToken:Key"]));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);
                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _configuration["JwtSecurityToken:Issuer"],
                    audience: _configuration["JwtSecurityToken:Audience"],
                    claims: Claims,
                    expires: DateTime.UtcNow.AddMonths(1),
                    signingCredentials: signingCredentials
                    );


                var data = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    expiration = jwtSecurityToken.ValidTo,
                    currentuser = user
                };

                return BuildJsonResponse(200, "Authentification avec succès", data);
            }

            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
        #endregion
    }
}