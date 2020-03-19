using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDI
{
    public class ServiceB : IService
    {
        public string GetMessage(string name)
        {
            var message = $"Hello {name} from {nameof(ServiceB)}";
            return message;
        }
    }
}
