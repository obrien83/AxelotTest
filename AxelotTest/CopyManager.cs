using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace AxelotTest
{
    /// <summary>
    /// Вызов копировальщика, в соответствие с типом item.
    /// </summary>
    public class CopyManager
    {
        private object _locker;

        public CopyManager()
        {
            _locker = new object();
        }
        /// <summary>
        /// Вызов копировальщика, в соответствие с типом item
        /// </summary>
        /// <param name="items">список объектов для копирования</param>
        /// <param name="dest">каталог назначения</param>
        /// <param name="interval">интервал копирования</param>
        public void Copy(List<Item> items, string dest,int interval, bool isDeleteMode)
        {
            ICopier copier = null;

            foreach (var item in items)
            {
                if (!item.IsHandled)
                {
                    lock (_locker)
                    {
                        item.IsHandled = true;
                        if (item.IsDirectory) copier = DirectoryRecursiveCopier.GetCopier();
                        else copier = FileCopier.GetCopier();

                        copier.Copy(item.Name, Path.Combine(dest, Path.GetFileName(item.Name)), isDeleteMode);
                        Thread.Sleep(interval);
                    }
                }
            }
        }
    }
}
