#define DEBUG
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AxelotTest
{
    internal class Copier
    {
        private object _locker;

        internal Copier()
        {
            _locker = new object();
        }
        internal void Copy(List<Item> items, string dest)
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

                        copier.Copy(item.Name, Path.Combine(dest, Path.GetFileName(item.Name)));
                    }
                }
#if DEBUG
                else
                {
                    Console.WriteLine(Thread.CurrentThread.Name + "skipping");
                }
#endif    
            }
        }
    }
}
