using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.Controllers
{
    [Route("User")]
    public class ValuesController : Controller
    {
    [Route("Value")]
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;


            Console.WriteLine(currentUser.Claims.First(c => c.Type == "Password"));

            Console.WriteLine(currentUser.FindAll("Password"));


            if (currentUser.HasClaim(c => c.Type == "Email"))
            {
                return View("Email");
            }

            if (spendingTimeWithCompany > 5)
            {
                return new string[] { "High Time1", "High Time2", "High Time3", "High Time4", "High Time5" };
            }
            else
            {
                return new string[] { "value1", "value2", "value3", "value4", "value5" };
            }
        }
    }
}