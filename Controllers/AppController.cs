using Microsoft.AspNetCore.Mvc;
using ex3___ApiLoginECors.Interfaces;
using ex3___ApiLoginECors.Model;
using ex3___ApiLoginECors.DTO;

namespace ex3___ApiLoginECors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ILoginService _login;
        
        private readonly IRegisterService _register;

        public AppController(ILoginService login, IRegisterService register)
        {
            _login = login;
            _register = register;

        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<RegisterDTO>>> register(RegisterDTO userToRegister)
        {
            return Ok ( await _register.Register(userToRegister) );

        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<LoginDTO>>> login(LoginDTO userToLogin)
        {
            return Ok ( await _login.Login(userToLogin) );

        }
    }
}