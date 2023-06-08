using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace WebAPI
{
    public static class WebApiConfig
    {


        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
        public static string GenerateJwtToken()
        {
            
            var secretKey = "skididibobbob";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

       
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature, secretKey);

           
            var claims = new[]
            {
                new Claim("ClaimName1", "ClaimValue1"),
                new Claim("ClaimName2", "ClaimValue2")
               
            };

            // Create the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials
            };

            
            var tokenHandler = new JwtSecurityTokenHandler();

           
            var token = tokenHandler.CreateToken(tokenDescriptor);

            
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }


    }
}
