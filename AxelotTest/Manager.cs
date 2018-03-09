using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AxelotTest
{
    /// <summary>
    /// Запуск программы, определение параметров, если не использованы аргументы командной строки.
    /// </summary>
    public class Manager
    {
        /// <summary>
        ///Путь к исходному каталогу 
        /// </summary>
        private string _inputDirectory;
        /// <summary>
        ///Путь к каталогу назназначения. 
        /// </summary>
        private string _outputDirectory;
        /// <summary>
        /// Интервал копирования.
        /// </summary>
        private int _interval;
        /// <summary>
        /// Количество потоков.
        /// </summary>
        private int _threadAmmount;
        /// <summary>
        /// Флаг рекурсивного копирования.
        /// </summary>
        private bool _isRecursive;
        /// <summary>
        /// Флаг удаления исходных файлов.
        /// </summary>
        private bool _isDeleteMode;
        /// <summary>
        /// Флаг вывода прочитанных файлов в консоль.
        /// </summary>
        private bool _isPrintMode;
        /// <summary>
        /// Объекты, представляющие файлы и каталоги для копирования.
        /// </summary>
        private List<Item> _items;
        /// <summary>
        /// Потоки
        /// </summary>
        private List<Thread> _threads;
        /// <summary>
        /// Общий объем скопированных данных.
        /// </summary>
        private static double _totalAmmount;

        /// <summary>
        /// Свойство для общего объема скопированных данных в килобайтах.
        /// </summary>
        public static double TotalAmmount
        {
            get => _totalAmmount;
            set => _totalAmmount = (value);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="options">Аргументы командной строки.</param>
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

        /// <summary>
        /// Запускает программу. Ожидает ввода "stop" на экран для завершения копирования.
        /// </summary>
        public void Manage()
        {
            _items = new DirectoryReader(_inputDirectory, _isRecursive).GetItems();
            _threads = new List<Thread>();

            bool isStopped = false;

            Greet();
            if (_isPrintMode)
            {
                Console.WriteLine("Будут скопированы:");
                foreach (var item in _items)
                {
                    Console.WriteLine(item.Name);
                }
            }

            Console.WriteLine("Идет копирование файлов, для остановки введите \"stop\"");
            for (int i = 1; i <= _threadAmmount; i++)
            {
                Thread newThread = new Thread(Run) {Name = String.Format("{0} {1}", "Thread#", i)};
                newThread.Start();
                _threads.Add(newThread);
            }

            while (!isStopped)
            {
                string stop = Console.ReadLine();
                if (stop.Equals("stop"))
                {
                   foreach (var thread in _threads)
                   {
                      thread.Abort();
                   }
                    isStopped = true;
                    Thread.Sleep(10);
                    Console.WriteLine("Процесс копирования закончен.");
                    Console.WriteLine("Общий объем скопированных данных: {0} Кб", TotalAmmount / 1024);
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Запуск копирования.
        /// </summary>
        private void Run()
        {
            new CopyManager().Copy(_items, _outputDirectory, _interval, _isDeleteMode);         
        }

        private void Greet()
        {
            Console.WriteLine(GetUsage());
            if (!_isRecursive) Console.WriteLine("Будут скопированы файлы из каталога {0}", _inputDirectory);
            else Console.WriteLine("Будут скопированы файлы и подкаталоги из каталога {0}", _inputDirectory);
            Console.WriteLine("Для начала копирования нажмите любую клавишу");
            Console.ReadKey();
        }

        private string GetUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("Тестовое приложение для Axelot");
            usage.AppendLine("Использование флагов командной строки:");
            usage.AppendLine("--in исходный каталог.");
            usage.AppendLine("--out каталог назначения.");
            usage.AppendLine("-i или --interval интервал копирования.");
            usage.AppendLine("-t или --thread количество потоков, по умолчанию 1.");
            usage.AppendLine("-d или --delete удалять файлы в исходном каталоге после копирования, по умолчанию нет.");
            usage.AppendLine("-p или --print вывести в консоль файлы для копирования, по умолчанию нет.");
            usage.AppendLine("-r или -recursive копировать все подкаталоги в исходном каталоге, по умолчанию нет.");
            return usage.ToString();
        } 
    }
}
