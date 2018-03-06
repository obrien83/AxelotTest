using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxelotTest
{
    internal struct Rick
    {
        internal void Roll()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Never Gonna Give You Up !");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
