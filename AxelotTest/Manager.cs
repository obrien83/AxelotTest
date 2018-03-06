using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AxelotTest
{
    internal class Manager
    {
        private string _inputDirectory;
        private string _outputDirectory;
        private int _interval;
        private int _threadAmmount;
        private bool _isRecursive;
        private bool _isDeleteMode;
        private bool _isPrintMode;
        private List<Item> _items;
        private List<Thread> _threads;
        private static long _totalAmmount;

        internal static long TotalAmount
        {
            get => _totalAmmount;
            set => _totalAmmount = (value / 1000);
        }

        public Manager(Options options)
        {
            this._inputDirectory = options.InputDirectory;
            this._outputDirectory = options.OutputDirectory;
            this._interval = options.Interval;
            this._threadAmmount = options.ThreadAmmount;
            this._isRecursive = options.IsRecursive;
            this._isDeleteMode = options.IsDeleteMode;
            this._isPrintMode = options.IsPrintMode;
        }

        public void Run()
        {
            _items = new DirectoryReader(_inputDirectory, _isRecursive).GetItems();
            _threads = new List<Thread>();

            bool isStopped = false;
            string stop = null;

            while (!isStopped)
            {
                _threads.Clear();
                for (int i = 1; i <= _threadAmmount; i++)
                {
                    Thread newThread = new Thread(Manage);
                    newThread.Name = String.Format("{0} {1}", "Thread#", i);
                    newThread.Start();
                    _threads.Add(newThread);
                }
                Console.Clear();
                foreach (var item in _items)
                {
                    item.IsHandled = false;
                }
                Thread.Sleep(_interval);

                stop = Console.ReadLine();
                if (stop.Equals("stop"))
                {
                    foreach (var thread in _threads)
                    {
                       thread.Abort();
                    }

                    isStopped = true;
                    Console.WriteLine("Done");
                    Console.WriteLine("Total ammount: {0} {1}", TotalAmount, "kB");
                    Console.ReadKey();
                }
            }
        }
        private void Manage()
        {
            new Copier().Copy(_items, _outputDirectory);         
        }
    }
}
