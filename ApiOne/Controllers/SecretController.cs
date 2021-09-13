using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiOne.Controllers
{
    public class SecretController : Controller
    {
        [Route("/secret")]
        [Authorize]
        public string Secret()
        {
            var claims = User.Claims.ToList();
            return "secret message from ApiOne";
        }

        [Route("/restrictedSecret")]
        [Authorize("RestrictedByClaim")]
        public string RestrictedSecret()
        {
            return "secret message from restricted endpoint in ApiOne";
        }
    }
}
