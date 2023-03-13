using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using API.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    public class BuggyController :BaseApiController
    {
        private readonly DataContext _context;
       public BuggyController(DataContext context)
       {
        _context=context;
       }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> getSecret()
        {
            return "Secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<Users> GetNotFound()
        {
           var things=_context.Users.Find(-1);

           if(things==null) return NotFound();
           return things;
        }

        [HttpGet("Server-Error")]
        public ActionResult<string> GetServerError()
        {
           var things=_context.users.FindAsync(-1);
           return things.toSting();
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequst()
        {
            return badRequest("this is not a good request");
        }

        
    }
}