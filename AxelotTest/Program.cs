using System;

namespace AxelotTest
{
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                new Manager(options).Manage();
            }
        }
    }
}
