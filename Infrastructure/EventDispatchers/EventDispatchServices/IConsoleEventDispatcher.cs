using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventDispatchers.EventDispatchServices
{
    public interface IConsoleEventDispatcher
    {
        void WriteToConsole(string message);
    }
}
