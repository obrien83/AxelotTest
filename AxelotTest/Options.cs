using System;
using System.IO;
using System.Text;
using CommandLine;

namespace AxelotTest
{
    /// <summary>
    /// Считывает и обрабатывает аргументы командной строки.
    /// </summary>
    public class Options
    {
        private string _inputDirectory;
        private string _outputDirecrory;
        private int _interval;
        private int _threadAmmount;

        public Options()
        {
            this.ThreadAmmount = 1;
        }

        /// <summary>
        /// Исходный каталог. Обязательный параметр.
        /// </summary>
        [Option("in", Required = true)]
        public string InputDirectory
        {
            get => _inputDirectory;
            set
            {
                try
                {
                    if (!CheckDirectoryExists(value)) throw new Exception("Неверный путь к каталогу назначения");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                    Console.WriteLine(GetUsage());
                    Environment.Exit(1);
                }
                _inputDirectory = value;
            }
        }

        /// <summary>
        /// Каталог назначения. Обязательный параметр.
        /// </summary>
        [Option("out", Required = true)]
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
                        Console.WriteLine("Не удалось создать каталог {0}: ", value + e.Message);
                        Console.WriteLine("Ошибка: " + e.Message);
                        Console.WriteLine(GetUsage());
                        Environment.Exit(1);
                    }
                }
                _outputDirecrory = value;
            }
        }

        /// <summary>
        /// Интервал копирования. Обязательный параметр.
        /// </summary>
        [Option('i', "interval", Required = true)]
        public int Interval
        {
            get => _interval;
            set
            {
                if (value < 0)
                {
                    try
                    {
                        throw new Exception("Интервал не может быть отрицательным числом");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Ошибка: " + e.Message);
                        Console.WriteLine(GetUsage());
                        Environment.Exit(1);
                    }
                }
                _interval = value * 1000;
            }
        }
        /// <summary>
        /// Количество потоков.
        /// </summary>
        [Option('t', "thread")]
        public int ThreadAmmount
        {
            get => _threadAmmount;
            set
            {
                try
                {
                    if (value < 1 || value > 10) throw new Exception("Неверное количество потоков, Должно быть от 1 до 10");
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
        /// <summary>
        /// Удалять ли файлы в исходном каталоге после копирования.
        /// </summary>
        [Option('d', "delete")]
        public bool IsDeleteMode { get; set; }
        /// <summary>
        /// Вывести ли в консоль файлы для копирования.
        /// </summary>
        [Option('p', "print")]
        public bool IsPrintMode { get; set; }
        /// <summary>
        /// Копировать ли все подкаталоги в исходном каталоге.
        /// </summary>
        [Option('r', "recursive")]
        public bool IsRecursive { get; set; }
        /// <summary>
        /// Справка.
        /// </summary>
        /// <returns></returns>
        [HelpOption]
        public string GetUsage()
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

        /// <summary>
        /// Проверка существует ли директория по указанному пути.
        /// </summary>
        /// <param name="path">Путь к каталогу.</param>
        /// <returns>Существует ли каталог по указанному пути.</returns>
        public bool CheckDirectoryExists(string path)
        {
            if (!Directory.Exists(path)) return false;
            return true;
        }
    }
}
