# DynamicDI

Example of dynamic depency injection with asp.net core. The tutorial is found at https://edi.wang/post/2018/12/28/dependency-injection-with-multiple-implementations-in-aspnet-core.

ASP.NET Core has dependency injection is built in. So you can mention the interface type in your controller and ASP.NET Core will provide the actual object of the interface at runtime. For this to work you need to write some configuration in the startup class.

I have a simple controller and service class. I want to use the service class inside the controller. I can mention the service interface type in the constructor of the controller. Also I need to register the service in the startup class so that ASP.NET Core can provide the actual type in the runtime.

    public interface IService
    {
        string GetHello(string name);
    }

    public class Service : IService
    {
        public string GetHello(string name)
        {
            return $"Hello {name}";
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IService), typeof(Service));
        }
    }

    public class HomeController : Controller
    {
        private readonly IService _s;

        public HomeController(IService s)
        {
            _s = s;
        }

        public IActionResult Index(string name)
        {
            var message = _s.GetHello(name);
            return View(message);
        }
    }
    
But if you have multiple implimentation of your IService interface then how you instruct ASP.NET Core to provide correct object in runtime? The process is very simple, you can actually get a collection of all implimentations and then choose the appropriate from them.

    public interface IService
    {
        string GetHello(string name);
    }
    
    public class ServiceA : IService
    {
        public string GetHello(string name)
        {
            return $"Hello {name}";
        }
    }
    
    public class ServiceB : IService
    {
        public string GetHello(string name)
        {
            return $"Hi {name}";
        }
    }
    
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IService), typeof(ServiceA));
            services.AddTransient(typeof(IService), typeof(ServiceB));
        }
    }
    
    public class HomeController : Controller
    {
        private readonly IEnumerable<IService> _services;

        public HomeController(IEnumerable<IService> services)
        {
            _services = services;
        }

        public IActionResult Index(string name)
        {
            var service = _services.First(s => s.GetType().Name == "ServiceA")
            var message = service.GetHello(name);
            return View(message);
        }
    }
