using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ArulOliNagar.Model;
using ArulOliNagar.Services;
namespace ArulOliNagar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController:ControllerBase
    {

        private readonly SmsService _smsService;
        private readonly IMemoryCache _cache;

        public SmsController(IMemoryCache cache , SmsService smsService)
        {
            _cache = cache;
            _smsService = smsService;
        }
        
        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] PhoneNumberModel request)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            // Save OTP temporarily in DB or cache (e.g., Redis)
            _cache.Set(request.PhoneNumber, otp, TimeSpan.FromMinutes(5));

            // Send OTP via Twilio (or Firebase)
            string formattedPhoneNumber = $"+91{request.PhoneNumber}";

            await _smsService.SendSms(formattedPhoneNumber, $"Your OTP is {otp}");

            return Ok(new { Message = "OTP Sent" });
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] OTPVerificationModel request)
        {
            if (_cache.TryGetValue(request.PhoneNumber, out string storedOtp) && storedOtp == request.Otp)
            {
                return Ok(new { Message = "Phone number verified" });
            }
            return BadRequest(new { Message = "Invalid OTP" });
        }
    }
}
