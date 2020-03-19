using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDI
{
    public interface IService
    {
        string GetMessage(string name);
    }
}
