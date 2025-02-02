namespace ArulOliNagar.Controllers
{
    using ArulOliNagar.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SupabaseService _supabaseService;

        public AuthController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            try
            {
                var user = await _supabaseService.SignUpAsync(request.Email, request.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            try
            {
                var user = await _supabaseService.SignInAsync(request.Email, request.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                await _supabaseService.SignOutAsync();
                return Ok("Signed out successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("current")]
        public IActionResult GetCurrentUser()
        {
            var user = _supabaseService.GetCurrentUser();

            if (user == null)
            {
                return Unauthorized(new { Message = "No user is currently authenticated." });
            }

            return Ok(user);
        }
    }

    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
