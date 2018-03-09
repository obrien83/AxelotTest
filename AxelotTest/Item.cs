using System;

namespace AxelotTest
{
    /// <summary>
    /// Объект для копирования
    /// </summary>
    public class Item
    {
        public Item(string name)
        {
            Name = name;
        }
        internal string Name { get; set; }
        internal bool IsHandled { get; set; }
        internal bool IsDirectory { get; set; }
    }
}