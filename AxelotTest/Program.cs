#define DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AxelotTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
#if DEBUG
                Console.WriteLine(options.Interval);
                Console.WriteLine(options.ThreadAmmount);
                Console.WriteLine(options.IsRecursive);
                Console.WriteLine(options.IsDeleteMode);
                Console.WriteLine(options.IsPrintMode);
#endif
                new Manager(options).Run();
            }
            if (options.IsRickRolled)
            {
                new Rick().Roll();
            }
        }
    }
}
