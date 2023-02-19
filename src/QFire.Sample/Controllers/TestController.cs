using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QFire.Abstraction.Core;
using QFire.Abstraction.Message;

namespace QFire.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IQFire<TestMessage> qFire;

        public TestController(IQFire<TestMessage> qFire)
        {
            this.qFire=qFire;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {

            Task.Run(() => { SendMessage(); });
            Task.Run(() => { SendMessage(); });
            Task.Run(() => { SendMessage(); });
            Task.Run(() => { SendMessage(); });
            return Ok();
        }


        private async Task SendMessage()
        {
            for (int i = 0; i<5000; i++)
            {
                var message = new TestMessage { MyMessage=i.ToString() };
                if (i%2==0)
                    message.SetPriority(Priority.High);
                else
                    message.SetPriority(Priority.Low);
                await qFire.SendAsync(message);
            }
        }
    }
}
