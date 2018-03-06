using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AxelotTest
{
    internal class FileCopier : ICopier
    {
        private static FileCopier _instance;

        private FileCopier()
        {

        }

        public static FileCopier GetCopier()
        {
            if (_instance == null)
            {
                return new FileCopier();
            }

            return _instance;
        }

        public void Copy(string src, string dest)
        {
            try
            {
                Console.WriteLine("Copiing: " + Path.GetFileName(src));
                Console.WriteLine(Thread.CurrentThread.Name + "copiing");
                File.Copy(src, dest, true);
                Manager.TotalAmount += new FileInfo(src).Length;
            }
            catch (Exception e)
            {
                Console.WriteLine("Errorr copiing file: " + e.Message);
            }
        }
    }
}
