using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MyProject.Services;
using System.Buffers;
using System.Collections.Generic;
using System.Security.Policy;

namespace MyProject.Controllers
{
    [Route("test-di")]
    public class DILifeCycleController : Controller
    {
        public MyOperationService MyOperationService { get; set; }
        public MyOperationService MyOperationService2 { get; set; }
        public IServiceProvider ServiceProvider { get; set; }


        public DILifeCycleController(MyOperationService myOperationService, 
                                     MyOperationService myOperationService2,
                                     IServiceProvider serviceProvider)
        {
            MyOperationService = myOperationService;
            MyOperationService2 = myOperationService2;
            ServiceProvider = serviceProvider;
        }
        public string Index()
        {

            return 
                "First instance: " + Environment.NewLine +
                "Transient        : " +  MyOperationService.Transient.OperationId + Environment.NewLine +
                "Scoped           : " +  MyOperationService.Scoped.OperationId + Environment.NewLine +
                "Singleton        : " +  MyOperationService.Singleton.OperationId + Environment.NewLine +
                "SingletonInstance: " +  MyOperationService.SingletonInstance.OperationId + Environment.NewLine +

                Environment.NewLine +
                Environment.NewLine +

                "Second instance:" + Environment.NewLine +
                "Transient        :" + MyOperationService2.Transient.OperationId + Environment.NewLine +
                "Scoped           :" + MyOperationService2.Scoped.OperationId + Environment.NewLine +
                "Singleton        :" + MyOperationService2.Singleton.OperationId + Environment.NewLine +
                "SingletonInstance:" + MyOperationService2.SingletonInstance.OperationId + Environment.NewLine;
        }

        [Route("test")]
        public string DIIntoTheAction([FromServices] MyOperationService myOperationService)
        {
            return "Transient        : " + MyOperationService.Transient.OperationId + Environment.NewLine +
                   "Scoped           : " + MyOperationService.Scoped.OperationId + Environment.NewLine +
                   "Singleton        : " + MyOperationService.Singleton.OperationId + Environment.NewLine +
                   "SingletonInstance: " + MyOperationService.SingletonInstance.OperationId + Environment.NewLine;
        }

        [Route("view")]
        public IActionResult DIIntoTheView()
        {
            return View("Index");
        }

        [Route("container")]
        public string TestContainer()
        {
            using(var serviceScope = ServiceProvider.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var singService = services.GetRequiredService<IOperationSingleton>();

                return "Instance Singleton: " + Environment.NewLine + 
                    singService.OperationId + Environment.NewLine;
            }
        }
    }
}
