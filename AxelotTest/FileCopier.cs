using System;
using System.IO;

namespace AxelotTest
{
    /// <summary>
    /// Файловый копировальщик. Синглтон.
    /// </summary>
    public class FileCopier : ICopier
    {
        /// <summary>
        /// Экземпляр
        /// </summary>
        private static FileCopier _instance;

        /// <summary>
        /// Объект-локер
        /// </summary>
        private static readonly object _locker = new object();

        /// <summary>
        /// Приватный конструктор.
        /// </summary>
        private FileCopier()
        {

        }

        /// <summary>
        /// Возвращает экземпляр копировальщика.
        /// </summary>
        /// <returns>экземпляр копировальщика</returns>
        public static FileCopier GetCopier()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    _instance = new FileCopier();
                }
            }
            return _instance;
        }

        /// <summary>
        /// Копирование.
        /// </summary>
        /// <param name="src">исходный каталог</param>
        /// <param name="dest">каталог назначения</param>
        /// <param name="isDeleteMode">удалять ли исходные файлы</param>
        public void Copy(string src, string dest, bool isDeleteMode)
        {
            try
            {
                File.Copy(src, dest, true);
                Manager.TotalAmmount += new FileInfo(src).Length;
                if (isDeleteMode)
                {
                    try
                    {
                        File.Delete(src);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Не удалось удалить файл {0}: ", src + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка копирования файла {0}: ", src + e.Message);
            }
        }
    }
}
