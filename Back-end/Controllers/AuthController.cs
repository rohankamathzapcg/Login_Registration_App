using Back_end.DataAccessLayer;
using Back_end.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly AuthInterFace _authInterface;
        public AuthController(AuthInterFace authInterFace)
        {
            _authInterface = authInterFace;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterRequest request)
        {
            RegistrationResponse response = new RegistrationResponse();
            try
            {

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequests request)
        {
            LoginResponse response = new LoginResponse();
            try
            {

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return Ok(response);
        }
    }
}
