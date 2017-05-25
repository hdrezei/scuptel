using Microsoft.AspNetCore.Mvc;
using ScupTel.API.DataTransferObject;
using ScupTel.Domain.Localidade;
using System.Reflection;

namespace ScupTel.API.Controllers
{
    [Produces("application/json")]
    [Route("/Home")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ApiStatusDto Get()
        {
            return new ApiStatusDto("ScupTel.API", "Running", "1.0.0");
        }
    }
}
