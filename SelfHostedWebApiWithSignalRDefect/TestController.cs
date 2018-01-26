using System.Web.Http;

namespace SelfHostedWebApiWithSignalRDefect
{
    [HostAuthentication(Constants.AuthenticationType)]
    public class TestController : ApiController
    {

        [Route("Value")]
        [HttpGet]
        public string GetValue()
        {
            return "Hello World!";
        }
    }
}
