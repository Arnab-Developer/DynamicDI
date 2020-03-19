using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDI
{
    public class ServiceA : IService
    {
        public string GetMessage(string name)
        {
            var message = $"Hello {name} from {nameof(ServiceA)}";
            return message;
        }
    }
}
