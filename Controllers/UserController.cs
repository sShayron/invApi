using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using invApi.Models;
using System.Collections.Generic;

namespace invApi.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpGet]
        public List<User> GetAll([FromServices]ModelContext context)
        {
            return context.Users.ToList();
        }
    }
}