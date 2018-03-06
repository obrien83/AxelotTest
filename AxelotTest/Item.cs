using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxelotTest
{
    internal class Item
    {
        internal Item(string name)
        {
            Name = name;
        }
        internal string Name { get; set; }
        internal bool IsHandled { get; set; }
        internal bool IsDirectory { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}