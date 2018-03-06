using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace AxelotTest
{
    internal class Options
    {
        private string _inputDirectory;
        private string _outputDirecrory;
        private int _interval;
        private int _threadAmmount;

        internal Options()
        {
            this.ThreadAmmount = 1;
        }

        [Option("in", Required = true, HelpText = "Input directory to read.")]
        public string InputDirectory
        {
            get => _inputDirectory;
            set
            {
                try
                {
                    if (!CheckDirectoryExists(value)) throw new Exception("Not correct path to source directory");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    Console.WriteLine(GetUsage());
                    Environment.Exit(1);
                }
                _inputDirectory = value;
            }
        }

        [Option("out", Required = true, HelpText = "Output directory to read.")]
        public string OutputDirectory
        {
            get => _outputDirecrory;
            set
            {
                if (!CheckDirectoryExists(value))
                {
                    try
                    {
                        new DirectoryInfo(value).Create();
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Can't create the unexisting output directory: " + e.Message);
                        Console.WriteLine(GetUsage());
                        Environment.Exit(1);
                    }
                }
                _outputDirecrory = value;
            }
        }

        [Option('i', "interval", Required = true, HelpText = "Interval of copiing")]
        public int Interval
        {
            get => _interval;
            set
            {
                if (value < 0)
                {
                    try
                    {
                        throw new Exception("Interval can not be negative");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                        Console.WriteLine(GetUsage());
                        Environment.Exit(1);
                    }
                }
                _interval = value * 1000;
            }
        }

        [Option('t', "thread", HelpText = "Thread ammount")]
        public int ThreadAmmount
        {
            get => _threadAmmount;
            set
            {
                try
                {
                    if (value <= 0 || value > 10) throw new Exception("Invalid number of threads");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(GetUsage());
                    Environment.Exit(1);
                }

                _threadAmmount = value;
            }
        }

        [Option('d', "delete", HelpText = "Delete files in source directory")]
        public bool IsDeleteMode { get; set; }

        [Option('p', "print", HelpText = "Show copied files")]
        public bool IsPrintMode { get; set; }

        [Option('r', "recursive", HelpText = "Recursive copiing")]
        public bool IsRecursive { get; set; }

        [Option("rick", HelpText = "RickRoll!!!")]
        public bool IsRickRolled { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            // this without using CommandLine.Tex
            //  or using HelpText.AutoBuild
             var usage = new StringBuilder();
             usage.AppendLine("Quickstart Application 1.0");
             usage.AppendLine("Read user manual for usage instructions...");
             return usage.ToString();
        }

        public bool CheckDirectoryExists(string path)
        {
            if (!Directory.Exists(path)) return false;
            return true;
        }
    }
}
