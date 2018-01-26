using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(SelfHostedWebApiWithSignalRDefect.Startup))]
namespace SelfHostedWebApiWithSignalRDefect
{
    public class TokenFormatter : ISecureDataFormat<AuthenticationTicket>
    {
        public string Protect(AuthenticationTicket data)
        {
            return string.Empty;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.NameIdentifier, "Bob")
            };
            var identity = new ClaimsIdentity(claims, Constants.AuthenticationType);
            var ticket = new AuthenticationTicket(identity, null);
            return ticket;
        }
    }
}
