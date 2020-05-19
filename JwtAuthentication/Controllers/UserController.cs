using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthentication.Models;
using JwtAuthentication.Repository;
using JwtAuthentication.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthentication.Controllers
{
    [Produces("application/json")]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IOptions<ConnectionStringModel> ConnectionString;
        public UserController(IOptions<ConnectionStringModel> app)
        {
            ConnectionString = app;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = DbClientFactory<UserDbClient>.Instance.GetAllUsers(ConnectionString.Value.cs);
            return Ok(data);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult InsertUser([FromBody]UserModel model)
        {
            var msg = new MessageModel<UserModel>();
            var data = DbClientFactory<UserDbClient>.Instance.InsertUser(model, ConnectionString.Value.cs);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                msg.ReturnMessage = "User saved successfully";
            }
            else if (data == "C201")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "email id already exists";
            }
            else
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Please Enter Valid Information";
            }
            return Ok(msg);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdateUser([FromBody]UserModel model)
        {
            var msg = new MessageModel<UserModel>();
            var data = DbClientFactory<UserDbClient>.Instance.UpdateUser(model, ConnectionString.Value.cs);
            if (data == "C202")
            {
                msg.IsSuccess = true;
                //if (model.Id == 0)
                msg.ReturnMessage = "User updated successfully";
                //else
                //    msg.ReturnMessage = "User updated successfully";
            }
            else
            {
                msg.ReturnMessage = "Please Insert valid User Id";
            }
            return Ok(msg);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteUser([FromBody] UserModel model)
        {
            var msg = new MessageModel<UserModel>();
            var data = DbClientFactory<UserDbClient>.Instance.DeleteUser(model.Id, ConnectionString.Value.cs);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                if (model.Id == 0)
                    msg.ReturnMessage = "User Deleted";
                else
                    msg.ReturnMessage = "User updated successfully";
            }
            else if (data == "C203")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Invalid Record";
            }
            return Ok(msg);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser([FromBody]UserModel login)
        {
            IActionResult response = Unauthorized();
            var msg = new MessageModel<UserModel>();
            var res = DbClientFactory<UserDbClient>.Instance.AuthenticateUser(login, ConnectionString.Value.cs);
            if (res == "C200")
            {
                msg.IsSuccess = true;
                msg.ReturnMessage = "User Login successfully";

                UserModel user = null;

                user = new UserModel { Email = login.Email, Password = login.Password };

                GenerateJWT c = new GenerateJWT(ConnectionString);


                var tokenString = c.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            else
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Invalid Email or Password";
            }
            return response;
        }

        //private string GenerateJSONWebToken(UserModel userInfo)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConnectionString.Value.Key));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var claims = new[]{
        //        new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
        //        new Claim("Password", userInfo.Password),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())};
        //    var token = new JwtSecurityToken(ConnectionString.Value.Issuer,
        //      ConnectionString.Value.Issuer,
        //      claims,
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}