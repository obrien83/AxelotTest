#define DEBUG
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxelotTest
{
    internal class DirectoryReader
    {
        private string _directory;
        private bool _isRecursive;

        internal DirectoryReader(string directory, bool isRecursive)
        {
            this._directory = directory;
            this._isRecursive = isRecursive;
        }

        internal List<Item> GetItems()
        {
            List<Item> items = new List<Item>();
            string[] files = Directory.GetFiles(_directory);
            string[] directories = Directory.GetDirectories(_directory);

            foreach (var item in files)
            {
                Item itemToAdd = new Item(item);
                items.Add(itemToAdd);

            }

            if (_isRecursive && directories.Length != 0)
            {
                foreach (var item in directories)
                {
                    Item itemToAdd = new Item(item);
                    itemToAdd.IsDirectory = true;
                    items.Add(itemToAdd);
                }
            }
#if DEBUG
            foreach (var item in items)
            {
                Console.WriteLine(item.Name + " " + item.IsDirectory);
            }
#endif
            return items;
        }
    }
}
