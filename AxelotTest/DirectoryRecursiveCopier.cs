using System;
using System.IO;

namespace AxelotTest
{
    /// <summary>
    /// Рекурсивный копировальщик. Синглтон.
    /// </summary>
    public class DirectoryRecursiveCopier : ICopier
    {
        /// <summary>
        /// Экземпляр
        /// </summary>
        private static DirectoryRecursiveCopier _instance;

        /// <summary>
        /// Объект-локер
        /// </summary>
        private static readonly object _locker = new object();

        /// <summary>
        /// Приватный конструктор.
        /// </summary>
        private DirectoryRecursiveCopier()
        {
        
        }

        /// <summary>
        /// Возвращает экземпляр копировальщика.
        /// </summary>
        /// <returns>экземпляр копировальщика</returns>
        public static DirectoryRecursiveCopier GetCopier()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    _instance = new DirectoryRecursiveCopier();
                }
            }
            return _instance;
        }

        /// <summary>
        /// Копирование.
        /// </summary>
        /// <param name="src">исходный каталог</param>
        /// <param name="dest">каталог назначения</param>
        /// <param name="isDeleteMode">удалять ли файлы и каталоги в исходном каталоге</param>
        public void Copy(string src, string dest, bool isDeleteMode)
        {
            Console.WriteLine("Copiing: " + Path.GetFileName(src));
            Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(dest), Path.GetFileName(src)));
            // Создание подкаталогов в каталоге назначения
            foreach (string directory in Directory.GetDirectories(src, "*", SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(directory.Replace(src, dest));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Не удалось создать подкаталог {0}", directory.Replace(src, dest) + e.Message);
                }
            }

            // Копирование файлов
            foreach (string file in Directory.GetFiles(src, "*.*", SearchOption.AllDirectories))
            {
                try
                {
                     File.Copy(file, file.Replace(src, dest), true);
                     Manager.TotalAmmount += new FileInfo(file).Length;
                     if (isDeleteMode)
                     {
                         try
                         {
                             File.Delete(src);
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine("Не удалось удалить файл: " + e.Message);
                         }
                     }
                }
                catch (Exception e)
                {
                     Console.WriteLine("Ошибка копирования файла {0}: ", src + e.Message);
                }
                if (isDeleteMode)
                {
                    try
                    {
                        Directory.Delete(src);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Не удалось удалить каталог {0}: ", src + e.Message);
                    }
                }
            }
        }
    }
}
