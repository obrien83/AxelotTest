using System;
using System.Collections.Generic;
using System.IO;

namespace AxelotTest
{
    /// <summary>
    ///  Читает файлы и каталоги в исходном каталоги.
    /// </summary>
    public class DirectoryReader
    {
        private string _directory;
        private bool _isRecursive;

        public DirectoryReader(string directory, bool isRecursive)
        {
            this._directory = directory;
            this._isRecursive = isRecursive;
        }

        public List<Item> GetItems()
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
                    Item itemToAdd = new Item(item) {IsDirectory = true};
                    items.Add(itemToAdd);
                }
            }
            return items;
        }
    }
}
