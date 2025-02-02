
namespace ArulOliNagar.Services
{
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;
    using Twilio.Types;
    public class SmsService
    {
        private readonly IConfiguration _config;

        public SmsService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendSms(string to, string message)
        {
            var accountSid = _config["Twilio:AccountSid"];
            var authToken = _config["Twilio:AuthToken"];
            var fromNumber = _config["Twilio:PhoneNumber"];
        

            TwilioClient.Init(accountSid, authToken);

            await MessageResource.CreateAsync(
                body: message,
                from: new PhoneNumber(fromNumber),
                to: new PhoneNumber(to)
            );
        }
    }

}
