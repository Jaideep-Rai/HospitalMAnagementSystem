using BAL.Interfaces.Registration;
using DTO.Registration;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.RegistrationController
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        public readonly IRegistration _iRegistration;
        public RegistrationController(IRegistration iRegistration)
        {
            _iRegistration = iRegistration;
        }
        

    }
}

