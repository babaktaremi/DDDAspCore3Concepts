using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Infrastructure.EventDispatchers.EventDispatchServices
{
   public class ConsoleEventDispatcher:IScopedDependency,IConsoleEventDispatcher
    {
        public void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
