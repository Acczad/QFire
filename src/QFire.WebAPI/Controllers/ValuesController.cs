using ICD.Framework.Abstraction.Session;
using ICD.Framework.Abstraction.Log;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ICD.FrameWork.Controllers;
using System;
using ICD.Framework.Abstraction.Cache;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using ICD.Framework.Abstraction.Network;
using ICD.Framework.AppSetting;
using ICD.Framework.DataAnnotation;
using Microsoft.Extensions.Configuration;
using ICD.Framework.EventBus;
using ICD.Framework.MessageProvider.MsTeams;
using ICD.Framework.Queue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace ICD.FrameWork.TestCycleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        private readonly IAppSession session;
        private readonly ISanaLogger sanaLogger;
        private readonly ISanaCache sanaCache;
        private readonly IHttpService httpService;
        private readonly IConfiguration configuration;
        private readonly IGlobalVariables globalVariables;
        private readonly IEventBus _eventBus;
        private readonly ConfigOption _config;
        private readonly IRabbitFactory _rabbitFactory;

        public ValuesController(
            IAppSession session,
            ISanaLogger sanaLogger,
            ISanaCache sanaCache,
            IHttpService httpService,
            IConfiguration configuration,
            IGlobalVariables globalVariables,
            IEventBus eventBus,
            ConfigOption config,
            IRabbitFactory rabbitFactory) : base(session)
        {
            this.session = session;
            this.sanaLogger = sanaLogger;
            this.sanaCache = sanaCache;
            this.httpService = httpService;
            this.configuration = configuration;
            this.globalVariables = globalVariables;
            _eventBus = eventBus;
            _config = config;
            _rabbitFactory = rabbitFactory;
        }

        [DataContract]
        public class test
        {
            [DataMember]
            public int num1 { get; set; }
            [DataMember]
            public string persian { get; set; }
            [DataMember]
            public DateTimeOffset DateTimeOffset { get; set; }
            [DataMember]
            public DateTime Date { get; set; }
            [DataMember]
            public float num2 { get; set; }
            [DataMember]
            public double num3 { get; set; }
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            await sanaLogger.Information("test");
            //var a = await globalVariables.GetValueAsync("Languages");
            return new string[] { "value1", "value2" };
        }


        [HttpPost("test")]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] object test)
        {
            var dto = new
            {
                emails = new List<string>{"Hesam.Shojaee@Meditechsys.com"},
                message = "some message",
                languageRef = 0
            };

            try
            {
                var m = await httpService.PostAsync<MsTeamsSendResult>("http://ts.icdgroup.org:83/api/MicrosoftTeams/send-message-to-users",
                    dto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //_eventBus.SendCommand(m);
            return Ok();
        }
        
        [HttpPost("config")]
        [AllowAnonymous]
        public async Task<ActionResult> Config()
        {
            var cnn = _config.ConnectionString.BaseDbContext;
            var isCache = _config.GetCacheOptions().IsCacheEnabled;
            
            return Ok();
        }
        
        [HttpPost("rabbit")]
        [AllowAnonymous]
        public async Task<ActionResult> Rabbit()
        {
            var ch = _rabbitFactory.GetChannel();

            var other = _rabbitFactory.GetChannel();
            
            return Ok();
        }
        
        [HttpPost("EventSendCommand")]
        [AllowAnonymous]
        public async Task<ActionResult> EventSendCommand()
        {
            for (int i = 0; i < 500; i++)
            {
                _eventBus.SendCommandAsync(new Message("http://localhost:5000/api/values/consume", new Dto{Name = "hamid"}, "NewRabbitTest"));
            }
            
            return Ok();
        }
        
        [HttpPost("consume")]
        [AllowAnonymous]
        public async Task<ActionResult> Consume()
        {
            
            
            return Ok();
        }
        
        public interface ITest
        {
            void Log();
        }

        [Dependency(typeof(ITest))]
        public class Test : ITest
        {
            private readonly ISanaLogger sanaLogger;

            public Test(ISanaLogger sanaLogger)
            {
                this.sanaLogger = sanaLogger;

            }

            public void Log()
            {
                sanaLogger.Information("ffffff");
            }
        }


    }

    class Dto
    {
        public string Name { get; set; }
    }
}
